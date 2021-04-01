//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

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

		public string ActionName { get; }
		public string ControllerExtension { get; }
		public string ControllerName { get; }
		public IEnumerable<DropDownListData> Filters { get; }
		public IEnumerable<IGridColumn> GridColumns { get; }
		public IPagedListParameters PagedListParameters { get; }
		public int RowCount { get; }
		public int Total { get; }
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

		public string ActionName { get; }

		protected virtual bool ClientSideSortingEnabled => false;

		public string ControllerExtension { get; set; }
		public string ControllerName { get; }

		protected IEnumerable<DropDownListData> Filters => _filters;

		public IEnumerable<IGridColumn> GridColumns => _gridColumns;

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
			var columnNumber = 0;
			foreach (var column in GridColumns)
			{
				sb.AppendFormat("<th{0}{1}", AlignAttribute.Center, ClientSideSortingEnabled && !column.IsClientSideSortable ? " class=\"{sorter: false}\"" : "");
				var columnSorter = !column.Sorter.IsNullOrEmpty(true) ? " class=\"{sorter: '" + column.Sorter + "'}\"" : "";
				sb.AppendFormat("{0}>", columnSorter);
				switch (column.Type)
				{
					case GridColumnType.Sortable:
						sb.Append(Link
							.To(ControllerName, ControllerExtension, ActionName)
							.WithLinkText(column.ColumnHeader)
							.WithQueryStringData(() => PagedListParameters.SortDirection, GetNextSortDirection(column.FieldName, column.IsDefaultSortColumn))
							.WithQueryStringData(() => PagedListParameters.SortField, column.FieldName)
							.WithQueryStringData(_filters.Select(f => new KeyValuePair<string, string>(((IWebControl)f).Id, ((IDropDownListData)f).SelectedValue)))
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
							.WithQueryStringData(column.FieldName, column[rowId]));
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

			var count = _items.Count();
			foreach (var rowId in Enumerable.Range(0, count))
			{
				BuildRow(rowId, sb);
			}
			return sb.ToString();
		}

		private string GetNextSortDirection(string fieldName, bool isDefaultSortColumn)
		{
			if (PagedListParameters.SortField == fieldName)
			{
				return string.Compare(PagedListParameters.SortDirection, "Asc", true) == 0 ? "Desc" : "Asc";
			}
			return isDefaultSortColumn ? "Desc" : "Asc";
		}
	}
}