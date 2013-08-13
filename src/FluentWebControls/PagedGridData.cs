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

		protected int LastPage
		{
			get { return (int)Math.Ceiling((decimal)Total / PagedListParameters.PageSize); }
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendFormat(Body,
				base.BuildFilters(),
				BuildHeaderColumns(),
				BuildRows(),
				Fluent.LinkTo(ControllerName, ControllerExtension, ActionName)
					.WithLinkText("<<")
					.WithMouseOverText("First Page")
					.WithCssClass("linkHighlight")
					.DisabledIf(PagedListParameters.PageNumber == 1)
					.WithQueryStringData(() => PagedListParameters.PageNumber, 1)
					.WithQueryStringData(() => PagedListParameters.PageSize)
					.WithQueryStringData(() => PagedListParameters.SortDirection)
					.WithQueryStringData(() => PagedListParameters.SortField)
					.WithQueryStringData(Filters.Select(f => new KeyValuePair<string, string>(((IWebControl)f).Id, ((IDropDownListData)f).SelectedValue))),
				Fluent.LinkTo(ControllerName, ControllerExtension, ActionName)
					.WithLinkText("<")
					.WithMouseOverText("Previous Page")
					.WithCssClass("linkHighlight")
					.DisabledIf(PagedListParameters.PageNumber == 1)
					.WithQueryStringData(() => PagedListParameters.PageNumber, PagedListParameters.PageNumber - 1)
					.WithQueryStringData(() => PagedListParameters.PageSize)
					.WithQueryStringData(() => PagedListParameters.SortDirection)
					.WithQueryStringData(() => PagedListParameters.SortField)
					.WithQueryStringData(Filters.Select(f => new KeyValuePair<string, string>(((IWebControl)f).Id, ((IDropDownListData)f).SelectedValue))),
				Fluent.LinkTo(ControllerName, ControllerExtension, ActionName)
					.WithLinkText(">")
					.WithMouseOverText("Next Page")
					.WithCssClass("linkHighlight")
					.DisabledIf(PagedListParameters.PageNumber == LastPage)
					.WithQueryStringData(() => PagedListParameters.PageNumber, PagedListParameters.PageNumber + 1)
					.WithQueryStringData(() => PagedListParameters.PageSize)
					.WithQueryStringData(() => PagedListParameters.SortDirection)
					.WithQueryStringData(() => PagedListParameters.SortField)
					.WithQueryStringData(Filters.Select(f => new KeyValuePair<string, string>(((IWebControl)f).Id, ((IDropDownListData)f).SelectedValue))),
				Fluent.LinkTo(ControllerName, ControllerExtension, ActionName)
					.WithLinkText(">>")
					.WithMouseOverText("Last Page")
					.WithCssClass("linkHighlight")
					.DisabledIf(PagedListParameters.PageNumber == LastPage)
					.WithQueryStringData(() => PagedListParameters.PageNumber, LastPage)
					.WithQueryStringData(() => PagedListParameters.PageSize)
					.WithQueryStringData(() => PagedListParameters.SortDirection)
					.WithQueryStringData(() => PagedListParameters.SortField)
					.WithQueryStringData(Filters.Select(f => new KeyValuePair<string, string>(((IWebControl)f).Id, ((IDropDownListData)f).SelectedValue))),
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
	}
}