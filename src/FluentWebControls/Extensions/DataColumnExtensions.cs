namespace FluentWebControls.Extensions
{
	public static class DataColumnExtensions
	{
		public static DataColumn<T> AlignCenter<T>(this DataColumn<T> dataColumn)
		{
			dataColumn.Align = AlignAttribute.Center;
			return dataColumn;
		}

		public static DataColumn<T> AlignHeaderLeft<T>(this DataColumn<T> dataColumn)
		{
			dataColumn.HeaderAlign = AlignAttribute.Left;
			return dataColumn;
		}

		public static DataColumn<T> AlignHeaderRight<T>(this DataColumn<T> dataColumn)
		{
			dataColumn.HeaderAlign = AlignAttribute.Right;
			return dataColumn;
		}

		public static DataColumn<T> AlignRight<T>(this DataColumn<T> dataColumn)
		{
			dataColumn.Align = AlignAttribute.Right;
			return dataColumn;
		}

		public static DataColumn<T> WithHeader<T>(this DataColumn<T> dataColumn, string header)
		{
			dataColumn.HeaderText = header;
			return dataColumn;
		}

		public static DataColumn<T> WithHeaderCssClass<T>(this DataColumn<T> dataColumn, string cssClass)
		{
			dataColumn.HeaderCssClass = cssClass;
			return dataColumn;
		}

		public static DataColumn<T> WithCssClass<T>(this DataColumn<T> dataColumn, string cssClass)
		{
			dataColumn.CssClass = cssClass;
			return dataColumn;
		}
	}
}