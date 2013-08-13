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
	public static class TextBoxDataExtensions
	{
		public static TextBoxData AsReadOnly(this TextBoxData textBoxData)
		{
			textBoxData.ReadOnly = true;
			return textBoxData;
		}

		public static TextBoxData CssClass(this TextBoxData textBoxData, string cssClass)
		{
			textBoxData.CssClass = cssClass;
			return textBoxData;
		}

		public static TextBoxData MaxValue(this TextBoxData textBoxData, int maxValue)
		{
			textBoxData.MaxValue = maxValue;
			return textBoxData;
		}

		public static TextBoxData MinValue(this TextBoxData textBoxData, int minValue)
		{
			textBoxData.MinValue = minValue;
			return textBoxData;
		}

		public static TextBoxData Width(this TextBoxData textBoxData, string width)
		{
			textBoxData.Width = width;
			return textBoxData;
		}

		public static TextBoxData WithLabel(this TextBoxData textBoxData, string labelText)
		{
			var label = Label.ForIt().WithText(labelText);
			return textBoxData.WithLabel(label);
		}

		public static TextBoxData WithLabel(this TextBoxData textBoxData, LabelData label)
		{
			textBoxData.Label = label;
			return textBoxData;
		}

		public static TextBoxData WithTabIndex(this TextBoxData textBoxData, string tabIndex)
		{
			textBoxData.TabIndex = tabIndex;
			return textBoxData;
		}
	}
}