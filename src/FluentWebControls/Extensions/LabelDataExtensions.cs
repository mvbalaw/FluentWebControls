//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using System;

namespace FluentWebControls.Extensions
{
	public static class LabelDataExtensions
	{
		public static LabelData Style(this LabelData labelData, string style)
		{
			labelData.Style = style;
			return labelData;
		}

		public static LabelData Width(this LabelData labelData, string width)
		{
			labelData.Width = width;
			return labelData;
		}

		public static LabelData WithData<T>(this LabelData labelData, T item, Func<T, string> getValue)
		{
			labelData.Value = getValue(item);
			return labelData;
		}

		public static LabelData WithText(this LabelData labelData, string text)
		{
			labelData.Text = text;
			return labelData;
		}
	}
}