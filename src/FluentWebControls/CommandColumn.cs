using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using FluentWebControls.Controls;
using FluentWebControls.Extensions;

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
		string HeaderText { get; }
		string ImageUrl { get; }
		string InnerHtml { get; }
		string Text { get; }
	}

	public class CommandColumn<T> : ICommandColumn, IHtmlColumn<T>
	{
		public CommandColumn(Func<T, string, Control> getControl)
		{
			_getControl = getControl;
			Align = AlignAttribute.Center;
		}

		private readonly Func<T, string, Control> _getControl;

		public void Render(T item, int rowIndex, HtmlTextWriter writer)
		{
			var columnText = (GetText != null ? GetText(item) : Text).ToNonNull().EscapeForHtml();

			if (InnerHtml != null)
			{
				columnText = InnerHtml;
			}
			else if (ImageUrl != null && Alt != null)
			{
				columnText = String.Format("<img src='{0}' alt='{1}'/>", ImageUrl, Alt);
			}

			var control = _getControl(item, columnText);

			var cell = new TableCell
			           {
				           HorizontalAlign = Align.ToHorizontalAlign(),
				           CssClass = CssClass
			           };
			if (control is IShouldHaveDifferentNameAndId)
			{
				control.ID += "_" + rowIndex;
			}
			if (LinkTarget != null)
			{
				((HyperLink)control).Target = LinkTarget;
			}
			cell.Controls.Add(control);
			cell.RenderControl(writer);
		}

		public void RenderHeader(HtmlTextWriter writer)
		{
			var cell = new TableHeaderCell
			           {
				           Text = HeaderText == null ? "&nbsp;" : HeaderText.EscapeForHtml(),
				           HorizontalAlign = Align.ToHorizontalAlign(),
				           CssClass = HeaderCssClass
			           };
			cell.RenderControl(writer);
		}

		internal AlignAttribute Align { private get; set; }
		AlignAttribute ICommandColumn.Align
		{
			get { return Align; }
		}
		internal string Alt { private get; set; }
		string ICommandColumn.Alt
		{
			get { return Alt; }
		}
		internal string CssClass { private get; set; }
		string ICommandColumn.CssClass
		{
			get { return CssClass; }
		}
		public Func<T, string> GetText { get; set; }
		internal string HeaderCssClass { private get; set; }
		string ICommandColumn.HeaderCssClass
		{
			get { return HeaderCssClass; }
		}
		public string HeaderText { get; set; }

		string ICommandColumn.HeaderText
		{
			get { return HeaderText; }
		}
		internal string ImageUrl { private get; set; }
		string ICommandColumn.ImageUrl
		{
			get { return ImageUrl; }
		}
		internal string InnerHtml { private get; set; }
		string ICommandColumn.InnerHtml
		{
			get { return InnerHtml; }
		}
		internal string Text { private get; set; }

		string ICommandColumn.Text
		{
			get { return Text; }
		}
		public string LinkTarget { get; set; }
	}
}