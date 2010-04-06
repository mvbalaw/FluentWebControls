using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FluentWebControls
{
	public class DataColumn
	{
		public static DataColumn<T> For<T>(Func<T, string> getColumnText)
		{
			return new DataColumn<T>(getColumnText);
		}
	}

	public interface IDataColumn
	{
		AlignAttribute Align { get; }
		string CssClass { get; }
		AlignAttribute HeaderAlign { get; }
		string HeaderCssClass { get; }
		string HeaderText { get; }
	}

	public class DataColumn<T> : IDataColumn, IHtmlColumn<T>
	{
		private readonly Func<T, string> _getColumnText;

		public DataColumn(Func<T, string> getColumnText)
		{
			_getColumnText = getColumnText;
			Align = AlignAttribute.Left;
			HeaderAlign = AlignAttribute.Center;
		}

		internal AlignAttribute Align { private get; set; }
		internal string CssClass { private get; set; }
		internal AlignAttribute HeaderAlign { private get; set; }
		internal string HeaderCssClass { private get; set; }
		internal string HeaderText { private get; set; }

		AlignAttribute IDataColumn.Align
		{
			get { return Align; }
		}
		string IDataColumn.CssClass
		{
			get { return CssClass; }
		}
		string IDataColumn.HeaderText
		{
			get { return HeaderText; }
		}
		AlignAttribute IDataColumn.HeaderAlign
		{
			get { return HeaderAlign; }
		}
		string IDataColumn.HeaderCssClass
		{
			get { return HeaderCssClass; }
		}

		public void Render(T item, HtmlTextWriter writer)
		{
			var cell = new TableCell
				{
					HorizontalAlign = Align.ToHorizontalAlign(),
					Text = _getColumnText(item),
					CssClass = CssClass
				};
			cell.RenderControl(writer);
		}

		public void RenderHeader(HtmlTextWriter writer)
		{
			var cell = new TableHeaderCell
				{
					HorizontalAlign = HeaderAlign.ToHorizontalAlign(),
					Text = HeaderText,
					CssClass = HeaderCssClass
				};
			cell.RenderControl(writer);
		}
	}
}