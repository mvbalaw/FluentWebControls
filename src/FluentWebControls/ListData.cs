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
using System.Collections.Generic;
using System.Text;

using FluentWebControls.Extensions;

using Microsoft.Build.Framework.XamlTypes;

namespace FluentWebControls
{
	public class ListData<T>
	{
		private readonly List<IListItem<T>> _columns = new List<IListItem<T>>();
		private readonly IEnumerable<T> _items;

		public ListData(IEnumerable<T> items)
		{
			_items = items;
		}

		public string CssClass { get; set; }
		internal string Id { get; set; }
		public string ItemCssClass { get; set; }
        public Func<T, int> GetItemId { get; set; }
		public string ItemDivCssClass { get; set; }
		public string SpanContent { get; set; }
		public string SpanCssClass { get; set; }
		public string SpanId { get; set; }
	    public NameValuePair Data { get; set; }
        public Func<T, string> GetLink { private get; set; }
	    public string ItemIdPrefix { get; set; }


	    public void AddListItem(DataItem<T> dataItem)
		{
			_columns.Add(dataItem);
		}

		public void AddListItem(CommandItem<T> commandItem)
		{
			_columns.Add(commandItem);
		}

		private StringBuilder BeginList()
		{
			var list = new StringBuilder();
			list.Append('<');
			list.Append("ul");
			if (Id != null)
			{
				list.Append(Id.CreateQuotedAttribute("id"));
			}
			if (CssClass != null)
			{
				list.Append(CssClass.Trim().CreateQuotedAttribute("class"));
			}
            if (Data != null && !Data.Name.IsNullOrEmpty())
			{
				list.Append(Data.Value.CreateQuotedAttribute(String.Format("data-{0}",Data.Name)));
			}
			list.Append('>');
			return list;
		}

		private StringBuilder BeginListItem(T item)
		{
			var list = new StringBuilder();
			list.Append('<');
			list.Append("li");
			if (GetItemId != null)
			{
				list.Append(GetListItemId(item).Trim().CreateQuotedAttribute("id"));
			}
            if (ItemCssClass != null)
			{
				list.Append(ItemCssClass.Trim().CreateQuotedAttribute("class"));
			}
		    if (GetLink != null)
		    {
		        list.Append(GetLink(item).CreateQuotedAttribute("data-link"));
		    }
			list.Append('>');
            if (ItemDivCssClass != null)
            {
                list.Append("<div " + ItemDivCssClass.Trim().CreateQuotedAttribute("class") + ">");
            }
			if (SpanCssClass != null)
			{
				list.Append(new SpanData(SpanContent).WithId(SpanId).WithCssClass(SpanCssClass));
			}
			return list;
		}

	    private string GetListItemId(T item)
	    {
	        return String.Format("{0}{1}", ItemIdPrefix, GetItemId(item));
	    }

	    private static StringBuilder EndList()
		{
			var list = new StringBuilder();
			list.Append("</ul>");
			return list;
		}

		private StringBuilder EndListItem()
		{
			var list = new StringBuilder();
		    if (ItemDivCssClass != null)
		    {
                list.Append("</div>");
		    }
		    list.Append("</li>");
			return list;
		}

		public override string ToString()
		{
			var unOrderedList = new StringBuilder();
			unOrderedList.Append(BeginList());
			foreach (var item in _items)
			{
				unOrderedList.Append(BeginListItem(item));
				foreach (var column in _columns)
				{
					unOrderedList.Append(column.Render(item));
				}
				unOrderedList.Append(EndListItem());
			}
			unOrderedList.Append(EndList());
			return unOrderedList.ToString();
		}
	}
}