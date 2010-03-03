namespace FluentWebControls.Extensions
{
	public static class CommandColumnExtensions
	{
		public static CommandColumn<T> AlignCenter<T>(this CommandColumn<T> sortableColumn)
		{
			sortableColumn.Align = AlignAttribute.Center;
			return sortableColumn;
		}

		public static CommandColumn<T> AlignLeft<T>(this CommandColumn<T> sortableColumn)
		{
			sortableColumn.Align = AlignAttribute.Left;
			return sortableColumn;
		}

		public static CommandColumn<T> AlignRight<T>(this CommandColumn<T> sortableColumn)
		{
			sortableColumn.Align = AlignAttribute.Right;
			return sortableColumn;
		}
	}
}