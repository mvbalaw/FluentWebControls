using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentWebControls.Interfaces;

namespace FluentWebControls
{
	public class ScrollableGridData<T> : GridData<T>
	{
		private const string Body = @"
<style type=""text/css"">
	div.tableContainer {{ /* add the scroll bar */
		height: 300px; 	/* must be greater than tbody */
		overflow:hidden;
		overflow-y:auto;
	}}
	
	div.tableContainer th {{
		margin: 0;
		border-right: 1px solid #999;
		border-top: 1px solid #999;
		padding: 4px 3px 3px 4px;
		font-weight: bold;
	}}
	
	div.tableContainer thead tr	
	{{
		/* Causes the header row to stay fixed in IE */
		top: expression(offsetParent.scrollTop-2);
	}}
	
	p.tip em {{padding: 2px; background-color: #6cf; color: #FFF;}}
</style>
{0}
<table style=""margin-left: auto; margin-right: auto; width:inherit"">
	<tr>
		<td>
			<div class=""tableContainer"">
				<table id='scrollableList' class=""tablesorter {{sortlist: [[{1},0]]}}"" cellspacing=""0"" rules=""cols"" border=""1"" style=""border-color:Gray;border-collapse:collapse;margin-left: auto; margin-right: auto;"">
					<thead>
						<tr class=""tblHeaderStyle"">
							{2}
						</tr>
					</thead>
					<tbody>
						{3}
					</tbody>
				</table>
			</div>
		</td>
	</tr>
</table>

<p class=""tip"" style=""visibility:{4}""> 
	<em>TIP!</em> Sort multiple columns simultaneously by holding down the shift key and clicking a second, third or even fourth column header!
</p>	
";

		public ScrollableGridData(IPagedListParameters pagedListParameters, string controllerName, string actionName, IEnumerable<T> items, int total)
			: base(pagedListParameters, controllerName, actionName, items, total)
		{
		}

		protected override bool ClientSideSortingEnabled
		{
			get { return true; }
		}

		private int GetDefaultSortColumnIndex()
		{
			var indexedGridColumns = GridColumns
				.Select((x, i) => new
					{
						Item = x,
						Index = i
					})
				.ToList();
			var sortField = indexedGridColumns.FirstOrDefault(x => x.Item.FieldName == PagedListParameters.SortField);
			if (sortField != null)
			{
				return sortField.Index;
			}
			var defaultColumn = indexedGridColumns.FirstOrDefault(x => x.Item.IsDefaultSortColumn);
			if (defaultColumn != null)
			{
				return defaultColumn.Index;
			}
			return 0;
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			string tipVisibility = GridColumns.Where(c => c.Type != GridColumnType.Command && c.IsClientSideSortable).Count() > 1 ? "visible" : "hidden";
			sb.AppendFormat(Body, BuildFilters(), GetDefaultSortColumnIndex(), BuildHeaderColumns(), BuildRows(), tipVisibility);
			return sb.ToString();
		}
	}
}