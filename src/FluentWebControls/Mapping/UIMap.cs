using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using FluentWebControls.Extensions;
using MvbaCore;

namespace FluentWebControls.Mapping
{
	public class UIMap<TDomain, TModel> : IUIMap
		where TDomain : class, new()
	{
		private readonly dynamic _container = new ExpandoObject();
		private readonly IDictionary<string, object> _mappings;
		private string _idPrefix;

		protected UIMap(TDomain item)
		{
			Item = item.ToNonNull();
			_mappings = (IDictionary<string, object>) _container;
			foreach (var matchingProperty in Reflection
				.GetMatchingProperties(typeof (TDomain), typeof (TModel)))
			{
				var source = matchingProperty.SourcePropertyType;
				if (source.IsGenericType &&
				    (source.GetGenericTypeDefinition() == typeof (List<>) ||
				     source.GetGenericTypeDefinition() == typeof (IList<>)))
				{
					continue;
				}
				var property = matchingProperty.DestinationPropertyType;
				if (property.IsGenericType &&
				    (property.GetGenericTypeDefinition() == typeof (List<>) ||
				     property.GetGenericTypeDefinition() == typeof (IList<>)))
				{
					continue;
				}

				_mappings.Add(matchingProperty.Name, GetMap(matchingProperty));
			}
		}

		public TDomain Item { get; private set; }

		#region IUIMap Members

		public void WithIdPrefix(string prefix)
		{
			if (_idPrefix.IsNullOrEmpty())
			{
				_idPrefix = prefix;
				return;
			}
			_idPrefix = _idPrefix.CombineWithWebCompatibleSeparator(prefix);
		}

		#endregion

		public void Populate(TModel model)
		{
			var properties = typeof (TModel).GetProperties()
				.ToDictionary(x => x.Name, x => x);
			foreach (var mapping in _mappings)
			{
				if (!properties.ContainsKey(mapping.Key)) continue;
				var property = properties[mapping.Key];
				var source = mapping.Value as IModelMap;
				if (source == null)
				{
					var listSource = mapping.Value as IListUIMap;
					if (listSource == null)
					{
						continue;
					}

					listSource.Populate(model);
					continue;
				}
				object valueForModel = source.GetValueForModel();
				if (valueForModel == null)
				{
					continue;
				}
				var stringValue = valueForModel.ToString();
				if (stringValue.Length == 0)
				{
					continue;
				}

				if (source is IChoiceListMap &&
				    property.PropertyType.IsGenericType)
				{
					if (property.PropertyType.GetGenericTypeDefinition() == typeof (List<>))
					{
						var itemType = property.PropertyType.GetGenericArguments()[0];
						var choiceList = source as IChoiceListMap;
						var items = choiceList.ListItems.Select(x => x.Value.To(itemType)).ToList();
						var targetList = property.GetValue(model, null);
						var addMethod = targetList.GetType().GetMethod("Add");
						foreach (var item in items)
						{
							addMethod.Invoke(targetList, new[] {item});
						}
					}
				}
				else
				{
					var convertedValue = stringValue.To(property.PropertyType);
					property.SetValue(model, convertedValue, null);
				}
			}
		}

		public CheckBoxData CheckBoxFor(Expression<Func<TModel, object>> source)
		{
			var uiMap = TryGetRequestedMap(source);
			var booleanMap = uiMap.TryCastTo<IBooleanMap>();
			return booleanMap.AsCheckBox().WithIdPrefix(_idPrefix);
		}

		public CheckBoxListData CheckBoxListFor(Expression<Func<TModel, object>> source)
		{
			var uiMap = TryGetRequestedMap(source);
			var listUiMap = uiMap.TryCastTo<IChoiceListMap>();
			return listUiMap.AsCheckBoxList().WithIdPrefix(_idPrefix);
		}

		public ComboSelectData ComboSelectFor(Expression<Func<TModel, object>> source)
		{
			var uiMap = TryGetRequestedMap(source);
			var listUiMap = uiMap.TryCastTo<IChoiceListMap>();
			return listUiMap.AsComboSelect().WithIdPrefix(_idPrefix);
		}

		protected BooleanMap ConfigureBoolean(Expression<Func<TModel, object>> forId, Func<TDomain, bool> getItemValue)
		{
			string propertyName = Reflection.GetPropertyName(forId);
			var booleanControl = new BooleanMap(propertyName, getItemValue(Item));
			_mappings[propertyName] = booleanControl;
			return booleanControl;
		}

		protected ChoiceListMap<TDomain, TModel, TItemType> ConfigureChoiceList<TItemType>(
			Expression<Func<TModel, object>> forId,
			Expression<Func<TDomain, TItemType>> getSelectedItem,
			Func<TItemType, string> getItemText,
			Func<TItemType, string> getItemValue)
		{
			string propertyName = Reflection.GetPropertyName(forId);
			var listUiMap = new ChoiceListMap<TDomain, TModel, TItemType>(propertyName, getSelectedItem.Compile()(Item),
			                                                              getItemText, getItemValue);
			TryAddValidation(getSelectedItem, listUiMap);
			if (_mappings.ContainsKey(propertyName) && listUiMap.Validation == null)
			{
				object value;
				bool exists = _mappings.TryGetValue(propertyName, out value);
				if (exists)
				{
					listUiMap.Validation = ((FreeTextMap<TDomain>) value).Validation;
				}
			}
			_mappings[propertyName] = listUiMap;
			return listUiMap;
		}

		protected ChoiceListMap<TDomain, TModel, TItemType> ConfigureChoiceList<TItemType>(
			Expression<Func<TModel, object>> forId,
			Expression<Func<TDomain, IEnumerable<TItemType>>> getSelectedItems,
			Func<TItemType, string> getItemText,
			Func<TItemType, string> getItemValue)
		{
			string propertyName = Reflection.GetPropertyName(forId);
			var listUiMap = new ChoiceListMap<TDomain, TModel, TItemType>(propertyName, getItemText, getItemValue);
			listUiMap.WithSelectedItems(getSelectedItems.Compile()(Item));
			TryAddValidation(getSelectedItems, listUiMap);
			if (listUiMap.Validation == null)
			{
				object value;
				bool exists = _mappings.TryGetValue(propertyName, out value);
				if (exists)
				{
					listUiMap.Validation = ((FreeTextMap<TDomain>) value).Validation;
				}
			}
			_mappings[propertyName] = listUiMap;
			return listUiMap;
		}

		protected FreeTextMap<TDomain> ConfigureFreeText(Expression<Func<TModel, object>> forId,
		                                                 Expression<Func<TDomain, string>> getValue)
		{
			string propertyName = Reflection.GetPropertyName(forId);
			var getValueFunction = getValue.Compile();
			var freeTextUiMap = new FreeTextMap<TDomain>(Item,
			                                             propertyName,
			                                             getValueFunction);
			TryAddValidation(getValue, freeTextUiMap);
			if (_mappings.ContainsKey(propertyName) && freeTextUiMap.Validation == null)
			{
				object value;
				bool exists = _mappings.TryGetValue(propertyName, out value);
				if (exists)
				{
					freeTextUiMap.Validation = ((FreeTextMap<TDomain>) value).Validation;
				}
			}
			_mappings[propertyName] = freeTextUiMap;
			return freeTextUiMap;
		}

		protected ListUIMap<TChildDomain, TChildModel> ConfigureList<TChildDomain, TChildModel>(
			Expression<Func<TModel, object>> forIdPrefix,
			Func<TDomain, IEnumerable<TChildDomain>> getSourceItems,
			Func<TDomain, IEnumerable<TChildDomain>, ListUIMap<TChildDomain, TChildModel>> createMapper)
		{
			string propertyName = Reflection.GetPropertyName(forIdPrefix);
			var listUiMap = createMapper(Item, getSourceItems(Item));
			_mappings[propertyName] = listUiMap;
			return listUiMap;
		}

		protected void ConfigureMap<TOutput, TItemType>(Expression<Func<TModel, object>> forId,
		                                                Func<TDomain, TItemType> getItem, Func<TItemType, TOutput> createMap)
		{
			string propertyName = Reflection.GetPropertyName(forId);
			var uiMap = createMap(getItem(Item));
			_mappings[propertyName] = uiMap;
		}

		public DropDownListData DropDownListFor(Expression<Func<TModel, object>> source)
		{
			var uiMap = TryGetRequestedMap(source);
			var listUiMap = uiMap.TryCastTo<IChoiceListMap>();
			return listUiMap.AsDropDownList().WithIdPrefix(_idPrefix);
		}

		private object GetMap(PropertyMappingInfo propertyMappingInfo)
		{
			var info = propertyMappingInfo;
			if (typeof (bool).IsAssignableFrom(propertyMappingInfo.SourcePropertyType))
			{
				var booleanMap = new BooleanMap(propertyMappingInfo.Name, (bool) info.GetValueFromSource(Item));
				return booleanMap;
			}

			var freeTextMap = new FreeTextMap<TDomain>(Item,
			                                           propertyMappingInfo.Name,
			                                           x =>
			                                           	{
			                                           		var source = info.GetValueFromSource(x);
			                                           		return source == null ? (string) null : source.ToString();
			                                           	});
			TryAddValidation(propertyMappingInfo.Name, freeTextMap);
			return freeTextMap;
		}

		public HiddenData HiddenFor(Expression<Func<TModel, object>> source)
		{
			var uiMap = TryGetRequestedMap(source);
			var freeTextUiMap = uiMap.TryCastTo<IFreeTextMap>();
			return freeTextUiMap.AsHidden().WithIdPrefix(_idPrefix);
		}

		public TOutput ListMapFor<TOutput>(Expression<Func<TModel, object>> source) where TOutput : class, IListUIMap
		{
			var uiMap = TryGetRequestedMap(source);
			var listUiMap = uiMap.TryCastTo<TOutput>();
			return listUiMap;
		}

		public TOutput MapFor<TOutput>(Expression<Func<TModel, object>> source) where TOutput : class, IUIMap
		{
			var uiMap = TryGetRequestedMap(source);
			var listUiMap = uiMap.TryCastTo<TOutput>();
			listUiMap.WithIdPrefix(_idPrefix);
			listUiMap.WithIdPrefix(Reflection.GetPropertyName(source));
			return listUiMap;
		}

		public RadioButtonListData RadioButtonListFor(Expression<Func<TModel, object>> source)
		{
			var uiMap = TryGetRequestedMap(source);
			var listUiMap = uiMap.TryCastTo<IChoiceListMap>();
			return listUiMap.AsRadioButtons().WithIdPrefix(_idPrefix);
		}

		public SpanData SpanFor(Expression<Func<TModel, object>> source)
		{
			var uiMap = TryGetRequestedMap(source);
			var freeTextUiMap = uiMap.TryCastTo<IFreeTextMap>();
			return freeTextUiMap.AsSpan().WithIdPrefix(_idPrefix);
		}

		public TextAreaData TextAreaFor(Expression<Func<TModel, object>> source)
		{
			var uiMap = TryGetRequestedMap(source);
			var freeTextUiMap = uiMap.TryCastTo<IFreeTextMap>();
			return freeTextUiMap.AsTextArea()
				.WithIdPrefix(_idPrefix);
		}

		public TextBoxData TextBoxFor(Expression<Func<TModel, object>> source)
		{
			var uiMap = TryGetRequestedMap(source);
			var freeTextUiMap = uiMap.TryCastTo<IFreeTextMap>();
			return freeTextUiMap.AsTextBox().WithIdPrefix(_idPrefix);
		}

		private static void TryAddValidation<TItemType>(Expression<Func<TDomain, TItemType>> getValue, IFreeTextMap map)
		{
			if (Configuration.ValidationMetaDataFactory != null)
			{
				try
				{
					map.Validation = Configuration.ValidationMetaDataFactory.GetFor(getValue);
				}
				catch
				{
				}
			}
		}

		private static void TryAddValidation(string propertyName, IFreeTextMap map)
		{
			if (Configuration.ValidationMetaDataFactory != null)
			{
				try
				{
					map.Validation = Configuration.ValidationMetaDataFactory.GetFor<TDomain>(propertyName);
				}
				catch
				{
				}
			}
		}

		private object TryGetRequestedMap(Expression<Func<TModel, object>> source)
		{
			string key = Reflection.GetPropertyName(source);
			object uiMap;
			if (!_mappings.TryGetValue(key, out uiMap))
			{
				throw new ArgumentException("No mapping defined for '" + key + "'");
			}
			return uiMap;
		}
	}
}