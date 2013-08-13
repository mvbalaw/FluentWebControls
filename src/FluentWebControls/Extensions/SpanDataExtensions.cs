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
	public static class SpanDataExtensions
	{
		public static SpanData WithCssClass(this SpanData spanData, string cssClass)
		{
			spanData.CssClass = cssClass;
			return spanData;
		}

		public static SpanData WithLabel(this SpanData spanData, string labelText)
		{
			return spanData.WithLabel(Label.ForIt().WithText(labelText));
		}

		public static SpanData WithLabel(this SpanData spanData, LabelData label)
		{
			spanData.Label = label;
			return spanData;
		}
	}
}