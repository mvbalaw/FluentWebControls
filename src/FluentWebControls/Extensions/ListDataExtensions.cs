namespace FluentWebControls.Extensions
{
	public static class ListDataExtensions
	{
		public static ListData<T> WithClass<T>(this ListData<T> list, string @class)
		{
			list.CssClass = @class;
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

		public static ListData<T> WithItemClass<T>(this ListData<T> list, string @class)
		{
			list.ItemCssClass = @class;
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
	}
}