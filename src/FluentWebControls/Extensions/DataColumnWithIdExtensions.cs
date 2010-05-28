namespace FluentWebControls.Extensions
{
	public static class DataColumnWithIdExtensions
	{
		public static DataColumnWithId<T> AlignCenter<T>(this DataColumnWithId<T> dataColumn)
		{
			dataColumn.Align = AlignAttribute.Center;
			return dataColumn;
		}

		public static DataColumnWithId<T> AlignHeaderLeft<T>(this DataColumnWithId<T> dataColumn)
		{
			dataColumn.HeaderAlign = AlignAttribute.Left;
			return dataColumn;
		}

		public static DataColumnWithId<T> AlignHeaderRight<T>(this DataColumnWithId<T> dataColumn)
		{
			dataColumn.HeaderAlign = AlignAttribute.Right;
			return dataColumn;
		}

		public static DataColumnWithId<T> AlignRight<T>(this DataColumnWithId<T> dataColumn)
		{
			dataColumn.Align = AlignAttribute.Right;
			return dataColumn;
		}

		public static DataColumnWithId<T> AsTextBox<T>(this DataColumnWithId<T> dataColumn)
		{
			dataColumn.ColumnTextType = ColumnTextType.TextBox;
			return dataColumn;
		}

		public static DataColumnWithId<T> WithHeader<T>(this DataColumnWithId<T> dataColumn, string header)
		{
			dataColumn.HeaderText = header;
			return dataColumn;
		}

		public static DataColumnWithId<T> WithHeaderCssClass<T>(this DataColumnWithId<T> dataColumn, string cssClass)
		{
			dataColumn.HeaderCssClass = cssClass;
			return dataColumn;
		}

		public static DataColumnWithId<T> WithInputElementCssClass<T>(this DataColumnWithId<T> dataColumn, string cssClass)
		{
			dataColumn.InputElementCssClass = cssClass;
			return dataColumn;
		}

		public static DataColumnWithId<T> WithCssClass<T>(this DataColumnWithId<T> dataColumn, string cssClass)
		{
			dataColumn.CssClass = cssClass;
			return dataColumn;
		}
	}
}