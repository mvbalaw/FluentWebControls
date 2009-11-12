using System;
using System.Collections.Generic;
using System.Linq;

using FluentWebControls.Interfaces;

namespace FluentWebControls
{
	public class GridData
	{
		public GridData(IPagedListParameters pagedListParameters, string controllerName, string controllerExtension, string actionName, IEnumerable<IGridColumn> gridColumns, int total, IEnumerable<DropDownListData> filters, int rowCount)
		{
			Filters = filters;
			PagedListParameters = pagedListParameters;
			ControllerName = controllerName;
			ControllerExtension = controllerExtension;
			ActionName = actionName;
			GridColumns = gridColumns;
			Total = total;
			RowCount = rowCount;
		}

		public string ActionName { get; private set; }
		public string ControllerExtension { get; private set; }
		public string ControllerName { get; private set; }
		public IEnumerable<DropDownListData> Filters { get; private set; }
		public IEnumerable<IGridColumn> GridColumns { get; private set; }
		public IPagedListParameters PagedListParameters { get; private set; }
		public int RowCount { get; private set; }
		public int Total { get; private set; }
	}

	public class GridData<T>
	{
		private readonly List<DropDownListData> _filters = new List<DropDownListData>();
		private readonly List<IGridColumn> _gridColumns = new List<IGridColumn>();
		private readonly IEnumerable<T> _items;

		public GridData(IPagedListParameters pagedListParameters, string controllerName, string actionName, IEnumerable<T> items, int total)
		{
			_items = items;
			Total = total;
			PagedListParameters = pagedListParameters;
			ControllerName = controllerName;
			ActionName = actionName;
		}

		public string ActionName { get; private set; }
		public string ControllerExtension { get; set; }
		public string ControllerName { get; private set; }

		public IEnumerable<IGridColumn> GridColumns
		{
			get { return _gridColumns; }
		}

		public IPagedListParameters PagedListParameters { get; set; }
		public int Total { get; set; }

		public void AddColumn(SortableColumn<T> column)
		{
			AddColumn(column, ActionName, column.GetItemValue);
		}

		public void AddColumn(RegularColumn<T> column)
		{
			AddColumn(column, ActionName, column.GetItemValue);
		}

		public void AddColumn(CommandColumn<T> column)
		{
			AddColumn(column, column.ActionName, column.GetItemId);
		}

		private void AddColumn(GridColumnBuilder column, string actionName, Func<T, string> getItemValue)
		{
			_gridColumns.Add(
				new GridColumn(column.Type,
				               column.ColumnHeader,
				               column.FieldName,
				               column.Align,
				               column.IsDefaultSortColumn,
				               column.IsClientSideSortable,
				               actionName,
				               _items.Select(getItemValue).ToList())
				);
		}

		public void AddFilter(DropDownListData filter)
		{
			_filters.Add(filter);
		}

		public GridData ToModel()
		{
			return new GridData(PagedListParameters, ControllerName, ControllerExtension, ActionName, _gridColumns, Total, _filters, _items.Count());
		}
	}
}