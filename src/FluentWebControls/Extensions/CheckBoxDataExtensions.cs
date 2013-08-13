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
	public static class CheckBoxDataExtensions
	{
		public static CheckBoxData IsChecked(this CheckBoxData checkBoxData, bool @checked)
		{
			checkBoxData.Checked = @checked;
			return checkBoxData;
		}

		public static CheckBoxData WithClass(this CheckBoxData checkBoxData, string cssClass)
		{
			checkBoxData.CssClass = cssClass;
			return checkBoxData;
		}

		public static CheckBoxData WithCssClass(this CheckBoxData checkBoxData, string cssClass)
		{
			checkBoxData.CssClass = cssClass;
			return checkBoxData;
		}

		public static CheckBoxData WithLabel(this CheckBoxData checkBoxData, string labelText)
		{
			var label = Label.ForIt().WithText(labelText);
			return checkBoxData.WithLabel(label);
		}

		public static CheckBoxData WithLabel(this CheckBoxData checkBoxData, LabelData label)
		{
			checkBoxData.Label = label;
			return checkBoxData;
		}

		public static CheckBoxData WithLabelAlignedLeft(this CheckBoxData checkBoxData, LabelData label)
		{
			checkBoxData.LabelAlignAttribute = AlignAttribute.Left;
			return checkBoxData.WithLabel(label);
		}

		public static CheckBoxData WithLabelAlignedLeft(this CheckBoxData checkBoxData, string labelText)
		{
			checkBoxData.LabelAlignAttribute = AlignAttribute.Left;
			return checkBoxData.WithLabel(labelText);
		}

		public static CheckBoxData WithTabIndex(this CheckBoxData checkBoxData, string tabIndex)
		{
			checkBoxData.TabIndex = tabIndex;
			return checkBoxData;
		}

		public static CheckBoxData WithValue(this CheckBoxData checkBoxData, string value)
		{
			checkBoxData.Value = value;
			return checkBoxData;
		}
	}
}