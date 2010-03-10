using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FluentWebControls
{
	public class CommandColumn
	{
		public static CommandColumn<T> For<T>(Func<T, string> getHref)
		{
			return new CommandColumn<T>(getHref);
		}
	}

	public interface ICommandColumn
	{
		AlignAttribute Align { get; }
		//string Href { get; }
		string CssClass { get; }
		string Text { get; }
	}

	public class CommandColumn<T> : ICommandColumn, IHtmlColumn<T>
	{
		private readonly Func<T, string> _getHref;

		public CommandColumn(Func<T, string> getHref)
		{
			_getHref = getHref;
			Align = AlignAttribute.Center;
		}

		internal AlignAttribute Align { private get; set; }
		internal string CssClass { private get; set; }

		internal string Text { private get; set; }
		string ICommandColumn.Text
		{
			get { return Text; }
		}

		AlignAttribute ICommandColumn.Align
		{
			get { return Align; }
		}

		string ICommandColumn.CssClass
		{
			get { return CssClass; }
		}

		public void Render(T item, HtmlTextWriter writer)
		{
			var link = new HyperLink
				{
					NavigateUrl = _getHref(item),
					Text = Text
				};
			var cell = new TableCell
				{
					HorizontalAlign = Align.ToHorizontalAlign(),
					CssClass = CssClass
				};
			cell.Controls.Add(link);

			cell.RenderBeginTag(writer);
			cell.RenderControl(writer);
			cell.RenderEndTag(writer);
		}

		public void RenderHeader(HtmlTextWriter writer)
		{
			var cell = new TableHeaderCell
				{
					Text = "&nbsp;"
				};
			cell.RenderBeginTag(writer);
			cell.RenderControl(writer);
			cell.RenderEndTag(writer);
		}
	}
}