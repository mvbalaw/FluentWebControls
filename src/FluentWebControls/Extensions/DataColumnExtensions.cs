using System;

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

		public static DataColumn<T> AsHidden<T>(this DataColumn<T> dataColumn, Func<T, string> forId)
		{
			dataColumn.ColumnTextType = ColumnTextType.Hidden;
			dataColumn.GetItemId = forId;
			return dataColumn;
		}

		public static DataColumn<T> AsHidden<T>(this DataColumn<T> dataColumn, string forId)
		{
			dataColumn.ColumnTextType = ColumnTextType.Hidden;
			dataColumn.InputTextId = forId;
			return dataColumn;
		}

		public static DataColumn<T> AsSpan<T>(this DataColumn<T> dataColumn, Func<T, string> forId)
		{
			dataColumn.ColumnTextType = ColumnTextType.Span;
			dataColumn.GetItemId = forId;
			return dataColumn;
		}

		public static DataColumn<T> AsTextBox<T>(this DataColumn<T> dataColumn, Func<T, string> forId)
		{
			dataColumn.ColumnTextType = ColumnTextType.TextBox;
			dataColumn.GetItemId = forId;
			return dataColumn;
		}

		public static DataColumn<T> AsTextBox<T>(this DataColumn<T> dataColumn, string forId)
		{
			dataColumn.ColumnTextType = ColumnTextType.TextBox;
			dataColumn.InputTextId = forId;
			return dataColumn;
		}

		public static DataColumn<T> WithCssClass<T>(this DataColumn<T> dataColumn, string cssClass)
		{
			dataColumn.CssClass = cssClass;
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

		public static DataColumn<T> WithInputCssClass<T>(this DataColumn<T> dataColumn, string inputCssClass)
		{
			dataColumn.InputCssClass = inputCssClass;
			return dataColumn;
		}

		public static DataColumn<T> WithPrefix<T>(this DataColumn<T> dataColumn, string prefix)
		{
			dataColumn.Prefix = prefix;
			return dataColumn;
		}
	}
}