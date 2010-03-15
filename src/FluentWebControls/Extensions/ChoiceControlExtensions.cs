using System.Collections.Generic;

namespace FluentWebControls.Extensions
{
	public static class ChoiceControlExtensions
	{
		public static ChoiceControl WithSelectedValue(this ChoiceControl input, string value)
		{
			input.SelectedValue = value;
			return input;
		}

		public static ChoiceControl WithListItems(this ChoiceControl input, IEnumerable<KeyValuePair<string, string>> value)
		{
			input.ListItems = value;
			return input;
		}

		public static ComboSelectData AsComboSelect(this ChoiceControl input)
		{
			IChoiceControl data = input;
			var comboSelect = new ComboSelectData(data.ListItems)
				.WithId(data.Id)
				.WithIdPrefix(data.IdPrefix);
			comboSelect.SelectedValues.Add(data.SelectedValue);
			return comboSelect;
		}

		public static DropDownListData AsDropDownList(this ChoiceControl input)
		{
			IChoiceControl data = input;
			return new DropDownListData(data.ListItems)
				.WithSelectedValue(data.SelectedValue)
				.WithId(data.Id)
				.WithIdPrefix(data.IdPrefix);
		}
	}
}