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
	public static class TExtensions
	{
		public static string CreateQuotedAttribute<T>(this T? value, string name) where T : struct
		{
			var v = value == null ? "" : value.ToString();
			return v.CreateQuotedAttribute(name);
		}

		public static string CreateQuotedAttribute<T>(this T value, string name) where T : struct
		{
			var v = value.ToString();
			return v.CreateQuotedAttribute(name);
		}

		public static string EscapeForTagAttribute<T>(this T? value) where T : struct
		{
			var v = value.ToString();
			return value == null ? "" : v.EscapeForTagAttribute();
		}

		public static string EscapeForTagAttribute<T>(this T value) where T : struct
		{
			var v = value.ToString();
			return v.EscapeForTagAttribute();
		}
	}
}