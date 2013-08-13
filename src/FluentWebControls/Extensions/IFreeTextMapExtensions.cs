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
	public static class IFreeTextMapExtensions
	{
		public static HiddenData AsHidden(this IFreeTextMap input)
		{
			return new HiddenData()
				.WithValue(input.Value)
				.WithId(input.Id)
				.WithName(input.Id);
		}

		public static SpanData AsSpan(this IFreeTextMap input)
		{
			return new SpanData(input.Value)
				.WithId(input.Id)
				.WithName(input.Id);
		}

		public static TextAreaData AsTextArea(this IFreeTextMap input)
		{
			return new TextAreaData(input.Value)
				.WithId(input.Id)
				.WithName(input.Id)
				.WithValidationFrom(input.Validation);
		}

		public static TextBoxData AsTextBox(this IFreeTextMap input)
		{
			return new TextBoxData(input.Value)
				.WithId(input.Id)
				.WithName(input.Id)
				.WithValidationFrom(input.Validation);
		}
	}
}