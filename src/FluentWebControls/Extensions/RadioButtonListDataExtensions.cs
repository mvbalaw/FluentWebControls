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
	public static class RadioButtonListDataExtensions
	{
		public static RadioButtonListData WithCssClass(this RadioButtonListData radioButtonListData, string cssClass)
		{
			radioButtonListData.CssClass = cssClass;
			return radioButtonListData;
		}

		public static RadioButtonListData WithLabel(this RadioButtonListData radioButtonListData, string labelText)
		{
			var label = Label.ForIt().WithText(labelText);
			return radioButtonListData.WithLabel(label);
		}

		public static RadioButtonListData WithLabel(this RadioButtonListData radioButtonListData, LabelData label)
		{
			radioButtonListData.Label = label;
			return radioButtonListData;
		}

		public static RadioButtonListData WithoutItemSeparator(this RadioButtonListData radioButtonListData)
		{
			radioButtonListData.UseItemSeparator = false;
			return radioButtonListData;
		}
	}
}