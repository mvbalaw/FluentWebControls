using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentWebControls.Mapping
{
	public class ChoiceListMap<TDomain, TModel, TItemType> : IChoiceListMap, IFreeTextMap
	{
		private readonly Func<TItemType, string> _getItemText;
		private readonly Func<TItemType, string> _getItemValue;
		private readonly List<string> _selectedValues = new List<string>();
		private Func<IEnumerable<TItemType>> _getListItems;
		private IEnumerable<KeyValuePair<string, string>> _listItems;

		public ChoiceListMap(string id, TItemType selectedItem,
		                       Func<TItemType, string> getItemText,
		                       Func<TItemType, string> getItemValue)
		{
			_getItemText = getItemText;
			_getItemValue = getItemValue;
			_selectedValues.Add(getItemValue(selectedItem));
			Id = id;
		}

		public string Id { get; private set; }
		public IEnumerable<KeyValuePair<string, string>> ListItems
		{
			get
			{
				if (_listItems == null)
				{
					_listItems = _getListItems().Select(x => new KeyValuePair<string, string>(_getItemText(x), _getItemValue(x)));
				}
				return _listItems;
			}
		}
		public string SelectedValue
		{
			get { return _selectedValues.FirstOrDefault(); }
		}
		string IFreeTextMap.Value
		{
			get { return SelectedValue; }
		}

		public ChoiceListMap<TDomain, TModel, TItemType> WithItems(Func<IEnumerable<TItemType>> getListItems)
		{
			_getListItems = getListItems;
			return this;
		}
	}
}