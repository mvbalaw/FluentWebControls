//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using FluentWebControls.Mapping;

namespace FluentWebControls.Extensions
{
	public static class IChoiceListMapExtensions
	{
		public static CheckBoxListData AsCheckBoxList(this IChoiceListMap input)
		{
			var checkBoxList = new CheckBoxListData(input.ListItems)
				.WithId(input.Id)
				.WithName(input.Id)
				.WithValidationFrom(input.Validation);
			checkBoxList.SelectedValues.AddRange(input.SelectedValues);
			return checkBoxList;
		}

		public static ComboSelectData AsComboSelect(this IChoiceListMap input)
		{
			var comboSelect = new ComboSelectData(input.ListItems)
				.WithId(input.Id)
				.WithName(input.Id)
				.WithValidationFrom(input.Validation);
			comboSelect.SelectedValues.AddRange(input.SelectedValues);
			return comboSelect;
		}

		public static DropDownListData AsDropDownList(this IChoiceListMap input)
		{
			return new DropDownListData(input.ListItems)
				.WithSelectedValue(input.SelectedValue)
				.WithId(input.Id)
				.WithName(input.Id)
				.WithValidationFrom(input.Validation);
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
	}
}