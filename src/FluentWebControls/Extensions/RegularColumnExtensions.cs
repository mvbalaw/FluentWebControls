namespace FluentWebControls.Extensions
{
	public static class RegularColumnExtensions
	{
		public static RegularColumn<T> AddSorterType<T>(this RegularColumn<T> regularColumn, string sorterType)
		{
			regularColumn.Sorter = sorterType;
			return regularColumn;
		}

		public static RegularColumn<T> AlignCenter<T>(this RegularColumn<T> regularColumn)
		{
			regularColumn.Align = AlignAttribute.Center;
			return regularColumn;
		}

		public static RegularColumn<T> AlignLeft<T>(this RegularColumn<T> regularColumn)
		{
			regularColumn.Align = AlignAttribute.Left;
			return regularColumn;
		}

		public static RegularColumn<T> AlignRight<T>(this RegularColumn<T> regularColumn)
		{
			regularColumn.Align = AlignAttribute.Right;
			return regularColumn;
		}

		public static RegularColumn<T> NotClientSideSortable<T>(this RegularColumn<T> regularColumn)
		{
			regularColumn.IsClientSideSortable = false;
			return regularColumn;
		}
	}
}