<%@ Import Namespace="Mvba.Enterprise.Mvc"%>
<%@ Control Language="C#" AutoEventWireup="false" Codebehind="ScrollableGridControl.ascx.cs" Inherits="Mvba.Enterprise.Views.Shared.ScrollableGridControl" %>
<%@ Import Namespace="FluentWebControls"%>
<%@ Import Namespace="Mvba.Enterprise.Views.Shared"%>

<style type="text/css">
	div.tableContainer { /* add the scroll bar */
		height: 300px; 	/* must be greater than tbody */
		overflow:hidden;
		overflow-y:auto;
	}
	
	div.tableContainer th {
		margin: 0;
		border-right: 1px solid #999;
		border-top: 1px solid #999;
		padding: 4px 3px 3px 4px;
		font-weight: bold;
	}
	
	div.tableContainer thead tr	
	{
		/* Causes the header row to stay fixed in IE */
		top: expression(offsetParent.scrollTop-2);
	}
	
	p.tip em {padding: 2px; background-color: #6cf; color: #FFF;}
</style>

<%= BuildFilters %>
<table style="margin-left: auto; margin-right: auto; width:inherit">
	<tr>
		<td>
			<div class="tableContainer">
				<table id='scrollableList' class="tablesorter {sortlist: [[<%= DefaultSortColumnIndex %>,0]]}" cellspacing="0" rules="cols" border="1" style="border-color:Gray;border-collapse:collapse;margin-left: auto; margin-right: auto;">
					<thead>
						<tr class="tblHeaderStyle">
							<%=BuildHeaderColumns%>
						</tr>
					</thead>
					<tbody>
						<%=BuildRows%>
					</tbody>
				</table>
			</div>
		</td>
	</tr>
</table>

<p class="tip" style="visibility:<%= GridColumns.Where(c=>c.Type != GridColumnType.Command && c.IsClientSideSortable).Count() > 1 ? "visible" : "hidden" %>"> 
	<em>TIP!</em> Sort multiple columns simultaneously by holding down the shift key and clicking a second, third or even fourth column header!
</p>	
