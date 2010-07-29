using System;

namespace FluentWebControls.Extensions
{
	public static class DataItemExtensions
	{
		public static DataItem<T> AlignCenter<T>(this DataItem<T> dataItem)
		{
			dataItem.Align = AlignAttribute.Center;
			return dataItem;
		}

		public static DataItem<T> AlignRight<T>(this DataItem<T> dataItem)
		{
			dataItem.Align = AlignAttribute.Right;
			return dataItem;
		}

		public static DataItem<T> AsHidden<T>(this DataItem<T> dataItem, Func<T, string> forId)
		{
			dataItem.ColumnTextType = ColumnTextType.Hidden;
			dataItem.GetItemId = forId;
			return dataItem;
		}

		public static DataItem<T> AsHidden<T>(this DataItem<T> dataItem, string forId)
		{
			dataItem.ColumnTextType = ColumnTextType.Hidden;
			dataItem.InputTextId = forId;
			return dataItem;
		}

		public static DataItem<T> AsSpan<T>(this DataItem<T> dataItem, Func<T, string> forId)
		{
			dataItem.ColumnTextType = ColumnTextType.Span;
			dataItem.GetItemId = forId;
			return dataItem;
		}

		public static DataItem<T> AsTextBox<T>(this DataItem<T> dataItem, Func<T, string> forId)
		{
			dataItem.ColumnTextType = ColumnTextType.TextBox;
			dataItem.GetItemId = forId;
			return dataItem;
		}

		public static DataItem<T> WithInputCssClass<T>(this DataItem<T> dataItem, string inputCssClass)
		{
			dataItem.InputCssClass = inputCssClass;
			return dataItem;
		}
	}
}