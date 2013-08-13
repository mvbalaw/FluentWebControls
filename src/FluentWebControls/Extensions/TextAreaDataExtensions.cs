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
	public static class TextAreaDataExtensions
	{
		public static TextAreaData Cols(this TextAreaData textAreaData, int cols)
		{
			textAreaData.Cols = cols;
			return textAreaData;
		}

		public static TextAreaData CssClass(this TextAreaData textAreaData, string cssClass)
		{
			textAreaData.CssClass = cssClass;
			return textAreaData;
		}

		public static TextAreaData Rows(this TextAreaData textAreaData, int rows)
		{
			textAreaData.Rows = rows;
			return textAreaData;
		}

		public static TextAreaData Width(this TextAreaData textAreaData, string width)
		{
			textAreaData.Width = width;
			return textAreaData;
		}

		public static TextAreaData WithLabel(this TextAreaData textAreaData, string labelText)
		{
			return textAreaData.WithLabel(Label.ForIt().WithText(labelText));
		}

		public static TextAreaData WithLabel(this TextAreaData textAreaData, LabelData label)
		{
			textAreaData.Label = label;
			return textAreaData;
		}

		public static TextAreaData WithTabIndex(this TextAreaData textAreaData, string tabIndex)
		{
			textAreaData.TabIndex = tabIndex;
			return textAreaData;
		}
	}
}