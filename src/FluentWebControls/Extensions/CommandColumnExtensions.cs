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

		public static CommandColumn<T> WithText<T>(this CommandColumn<T> commandColumn, string text)
		{
			commandColumn.Text = text;
			return commandColumn;
		}
	}
}