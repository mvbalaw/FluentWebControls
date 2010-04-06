using FluentWebControls.Mapping;

namespace FluentWebControls.Extensions
{
	public static class IChoiceListMapExtensions
	{
		public static ComboSelectData AsComboSelect(this IChoiceListMap input)
		{
			var comboSelect = new ComboSelectData(input.ListItems)
				.WithId(input.Id)
				.WithValidationFrom(input.Validation);
			comboSelect.SelectedValues.Add(input.SelectedValue);
			return comboSelect;
		}

		public static DropDownListData AsDropDownList(this IChoiceListMap input)
		{
			return new DropDownListData(input.ListItems)
				.WithSelectedValue(input.SelectedValue)
				.WithId(input.Id)
				.WithValidationFrom(input.Validation);
		}
	}
}