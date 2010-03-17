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
				.GetMatchingProperties(typeof(TDomain), typeof(TModel))
				.ToDictionary(x => x.Name, x => GetMap(x));
		}

		public TDomain Item { get; private set; }

		public void WithIdPrefix(string prefix)
		{
			if (_idPrefix.IsNullOrEmpty())
			{
				_idPrefix = prefix;
				return;
			}
			_idPrefix = String.Format("{0}.{1}", _idPrefix, prefix);
		}

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
			Func<TDomain, TItemType> getSelectedItem,
			Func<TItemType, string> getItemText,
			Func<TItemType, string> getItemValue)
		{
			string propertyName = Reflection.GetPropertyName(forId);
			var listUiMap = new ChoiceListMap<TDomain, TModel, TItemType>(propertyName, getSelectedItem(Item), getItemText, getItemValue);
			_mappings[propertyName] = listUiMap;
			return listUiMap;
		}

		protected FreeTextMap<TDomain> ConfigureFreeText(Expression<Func<TModel, object>> forId, Func<TDomain, string> getValue)
		{
			string propertyName = Reflection.GetPropertyName(forId);
			var freeTextUiMap = new FreeTextMap<TDomain>(Item,
			                                             propertyName,
			                                             getValue);
			_mappings[propertyName] = freeTextUiMap;
			return freeTextUiMap;
		}

		protected ListUIMap<TChildDomain, TChildModel> ConfigureList<TChildDomain, TChildModel>(
			Expression<Func<TModel, object>> forIdPrefix,
			Func<TDomain, IEnumerable<TChildDomain>> getSourceItems,
			Func<TDomain, IEnumerable<TChildDomain>, ListUIMap<TChildDomain, TChildModel>> createMapper)
		{
			string propertyName = Reflection.GetPropertyName(forIdPrefix);
			var listUIMap = createMapper(Item, getSourceItems(Item));
			_mappings[propertyName] = listUIMap;
			return listUIMap;
		}

		protected void ConfigureMap<TOutput, TItemType>(Expression<Func<TModel, object>> forId, Func<TDomain, TItemType> getItem, Func<TItemType, TOutput> createMap)
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
			if (typeof(bool).IsAssignableFrom(propertyMappingInfo.SourcePropertyType))
			{
				return new BooleanMap(propertyMappingInfo.Name, (bool)info.GetValueFromSource(Item));
			}

			return new FreeTextMap<TDomain>(Item,
			                                propertyMappingInfo.Name,
			                                x =>
			                                	{
			                                		var source = info.GetValueFromSource(x);
			                                		return source == null ? (string)null : source.ToString();
			                                	});
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
			var listUIMap = uiMap.TryCastTo<TOutput>();
			return listUIMap;
		}

		public TOutput MapFor<TOutput>(Expression<Func<TModel, object>> source) where TOutput : class, IUIMap
		{
			var uiMap = TryGetRequestedMap(source);
			var listUIMap = uiMap.TryCastTo<TOutput>();
			listUIMap.WithIdPrefix(_idPrefix);
			listUIMap.WithIdPrefix(Reflection.GetPropertyName(source));
			return listUIMap;
		}

		public TextAreaData TextAreaFor(Expression<Func<TModel, object>> source)
		{
			var uiMap = TryGetRequestedMap(source);
			var freeTextUiMap = uiMap.TryCastTo<IFreeTextMap>();
			return freeTextUiMap.AsTextArea().WithIdPrefix(_idPrefix);
		}

		public TextBoxData TextBoxFor(Expression<Func<TModel, object>> source)
		{
			var uiMap = TryGetRequestedMap(source);
			var freeTextUiMap = uiMap.TryCastTo<IFreeTextMap>();
			return freeTextUiMap.AsTextBox().WithIdPrefix(_idPrefix);
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