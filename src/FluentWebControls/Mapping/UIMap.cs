using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using FluentWebControls.Extensions;

using MvbaCore;
using MvbaCore.Extensions;

namespace FluentWebControls.Mapping
{
	public class UIMap<TDomain, TModel>
	{
		private readonly Dictionary<string, object> _mappings = new Dictionary<string, object>();

		protected UIMap(TDomain item)
		{
			Item = item;
		}

		public TDomain Item { get; private set; }

		public CheckBoxData CheckBoxFor(Expression<Func<TModel, object>> source)
		{
			var uiMap = TryGetRequestedMap(source);
			var booleanMap = uiMap.TryCastTo<BooleanMap>();
			return new BooleanControl()
				.WithId(booleanMap.Id)
				.WithIdPrefix(booleanMap.IdPrefix)
				.SetCheckedTo(booleanMap.IsChecked)
				.AsCheckBox();
		}

		public ComboSelectData ComboSelectFor(Expression<Func<TModel, object>> source)
		{
			var uiMap = TryGetRequestedMap(source);
			var listUiMap = uiMap.TryCastTo<IChoiceListUIMap>();
			return new ChoiceControl()
				.WithId(listUiMap.Id)
				.WithIdPrefix(listUiMap.IdPrefix)
				.WithSelectedValue(listUiMap.SelectedValue)
				.WithListItems(listUiMap.ListItems)
				.AsComboSelect();
		}

		protected BooleanMap ConfigureBoolean(Expression<Func<TModel, object>> forId, Func<TDomain, bool> getItemValue)
		{
			string propertyName = Reflection.GetPropertyName(forId);
			var booleanControl = new BooleanMap(propertyName, getItemValue(Item));
			_mappings.Add(propertyName, booleanControl);
			return booleanControl;
		}

		protected ChoiceListUIMap<TDomain, TModel, TItemType> ConfigureChoiceList<TItemType>(
			Expression<Func<TModel, object>> forId,
			Func<TDomain, TItemType> getSelectedItem,
			Func<TItemType, string> getItemText,
			Func<TItemType, string> getItemValue)
		{
			string propertyName = Reflection.GetPropertyName(forId);
			var listUiMap = new ChoiceListUIMap<TDomain, TModel, TItemType>(propertyName, getSelectedItem(Item), getItemText, getItemValue);
			_mappings.Add(propertyName, listUiMap);
			return listUiMap;
		}

		protected FreeTextUIMap<TDomain> ConfigureFreeText(Expression<Func<TModel, object>> forId, Func<TDomain, string> getValue)
		{
			string propertyName = Reflection.GetPropertyName(forId);
			var freeTextUiMap = new FreeTextUIMap<TDomain>(Item,
			                                               propertyName,
			                                               getValue);
			_mappings.Add(propertyName, freeTextUiMap);
			return freeTextUiMap;
		}

		protected ListUIMap<TChildDomain, TChildModel> ConfigureList<TChildDomain, TChildModel>(
			Expression<Func<TModel, object>> forIdPrefix,
			Func<TDomain, IEnumerable<TChildDomain>> getSourceItems,
			Func<TDomain, IEnumerable<TChildDomain>, ListUIMap<TChildDomain, TChildModel>> createMapper)
		{
			string propertyName = Reflection.GetPropertyName(forIdPrefix);
			var listUIMap = createMapper(Item, getSourceItems(Item));
			_mappings.Add(propertyName, listUIMap);
			return listUIMap;
		}

		protected void ConfigureMap<TOutput, TItemType>(Expression<Func<TModel, object>> forId, Func<TDomain, TItemType> getItem, Func<TItemType, TOutput> createMap)
		{
			string propertyName = Reflection.GetPropertyName(forId);
			var uiMap = createMap(getItem(Item));
			_mappings.Add(propertyName, uiMap);
		}

		public DropDownListData DropDownListFor(Expression<Func<TModel, object>> source)
		{
			var uiMap = TryGetRequestedMap(source);
			var listUiMap = uiMap.TryCastTo<IChoiceListUIMap>();
			return new ChoiceControl()
				.WithId(listUiMap.Id)
				.WithIdPrefix(listUiMap.IdPrefix)
				.WithSelectedValue(listUiMap.SelectedValue)
				.WithListItems(listUiMap.ListItems)
				.AsDropDownList();
		}

		public HiddenData HiddenFor(Expression<Func<TModel, object>> source)
		{
			var uiMap = TryGetRequestedMap(source);
			var freeTextUiMap = uiMap.TryCastTo<FreeTextUIMap<TDomain>>();
			return new FreeTextControl()
				.WithId(freeTextUiMap.Id)
				.WithIdPrefix(freeTextUiMap.IdPrefix)
				.WithValue(freeTextUiMap.Value)
				.AsHidden();
		}

		public TOutput MapFor<TOutput>(Expression<Func<TModel, object>> source) where TOutput : class
		{
			var uiMap = TryGetRequestedMap(source);
			var listUIMap = uiMap.TryCastTo<TOutput>();
			return listUIMap;
		}

		public TextAreaData TextAreaFor(Expression<Func<TModel, object>> source)
		{
			var uiMap = TryGetRequestedMap(source);
			var freeTextUiMap = uiMap.TryCastTo<FreeTextUIMap<TDomain>>();
			return new FreeTextControl()
				.WithId(freeTextUiMap.Id)
				.WithIdPrefix(freeTextUiMap.IdPrefix)
				.WithValue(freeTextUiMap.Value)
				.AsTextArea();
		}

		public TextBoxData TextBoxFor(Expression<Func<TModel, object>> source)
		{
			var uiMap = TryGetRequestedMap(source);
			var freeTextUiMap = uiMap.TryCastTo<FreeTextUIMap<TDomain>>();
			return new FreeTextControl()
				.WithId(freeTextUiMap.Id)
				.WithIdPrefix(freeTextUiMap.IdPrefix)
				.WithValue(freeTextUiMap.Value)
				.AsTextBox();
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