//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

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
		public string ControllerExtension { get; set; }
		public string FieldName { get; private set; }
		public bool IsClientSideSortable { get; set; }
		public bool IsDefaultSortColumn { get; set; }
		public string Sorter { get; set; }
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
			string sorter,
			string actionName,
			IList<string> rows)
		{
			Type = type;
			ColumnHeader = columnHeader;
			FieldName = fieldName;
			Align = align;
			IsDefaultSortColumn = isDefaultSortColumn;
			IsClientSideSortable = isClientSideSortable;
			Sorter = sorter;
			ActionName = actionName;
			Rows = rows;
			Count = rows.Count;
		}

		private IList<string> Rows { get; set; }
		public string Sorter { get; set; }

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