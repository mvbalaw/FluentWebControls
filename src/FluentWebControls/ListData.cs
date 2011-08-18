using System.Collections.Generic;
using System.Text;

using FluentWebControls.Extensions;

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
		public string SpanCssClass { get; set; }
		public string SpanContent { get; set; }
		public string SpanId { get; set; }

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
			list.Append("<");
			list.Append("ul");
			if (Id != null)
			{
				list.Append(Id.CreateQuotedAttribute("id"));
			}

			if (CssClass != null)
			{
				list.Append(CssClass.Trim().CreateQuotedAttribute("class"));
			}
			list.Append(">");
			return list;
		}

		private StringBuilder BeginListItem()
		{
			var list = new StringBuilder();
			list.Append("<");
			list.Append("li");
			if (ItemCssClass != null)
			{
				list.Append(ItemCssClass.Trim().CreateQuotedAttribute("class"));
			}
			list.Append(">");
			if (SpanCssClass != null)
			{
				list.Append(new SpanData(SpanContent).WithId(SpanId).WithCssClass(SpanCssClass).ToString());
			}
			return list;
		}

		private static StringBuilder EndList()
		{
			var list = new StringBuilder();
			list.Append("</ul>");
			return list;
		}

		private static StringBuilder EndListItem()
		{
			var list = new StringBuilder();
			list.Append("</li>");
			return list;
		}

		public override string ToString()
		{
			var unOrderedList = new StringBuilder();
			unOrderedList.Append(BeginList());
			foreach (var item in _items)
			{
				unOrderedList.Append(BeginListItem());
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