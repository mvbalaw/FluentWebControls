//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

namespace FluentWebControls.Extensions
{
	public static class RadioButtonDataExtensions
	{
		public static RadioButtonData IsChecked(this RadioButtonData radioButtonData, bool @checked)
		{
			radioButtonData.Checked = @checked;
			return radioButtonData;
		}

		public static RadioButtonData WithCssClass(this RadioButtonData radioButtonData, string cssClass)
		{
			radioButtonData.CssClass = cssClass;
			return radioButtonData;
		}

		public static RadioButtonData WithLabel(this RadioButtonData radioButtonData, string labelText)
		{
			var label = Label.ForIt().WithText(labelText);
			return radioButtonData.WithLabel(label);
		}

		public static RadioButtonData WithLabel(this RadioButtonData radioButtonData, LabelData label)
		{
			radioButtonData.Label = label;
			return radioButtonData;
		}

		public static RadioButtonData WithLabelAlignedLeft(this RadioButtonData radioButtonData, LabelData label)
		{
			radioButtonData.LabelAlignAttribute = AlignAttribute.Left;
			return radioButtonData.WithLabel(label);
		}

		public static RadioButtonData WithLabelAlignedLeft(this RadioButtonData radioButtonData, string labelText)
		{
			radioButtonData.LabelAlignAttribute = AlignAttribute.Left;
			return radioButtonData.WithLabel(labelText);
		}

		public static RadioButtonData WithTabIndex(this RadioButtonData checkBoxData, string tabIndex)
		{
			checkBoxData.TabIndex = tabIndex;
			return checkBoxData;
		}

		public static RadioButtonData WithValue(this RadioButtonData radioButtonData, string value)
		{
			radioButtonData.Value = value;
			return radioButtonData;
		}
	}
}