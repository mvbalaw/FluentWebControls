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
	public static class HiddenDataExtensions
	{
		[Obsolete("use .WithValue(foo)")]
		public static HiddenData Text(this HiddenData hiddenData, string text)
		{
			return hiddenData.WithValue(text);
		}

		public static HiddenData WithValue(this HiddenData hiddenData, string value)
		{
			hiddenData.Value = value;
			return hiddenData;
		}
	}
}