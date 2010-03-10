namespace FluentWebControls.Extensions
{
	public static class GridCommandColumnExtensions
	{
		public static GridCommandColumn<T> AlignCenter<T>(this GridCommandColumn<T> sortableColumn)
		{
			sortableColumn.Align = AlignAttribute.Center;
			return sortableColumn;
		}

		public static GridCommandColumn<T> AlignLeft<T>(this GridCommandColumn<T> sortableColumn)
		{
			sortableColumn.Align = AlignAttribute.Left;
			return sortableColumn;
		}

		public static GridCommandColumn<T> AlignRight<T>(this GridCommandColumn<T> sortableColumn)
		{
			sortableColumn.Align = AlignAttribute.Right;
			return sortableColumn;
		}
	}
}