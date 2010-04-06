using System.Web.UI.WebControls;

namespace FluentWebControls.Extensions
{
	public static class TableDataExtensions
	{
		public static TableData<T> WithBorderWidth<T>(this TableData<T> table, Unit border)
		{
			table.BorderWidth = border;
			return table;
		}

		public static TableData<T> WithCellSpacing<T>(this TableData<T> table, int cellSpacing)
		{
			table.CellSpacing = cellSpacing;
			return table;
		}

		public static TableData<T> WithClass<T>(this TableData<T> table, string @class)
		{
			table.CssClass = @class;
			return table;
		}

		public static TableData<T> WithColumn<T>(this TableData<T> table, DataColumn<T> dataColumn)
		{
			table.AddColumn(dataColumn);
			return table;
		}

		public static TableData<T> WithColumn<T>(this TableData<T> table, CommandColumn<T> commandColumn)
		{
			table.AddColumn(commandColumn);
			return table;
		}

		public static TableData<T> WithId<T>(this TableData<T> table, string id)
		{
			table.Id = id;
			return table;
		}

		public static TableData<T> WithGridLines<T>(this TableData<T> table, GridLines gridLines)
		{
			table.GridLines = gridLines;
			return table;
		}

		public static TableData<T> WithStyle<T>(this TableData<T> table, string styleName, string styleValue)
		{
			table.Style.Add(styleName, styleValue);
			return table;
		}
	}
}