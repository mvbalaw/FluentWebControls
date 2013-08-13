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
	public static class CommandColumnExtensions
	{
		public static CommandColumn<T> AlignLeft<T>(this CommandColumn<T> commandColumn)
		{
			commandColumn.Align = AlignAttribute.Left;
			return commandColumn;
		}

		public static CommandColumn<T> AlignRight<T>(this CommandColumn<T> commandColumn)
		{
			commandColumn.Align = AlignAttribute.Right;
			return commandColumn;
		}

		public static CommandColumn<T> WithCssClass<T>(this CommandColumn<T> commandColumn, string cssClass)
		{
			commandColumn.CssClass = cssClass;
			return commandColumn;
		}

		public static CommandColumn<T> WithHeaderCssClass<T>(this CommandColumn<T> commandColumn, string cssClass)
		{
			commandColumn.HeaderCssClass = cssClass;
			return commandColumn;
		}

		public static CommandColumn<T> WithImage<T>(this CommandColumn<T> commandColumn, string imageUrl)
		{
			return commandColumn.WithImage(imageUrl, "");
		}

		public static CommandColumn<T> WithImage<T>(this CommandColumn<T> commandColumn, string imageUrl, string alt)
		{
			commandColumn.ImageUrl = imageUrl;
			commandColumn.Alt = alt;
			return commandColumn;
		}

		public static CommandColumn<T> WithText<T>(this CommandColumn<T> commandColumn, string text)
		{
			commandColumn.Text = text;
			return commandColumn;
		}
	}
}