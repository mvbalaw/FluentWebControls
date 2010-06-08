using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentWebControls.Extensions;
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
		protected virtual bool ClientSideSortingEnabled
		{
			get { return false; }
		}
		public string ControllerExtension { get; set; }
		public string ControllerName { get; private set; }
		protected IEnumerable<DropDownListData> Filters
		{
			get { return _filters; }
		}

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

		public void AddColumn(GridCommandColumn<T> column)
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
				               column.Sorter,
				               actionName,
				               _items.Select(getItemValue).ToList())
				);
		}

		public void AddFilter(DropDownListData filter)
		{
			_filters.Add(filter);
		}

		protected string BuildFilters()
		{
			var sb = new StringBuilder();
			if (_filters.Any())
			{
				sb.Append("<table width='700px'><tr>");
				foreach (var filter in _filters)
				{
					sb.Append("<td align='center'>");
					sb.Append(filter);
					sb.Append("</td>");
				}
				sb.Append("</tr></table>");
			}
			return sb.ToString();
		}

		protected string BuildHeaderColumns()
		{
			var sb = new StringBuilder();
			int columnNumber = 0;
			foreach (var column in GridColumns)
			{
				sb.AppendFormat("<th{0}{1}", AlignAttribute.Center, ClientSideSortingEnabled && !column.IsClientSideSortable ? " class=\"{sorter: false}\"" : "");
				string columnSorter = !column.Sorter.IsNullOrEmpty(true) ? " class=\"{sorter: '" + column.Sorter + "'}\"" : "";
				sb.AppendFormat("{0}>", columnSorter);
				switch (column.Type)
				{
					case GridColumnType.Sortable:
						sb.Append(Link
						          	.To(ControllerName, ControllerExtension, ActionName)
						          	.WithLinkText(column.ColumnHeader)
						          	.WithData(() => PagedListParameters.SortDirection, GetNextSortDirection(column.FieldName, column.IsDefaultSortColumn))
						          	.WithData(() => PagedListParameters.SortField, column.FieldName)
						          	.WithData(_filters.Select(f => new KeyValuePair<string, string>(((IWebControl)f).Id, ((IDropDownListData)f).SelectedValue)))
						          	.WithId("th" + columnNumber)
							);
						break;
					case GridColumnType.Command:
						sb.Append("&nbsp;");
						break;
					case GridColumnType.Regular:
						sb.Append(column.ColumnHeader);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
				sb.AppendLine("</th>");
				columnNumber++;
			}
			return sb.ToString();
		}

		private void BuildRow(int rowId, StringBuilder sb)
		{
			sb.Append("<tr>");
			foreach (var column in GridColumns)
			{
				sb.AppendFormat("<td style='white-space: nowrap'{0}>", column.Align);
				switch (column.Type)
				{
					case GridColumnType.Regular:
					case GridColumnType.Sortable:
						sb.Append(column[rowId]);
						break;
					case GridColumnType.Command:
						sb.Append(Link
						          	.To(ControllerName, ControllerExtension, column.ActionName)
						          	.WithLinkText(column.ActionName)
						          	.WithData(column.FieldName, column[rowId]));
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
				sb.Append("</td>");
			}
			sb.AppendLine("</tr>");
		}

		protected string BuildRows()
		{
			var sb = new StringBuilder();

			int count = _items.Count();
			foreach (int rowId in Enumerable.Range(0, count))
			{
				BuildRow(rowId, sb);
			}
			return sb.ToString();
		}

		private string GetNextSortDirection(string fieldName, bool isDefaultSortColumn)
		{
			if (PagedListParameters.SortField == fieldName)
			{
				return String.Compare(PagedListParameters.SortDirection, "Asc", true) == 0 ? "Desc" : "Asc";
			}
			return isDefaultSortColumn ? "Desc" : "Asc";
		}
	}
}