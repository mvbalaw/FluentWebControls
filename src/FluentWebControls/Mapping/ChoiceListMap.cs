//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using FluentWebControls.Interfaces;

namespace FluentWebControls.Mapping
{
	public class ChoiceListMap<TDomain, TModel, TItemType> : IChoiceListMap, IFreeTextMap
		where TDomain : class, new()
	{
		private readonly Func<TItemType, string> _getItemText;
		private readonly Func<TItemType, string> _getItemValue;
		private readonly List<string> _selectedValues = new List<string>();
		private Func<IEnumerable<TItemType>> _getListItems;
		private IEnumerable<KeyValuePair<string, string>> _listItems;

		public ChoiceListMap(string id,
			Func<TItemType, string> getItemText,
			Func<TItemType, string> getItemValue)
		{
			_getItemText = getItemText;
			_getItemValue = getItemValue;
			Id = id;
		}

		public ChoiceListMap(string id,
			TItemType selectedItem,
			Func<TItemType, string> getItemText,
			Func<TItemType, string> getItemValue)
			: this(id, getItemText, getItemValue)
		{
			_selectedValues.Add(getItemValue(selectedItem));
		}

		public IEnumerable<string> SelectedValues
		{
			get { return _selectedValues; }
		}

		public string Id { get; private set; }

		public IEnumerable<KeyValuePair<string, string>> ListItems
		{
			get { return _listItems ?? (_listItems = _getListItems().Select(x => new KeyValuePair<string, string>(_getItemText(x), _getItemValue(x)))); }
		}

		public string SelectedValue
		{
			get { return _selectedValues.FirstOrDefault(); }
		}

		public IPropertyMetaData Validation { get; set; }

		object IModelMap.GetValueForModel()
		{
			return SelectedValue;
		}

		string IFreeTextMap.Value
		{
			get
			{
				var lookup = ListItems
					.GroupBy(x => x.Key)
					.Select(x => x.First())
					.Where(x => x.Value != null)
					.Where(x => x.Key != null)
					.ToDictionary(x => x.Value, x => x.Key);
				var selected = _selectedValues.FirstOrDefault(x => lookup.ContainsKey(x));
				if (selected == null)
				{
					return null;
				}
				return lookup[selected];
			}
		}

		public ChoiceListMap<TDomain, TModel, TItemType> WithItems(Func<IEnumerable<TItemType>> getListItems)
		{
			_getListItems = getListItems;
			return this;
		}

		public ChoiceListMap<TDomain, TModel, TItemType> WithSelectedItems(IEnumerable<TItemType> selectedItems)
		{
			_selectedValues.AddRange(selectedItems.Select(x => _getItemValue(x)));
			return this;
		}

		public ChoiceListMap<TDomain, TModel, TItemType> WithValidation(Expression<Func<TDomain, TItemType>> getProperty)
		{
			Validation = Configuration.ValidationMetaDataFactory.GetFor(getProperty);
			return this;
		}

		public ChoiceListMap<TDomain, TModel, TItemType> WithValidation(Action<IFieldValidationBuilder> updateValidation)
		{
			var propertyMetaDataWrapper = new PropertyMetaDataWrapper(Validation);
			updateValidation(propertyMetaDataWrapper);
			Validation = propertyMetaDataWrapper;
			return this;
		}
	}
}