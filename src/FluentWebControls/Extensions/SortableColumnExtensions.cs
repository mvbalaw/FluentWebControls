namespace FluentWebControls.Extensions
{
	public static class SortableColumnExtensions
	{
		public static SortableColumn<T> AlignCenter<T>(this SortableColumn<T> sortableColumn)
		{
			sortableColumn.Align = AlignAttribute.Center;
			return sortableColumn;
		}

		public static SortableColumn<T> AlignLeft<T>(this SortableColumn<T> sortableColumn)
		{
			sortableColumn.Align = AlignAttribute.Left;
			return sortableColumn;
		}

		public static SortableColumn<T> AlignRight<T>(this SortableColumn<T> sortableColumn)
		{
			sortableColumn.Align = AlignAttribute.Right;
			return sortableColumn;
		}

		public static SortableColumn<T> IsDefaultSortColumn<T>(this SortableColumn<T> sortableColumn)
		{
			sortableColumn.IsDefaultSortColumn = true;
			return sortableColumn;
		}

		public static SortableColumn<T> NotClientSideSortable<T>(this SortableColumn<T> sortableColumn)
		{
			sortableColumn.IsClientSideSortable = false;
			return sortableColumn;
		}
	}
}