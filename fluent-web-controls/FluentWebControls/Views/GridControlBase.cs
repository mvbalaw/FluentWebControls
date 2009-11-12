using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;

namespace FluentWebControls.Views
{
	public abstract class GridControlBase : MvcControl<GridData>
	{
		protected string ControllerName { get; private set; }
		protected string ControllerExtension { get; set; }
		protected string ActionName { get; private set; }
		protected IPagedListParameters PagedListParameters { get; set; }
		protected IEnumerable<IGridColumn> GridColumns { get; set; }
		protected IEnumerable<DropDownListData> Filters { get; set; }
		private int RowCount { get; set; }

		protected abstract bool ClientSideSortingEnabled { get; }

		public override void Page_Load(object sender, EventArgs e)
		{
			GridData model = ViewData.Model;
			GridColumns = model.GridColumns;
			Filters = model.Filters;
			PagedListParameters = model.PagedListParameters;
			ControllerName = model.ControllerName;
			ControllerExtension = model.ControllerExtension;
			ActionName = model.ActionName;
			RowCount = model.RowCount;
		}

		protected string BuildFilters
		{
			get
			{

				StringBuilder sb = new StringBuilder();
				if (Filters.Any())
				{
					sb.Append("<table width='700px'><tr>");
					foreach (DropDownListData filter in Filters)
					{
						sb.Append("<td align='center'>");
						sb.Append(filter);
						sb.Append("</td>");
					}
					sb.Append("</tr></table>");
				}
				return sb.ToString();
			}
		}

		protected string BuildHeaderColumns
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				int columnNumber = 0;
				foreach (IGridColumn column in GridColumns)
				{
					sb.AppendFormat("<th{0}{1}>", AlignAttribute.Center, ClientSideSortingEnabled && !column.IsClientSideSortable ? " class=\"{sorter: false}\"" : "");
					switch (column.Type)
					{
						case GridColumnType.Sortable:
							sb.Append(Link
							          	.To(ControllerName, ControllerExtension, ActionName)
							          	.WithLinkText(column.ColumnHeader)
							          	.WithData(() => PagedListParameters.SortDirection, GetNextSortDirection(column.FieldName, column.IsDefaultSortColumn))
							          	.WithData(() => PagedListParameters.SortField, column.FieldName)
							          	.WithData(Filters.Select(f => new KeyValuePair<string, string>(((IWebControl)f).Id, f.SelectedValue)))
							          	.Id("th" + columnNumber)
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
		}
		protected string BuildRows
		{
			get
			{
				StringBuilder sb = new StringBuilder();

				foreach (int rowId in Enumerable.Range(0, RowCount))
				{
					BuildRow(rowId, sb);
				}
				return sb.ToString();
			}
		}

		private void BuildRow(int rowId, StringBuilder sb)
		{
			sb.Append("<tr>");
			foreach (IGridColumn column in GridColumns)
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

		protected string GetNextSortDirection(string fieldName, bool isDefaultSortColumn)
		{
			if (PagedListParameters.SortField == fieldName)
			{
				return String.Compare(PagedListParameters.SortDirection, "Asc", true) == 0 ? "Desc" : "Asc";
			}
			return isDefaultSortColumn ? "Desc" : "Asc";
		}
	}
}