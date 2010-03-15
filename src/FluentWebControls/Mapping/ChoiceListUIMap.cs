using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentWebControls.Mapping
{
	public class ChoiceListUIMap<TDomain, TModel, TItemType> : IChoiceListUIMap
	{
		private readonly Func<TItemType, string> _getItemText;
		private readonly Func<TItemType, string> _getItemValue;
		private readonly List<string> _selectedValues = new List<string>();

		public ChoiceListUIMap(string id, TItemType selectedItem, Func<TItemType, string> getItemText,
		                       Func<TItemType, string> getItemValue)
		{
			_getItemText = getItemText;
			_getItemValue = getItemValue;
			_selectedValues.Add(getItemValue(selectedItem));
			Id = id;
		}

		public string Id { get; private set; }
		public string IdPrefix { get; set; }
		public IEnumerable<KeyValuePair<string, string>> ListItems { get; private set; }
		public string SelectedValue
		{
			get { return _selectedValues.FirstOrDefault(); }
		}

		public ChoiceListUIMap<TDomain, TModel, TItemType> WithItems(IEnumerable<TItemType> listItems)
		{
			ListItems = listItems.Select(x => new KeyValuePair<string, string>(_getItemText(x), _getItemValue(x)));
			return this;
		}
	}
}