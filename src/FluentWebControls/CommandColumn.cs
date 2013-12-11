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
using System.Web.UI;
using System.Web.UI.WebControls;

using FluentWebControls.Controls;

namespace FluentWebControls
{
	public class CommandColumn
	{
		public static CommandColumn<T> For<T>(Func<T, string, Control> getControl)
		{
			return new CommandColumn<T>(getControl);
		}
	}

	public interface ICommandColumn
	{
		AlignAttribute Align { get; }
		string Alt { get; }
		//string Href { get; }
		string CssClass { get; }
		string HeaderCssClass { get; }
		string ImageUrl { get; }
		string Text { get; }
	}

	public class CommandColumn<T> : ICommandColumn, IHtmlColumn<T>
	{
		private readonly Func<T, string, Control> _getControl;

		public CommandColumn(Func<T, string, Control> getControl)
		{
			_getControl = getControl;
			Align = AlignAttribute.Center;
		}

		internal AlignAttribute Align { private get; set; }
		internal string Alt { private get; set; }
		internal string CssClass { private get; set; }
		internal string HeaderCssClass { private get; set; }
		internal string ImageUrl { private get; set; }
		internal string Text { private get; set; }

		string ICommandColumn.HeaderCssClass
		{
			get { return HeaderCssClass; }
		}

		string ICommandColumn.Text
		{
			get { return Text; }
		}

		string ICommandColumn.ImageUrl
		{
			get { return ImageUrl; }
		}

		string ICommandColumn.Alt
		{
			get { return Alt; }
		}

		AlignAttribute ICommandColumn.Align
		{
			get { return Align; }
		}

		string ICommandColumn.CssClass
		{
			get { return CssClass; }
		}

		public void Render(T item, int rowIndex, HtmlTextWriter writer)
		{
			var control = _getControl(item, Text ?? String.Format("<img src='{0}' alt='{1}'/>", ImageUrl, Alt));
			var cell = new TableCell
			           {
				           HorizontalAlign = Align.ToHorizontalAlign(),
				           CssClass = CssClass
			           };
			if (control is IShouldHaveDifferentNameAndId)
			{
				control.ID += "_" + rowIndex;
			}
			cell.Controls.Add(control);
			cell.RenderControl(writer);
		}

		public void RenderHeader(HtmlTextWriter writer)
		{
			var cell = new TableHeaderCell
			           {
				           Text = "&nbsp;",
				           HorizontalAlign = Align.ToHorizontalAlign(),
				           CssClass = HeaderCssClass
			           };
			cell.RenderControl(writer);
		}
	}
}