using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;

namespace FluentWebControls
{
	public class PagedGridData<T> : GridData<T>
	{
		private const string Body = @"
{0}
<table class=""list"" cellspacing=""0"" rules=""cols"" border=""1"" style=""border-color:Gray;border-collapse:collapse;"">
	<thead>
		<tr class=""tblHeaderStyle"">
			{1}
		</tr>
	</thead>
	<tbody>
		{2}
	</tbody>
</table>
<div id=""pager"">
	{3}&nbsp;{4}&nbsp;&nbsp;&nbsp;{5}&nbsp;{6} Page {7} of {8}&nbsp;{9}{10}{11}{12}
</div>";

		public PagedGridData(IPagedListParameters pagedListParameters, string controllerName, string actionName, IEnumerable<T> items, int total)
			: base(pagedListParameters, controllerName, actionName, items, total)
		{
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat(Body,
			                base.BuildFilters(),
			                BuildHeaderColumns(),
			                BuildRows(),
			                Fluent.LinkTo(ControllerName, ControllerExtension, ActionName)
			                	.WithLinkText("<<")
			                	.WithMouseOverText("First Page")
			                	.WithCssClass("linkHighlight")
			                	.DisabledIf(PagedListParameters.PageNumber == 1)
			                	.WithData(() => PagedListParameters.PageNumber, 1)
			                	.WithData(() => PagedListParameters.PageSize)
			                	.WithData(() => PagedListParameters.SortDirection)
			                	.WithData(() => PagedListParameters.SortField)
			                	.WithData(Filters.Select(f => new KeyValuePair<string, string>(((IWebControl)f).Id, f.SelectedValue))),
			                Fluent.LinkTo(ControllerName, ControllerExtension, ActionName)
			                	.WithLinkText("<")
			                	.WithMouseOverText("Previous Page")
			                	.WithCssClass("linkHighlight")
			                	.DisabledIf(PagedListParameters.PageNumber == 1)
			                	.WithData(() => PagedListParameters.PageNumber, PagedListParameters.PageNumber - 1)
			                	.WithData(() => PagedListParameters.PageSize)
			                	.WithData(() => PagedListParameters.SortDirection)
			                	.WithData(() => PagedListParameters.SortField)
			                	.WithData(Filters.Select(f => new KeyValuePair<string, string>(((IWebControl)f).Id, f.SelectedValue))),
			                Fluent.LinkTo(ControllerName, ControllerExtension, ActionName)
			                	.WithLinkText(">")
			                	.WithMouseOverText("Next Page")
			                	.WithCssClass("linkHighlight")
			                	.DisabledIf(PagedListParameters.PageNumber == LastPage)
			                	.WithData(() => PagedListParameters.PageNumber, PagedListParameters.PageNumber + 1)
			                	.WithData(() => PagedListParameters.PageSize)
			                	.WithData(() => PagedListParameters.SortDirection)
			                	.WithData(() => PagedListParameters.SortField)
			                	.WithData(Filters.Select(f => new KeyValuePair<string, string>(((IWebControl)f).Id, f.SelectedValue))),
			                Fluent.LinkTo(ControllerName, ControllerExtension, ActionName)
			                	.WithLinkText(">>")
			                	.WithMouseOverText("Last Page")
			                	.WithCssClass("linkHighlight")
			                	.DisabledIf(PagedListParameters.PageNumber == LastPage)
			                	.WithData(() => PagedListParameters.PageNumber, LastPage)
			                	.WithData(() => PagedListParameters.PageSize)
			                	.WithData(() => PagedListParameters.SortDirection)
			                	.WithData(() => PagedListParameters.SortField)
			                	.WithData(Filters.Select(f => new KeyValuePair<string, string>(((IWebControl)f).Id, f.SelectedValue))),
			                Fluent.TextBoxFor(PagedListParameters, x => x.PageNumber.ToString(), x => x.PageNumber)
			                	.MinValue(1)
			                	.MaxValue(LastPage)
			                	.Width("21px"),
			                LastPage,
			                Fluent.ButtonFor(ButtonData.ButtonType.Go, ControllerName, ActionName),
			                Fluent.HiddenFor(PagedListParameters, x => x.PageSize.ToString(), x => x.PageSize),
			                Fluent.HiddenFor(PagedListParameters, x => x.SortDirection),
			                Fluent.HiddenFor(PagedListParameters, x => x.SortField)
				);
			return sb.ToString();
		}

		protected int LastPage
		{
			get { return (int)Math.Ceiling((decimal)Total / PagedListParameters.PageSize); }
		}
	}
}