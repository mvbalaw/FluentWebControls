using System.Collections.Generic;

namespace FluentWebControls
{
	public abstract class GridColumnBuilder
	{
		protected GridColumnBuilder(string columnHeader, string fieldName)
		{
			FieldName = fieldName;
			ColumnHeader = columnHeader;
			IsClientSideSortable = true;
		}

		public AlignAttribute Align { get; set; }
		public string ColumnHeader { get; private set; }
		public string FieldName { get; private set; }
		public bool IsClientSideSortable { get; set; }
		public bool IsDefaultSortColumn { get; set; }
		public abstract GridColumnType Type { get; }
	}

	public class GridColumn : IGridColumn
	{
		public GridColumn(GridColumnType type,
		                  string columnHeader,
		                  string fieldName,
		                  AlignAttribute align,
		                  bool isDefaultSortColumn,
		                  bool isClientSideSortable,
		                  string actionName,
		                  IList<string> rows)
		{
			Type = type;
			ColumnHeader = columnHeader;
			FieldName = fieldName;
			Align = align;
			IsDefaultSortColumn = isDefaultSortColumn;
			IsClientSideSortable = isClientSideSortable;
			ActionName = actionName;
			Rows = rows;
			Count = rows.Count;
		}

		private IList<string> Rows { get; set; }

		public GridColumnType Type { get; private set; }
		public string ColumnHeader { get; private set; }
		public string FieldName { get; private set; }
		public AlignAttribute Align { get; private set; }
		public string ActionName { get; private set; }
		public int Count { get; private set; }
		public bool IsDefaultSortColumn { get; private set; }
		public bool IsClientSideSortable { get; set; }

		public string this[int rowId]
		{
			get { return Rows[rowId]; }
		}
	}
}