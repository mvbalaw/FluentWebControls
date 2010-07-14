using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentWebControls.Extensions;
using MvbaCore;

namespace FluentWebControls.Mapping
{
	public class UIMap<TDomain, TModel> : IUIMap
		where TDomain : class, new()
	{
		private readonly Dictionary<string, object> _mappings;
		private string _idPrefix;

		protected UIMap(TDomain item)
		{
			Item = item.ToNonNull();
			_mappings = Reflection
				.GetMatchingProperties(typeof (TDomain), typeof (TModel))
				.ToDictionary(x => x.Name, x => GetMap(x));
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
			_idPrefix = String.Format("{0}.{1}", _idPrefix, prefix);
		}

		#endregion

		public CheckBoxData CheckBoxFor(Expression<Func<TModel, object>> source)
		{
			var uiMap = TryGetRequestedMap(source);
			var booleanMap = uiMap.TryCastTo<IBooleanMap>();
			return booleanMap.AsCheckBox().WithIdPrefix(_idPrefix);
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