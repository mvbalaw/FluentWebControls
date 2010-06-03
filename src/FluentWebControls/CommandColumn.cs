using System;
using System.Web.UI;
using System.Web.UI.WebControls;

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
		//string Href { get; }
		string CssClass { get; }
		string HeaderCssClass { get; }
		string Text { get; }
		string ImageUrl { get; }
		string Alt { get; }
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
		internal string CssClass { private get; set; }
		internal string HeaderCssClass { private get; set; }
		internal string Text { private get; set; }
		internal string ImageUrl { private get; set; }
		internal string Alt { private get; set; }

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

		public void Render(T item, HtmlTextWriter writer)
		{
			var control = _getControl(item, Text ?? String.Format("<img src='{0}' alt='{1}'/>", ImageUrl, Alt));
			var cell = new TableCell
				{
					HorizontalAlign = Align.ToHorizontalAlign(),
					CssClass = CssClass
				};
			cell.Controls.Add(control);
			cell.RenderControl(writer);
		}

		public void RenderHeader(HtmlTextWriter writer)
		{
			var cell = new TableHeaderCell
				{
					Text = "&nbsp;",
					CssClass = HeaderCssClass
				};
			cell.RenderControl(writer);
		}
	}
}