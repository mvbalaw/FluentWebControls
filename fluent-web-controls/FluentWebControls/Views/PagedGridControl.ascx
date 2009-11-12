<%@ Control Language="C#" AutoEventWireup="false" Codebehind="PagedGridControl.ascx.cs" Inherits="FluentWebControls.Views.PagedGridControl" %>
<%@ Import Namespace="System.Linq"%>
<%@ Import Namespace="System.Collections.Generic"%>
<%@ Import Namespace="FluentWebControls.Interfaces"%>
<%@ Import Namespace="FluentWebControls.Extensions"%>
<%@ Import Namespace="FluentWebControls"%>

<%= BuildFilters %>
<table class="list" cellspacing="0" rules="cols" border="1" style="border-color:Gray;border-collapse:collapse;">
	<thead>
		<tr class="tblHeaderStyle">
			<%=BuildHeaderColumns%>
		</tr>
	</thead>
	<tbody>
		<%=BuildRows%>
	</tbody>
</table>
<div id="pager">
	<%=
		Fluent.LinkTo(ControllerName, ".mvc", ActionName)
			.WithLinkText("<<")
			.WithMouseOverText("First Page")
			.CssClass("linkHighlight")
			.DisabledIf(PagedListParameters.PageNumber == 1)
			.WithData(() => PagedListParameters.PageNumber, 1)
			.WithData(() => PagedListParameters.PageSize)
			.WithData(() => PagedListParameters.SortDirection)
			.WithData(() => PagedListParameters.SortField)
			.WithData(Filters.Select(f=> new KeyValuePair<string,string>(((IWebControl)f).Id, f.SelectedValue)))
	%>&nbsp;<%=
		Fluent.LinkTo(ControllerName, ".mvc", ActionName)
			.WithLinkText("<")
			.WithMouseOverText("Previous Page")
			.CssClass("linkHighlight")
			.DisabledIf(PagedListParameters.PageNumber == 1)
			.WithData(() => PagedListParameters.PageNumber, PagedListParameters.PageNumber - 1)
			.WithData(() => PagedListParameters.PageSize)
			.WithData(() => PagedListParameters.SortDirection)
			.WithData(() => PagedListParameters.SortField)
			.WithData(Filters.Select(f=> new KeyValuePair<string,string>(((IWebControl)f).Id, f.SelectedValue)))
	%>&nbsp;&nbsp;&nbsp;<%=
		Fluent.LinkTo(ControllerName, ".mvc", ActionName)
			.WithLinkText(">")
			.WithMouseOverText("Next Page")
			.CssClass("linkHighlight")
			.DisabledIf(PagedListParameters.PageNumber == LastPage)
			.WithData(() => PagedListParameters.PageNumber, PagedListParameters.PageNumber + 1)
			.WithData(() => PagedListParameters.PageSize)
			.WithData(() => PagedListParameters.SortDirection)
			.WithData(() => PagedListParameters.SortField)
			.WithData(Filters.Select(f=> new KeyValuePair<string,string>(((IWebControl)f).Id, f.SelectedValue)))
	%>&nbsp;<%=
		Fluent.LinkTo(ControllerName, ".mvc", ActionName)
			.WithLinkText(">>")
			.WithMouseOverText("Last Page")
			.CssClass("linkHighlight")
			.DisabledIf(PagedListParameters.PageNumber == LastPage)
			.WithData(() => PagedListParameters.PageNumber, LastPage)
			.WithData(() => PagedListParameters.PageSize)
			.WithData(() => PagedListParameters.SortDirection)
			.WithData(() => PagedListParameters.SortField)
			.WithData(Filters.Select(f=> new KeyValuePair<string,string>(((IWebControl)f).Id, f.SelectedValue)))
	%> Page <%=
		Fluent.TextBoxFor(PagedListParameters, x => x.PageNumber.ToString(), x => x.PageNumber)
			.MinValue(1)
			.MaxValue(LastPage)
			.Width("21px")
	%> of <%= LastPage 
	%>&nbsp;<%= 
		Fluent.ButtonFor(ButtonData.ButtonType.Go, ControllerName, ActionName)
	%><%=
		Fluent.HiddenFor(PagedListParameters, x => x.PageSize.ToString(), x => x.PageSize)
	%><%=
		Fluent.HiddenFor(PagedListParameters, x => x.SortDirection)
	%><%=
		Fluent.HiddenFor(PagedListParameters, x => x.SortField)
	%>
</div>
