using FluentWebControls.Mapping;

namespace FluentWebControls.Extensions
{
	public static class IChoiceListMapExtensions
	{
		public static ComboSelectData AsComboSelect(this IChoiceListMap input)
		{
			var comboSelect = new ComboSelectData(input.ListItems)
				.WithId(input.Id)
				.WithName(input.Id)
				.WithValidationFrom(input.Validation);
			comboSelect.SelectedValues.AddRange(input.SelectedValues);
			return comboSelect;
		}

		public static CheckBoxListData AsCheckBoxList(this IChoiceListMap input)
		{
			var checkBoxList = new CheckBoxListData(input.ListItems)
				.WithId(input.Id)
				.WithName(input.Id)
				.WithValidationFrom(input.Validation);
			checkBoxList.SelectedValues.AddRange(input.SelectedValues);
			return checkBoxList;
		}
		
		public static RadioButtonListData AsRadioButtons(this IChoiceListMap input)
		{
			var radioButtons = new RadioButtonListData(input.ListItems)
				.WithId(input.Id)
				.WithName(input.Id)
				.WithValidationFrom(input.Validation);
			radioButtons.SelectedValue = input.SelectedValue;
			return radioButtons;
		}

		public static DropDownListData AsDropDownList(this IChoiceListMap input)
		{
			return new DropDownListData(input.ListItems)
				.WithSelectedValue(input.SelectedValue)
				.WithId(input.Id)
				.WithName(input.Id)
				.WithValidationFrom(input.Validation);
		}
	}
}