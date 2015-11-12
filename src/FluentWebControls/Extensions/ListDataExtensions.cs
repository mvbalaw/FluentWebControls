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

using Microsoft.Build.Framework.XamlTypes;

namespace FluentWebControls.Extensions
{
	public static class ListDataExtensions
	{
		public static ListData<T> WithClass<T>(this ListData<T> list, string @class)
		{
			list.CssClass = @class;
			return list;
		}

		public static ListData<T> WithId<T>(this ListData<T> list, string id)
		{
			list.Id = id;
			return list;
		}

		public static ListData<T> WithItem<T>(this ListData<T> list, DataItem<T> dataItem)
		{
			list.AddListItem(dataItem);
			return list;
		}

		public static ListData<T> WithItem<T>(this ListData<T> list, CommandItem<T> commandItem)
		{
			list.AddListItem(commandItem);
			return list;
		}

		public static ListData<T> WithItemClass<T>(this ListData<T> list, string @class)
		{
			list.ItemCssClass = @class;
			return list;
		}

        public static ListData<T> WithItemId<T>(this ListData<T> list, Func<T, int> getId, string prefix = "li_")
        {
            list.GetItemId = getId;
            list.ItemIdPrefix = prefix;
			return list;
		}

        public static ListData<T> WrapListItemInDivWithClass<T>(this ListData<T> list, string @class)
		{
			list.ItemDivCssClass = @class;
			return list;
		}

        public static ListData<T> WithData<T>(this ListData<T> list, string name, string value)
		{
            list.Data = new NameValuePair
            {
                Name = name,
                Value = value
            };
			return list;
		}

        public static ListData<T> WithItemLink<T>(this ListData<T> list, Func<T, string> getLink)
        {
            list.GetLink = getLink;
            return list;
        }

		public static ListData<T> WithSpan<T>(this ListData<T> list, string content, string @class)
		{
			return list.WithSpan("", content, @class);
		}

		public static ListData<T> WithSpan<T>(this ListData<T> list, string id, string content, string @class)
		{
			list.SpanId = id;
			list.SpanContent = content;
			list.SpanCssClass = @class;
			return list;
		}
	}
}