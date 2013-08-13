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
	public static class CommandItemExtensions
	{
		public static CommandItem<T> AlignLeft<T>(this CommandItem<T> commandItem)
		{
			commandItem.Align = AlignAttribute.Left;
			return commandItem;
		}

		public static CommandItem<T> AlignRight<T>(this CommandItem<T> commandItem)
		{
			commandItem.Align = AlignAttribute.Right;
			return commandItem;
		}

		public static CommandItem<T> WithCssClass<T>(this CommandItem<T> commandItem, string cssClass)
		{
			commandItem.CssClass = cssClass;
			return commandItem;
		}

		public static CommandItem<T> WithImage<T>(this CommandItem<T> commandItem, string imageUrl)
		{
			return commandItem.WithImage(imageUrl, "");
		}

		public static CommandItem<T> WithImage<T>(this CommandItem<T> commandItem, string imageUrl, string alt)
		{
			commandItem.ImageUrl = imageUrl;
			commandItem.Alt = alt;
			return commandItem;
		}

		public static CommandItem<T> WithText<T>(this CommandItem<T> commandItem, string text)
		{
			commandItem.Text = text;
			return commandItem;
		}

		public static CommandItem<T> WrapWithSpan<T>(this CommandItem<T> commandItem)
		{
			commandItem.WrapWithSpan = true;
			return commandItem;
		}
	}
}