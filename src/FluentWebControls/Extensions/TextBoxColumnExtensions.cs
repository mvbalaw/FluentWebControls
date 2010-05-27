namespace FluentWebControls.Extensions
{
	public static class TextBoxColumnExtensions
	{
		public static TextBoxColumn<T> AlignCenter<T>(this TextBoxColumn<T> dataColumn)
		{
			dataColumn.Align = AlignAttribute.Center;
			return dataColumn;
		}

		public static TextBoxColumn<T> AlignHeaderLeft<T>(this TextBoxColumn<T> dataColumn)
		{
			dataColumn.HeaderAlign = AlignAttribute.Left;
			return dataColumn;
		}

		public static TextBoxColumn<T> AlignHeaderRight<T>(this TextBoxColumn<T> dataColumn)
		{
			dataColumn.HeaderAlign = AlignAttribute.Right;
			return dataColumn;
		}

		public static TextBoxColumn<T> AlignRight<T>(this TextBoxColumn<T> dataColumn)
		{
			dataColumn.Align = AlignAttribute.Right;
			return dataColumn;
		}

		public static TextBoxColumn<T> WithCssClass<T>(this TextBoxColumn<T> dataColumn, string cssClass)
		{
			dataColumn.CssClass = cssClass;
			return dataColumn;
		}

		public static TextBoxColumn<T> WithHeader<T>(this TextBoxColumn<T> dataColumn, string header)
		{
			dataColumn.HeaderText = header;
			return dataColumn;
		}

		public static TextBoxColumn<T> WithHeaderCssClass<T>(this TextBoxColumn<T> dataColumn, string cssClass)
		{
			dataColumn.HeaderCssClass = cssClass;
			return dataColumn;
		}

		public static TextBoxColumn<T> WithTextBoxCssClass<T>(this TextBoxColumn<T> dataColumn, string cssClass)
		{
			dataColumn.TextBoxCssClass = cssClass;
			return dataColumn;
		}
	}
}