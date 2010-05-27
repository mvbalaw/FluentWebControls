using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using FluentWebControls.Extensions;

using MvbaCore;

namespace FluentWebControls
{
	public class TextBoxColumn
	{
		public static TextBoxColumn<T> For<T>(Func<T, string> getColumnText, Func<T, string> getColumnId, string textBoxId)
		{
			return new TextBoxColumn<T>(getColumnText, getColumnId, textBoxId);
		}
	}

	public interface ITextBoxColumn
	{
		AlignAttribute Align { get; }
		string CssClass { get; }
		string TextBoxCssClass { get; }
		AlignAttribute HeaderAlign { get; }
		string HeaderCssClass { get; }
		string HeaderText { get; }
	}

	public class TextBoxColumn<T> : ITextBoxColumn, IHtmlColumn<T>
	{
		private readonly Func<T, string> _getColumnText;
		private readonly Func<T, string> _getColumnId;
		private readonly string _textBoxId;

		public TextBoxColumn(Func<T, string> getColumnText, Func<T, string> getColumnId, string textBoxId)
		{
			_getColumnText = getColumnText;
			_getColumnId = getColumnId;
			_textBoxId = textBoxId;
			Align = AlignAttribute.Left;
			HeaderAlign = AlignAttribute.Center;
		}

		internal AlignAttribute Align { private get; set; }
		internal string CssClass { private get; set; }
		internal string TextBoxCssClass { private get; set; }
		internal AlignAttribute HeaderAlign { private get; set; }
		internal string HeaderCssClass { private get; set; }
		internal string HeaderText { private get; set; }

		public void Render(T item, HtmlTextWriter writer)
		{
			var id = String.Format("{0}_{1}_{2}", Reflection.GetClassName<T>(), _textBoxId, _getColumnId(item));
			var textBoxCssClass = TextBoxCssClass ?? "tabletextbox";
			var cell = new TableCell
				{
					HorizontalAlign = Align.ToHorizontalAlign(),
					Text = new TextBoxData(_getColumnText(item)).WithId(id).CssClass(textBoxCssClass).ToString(),
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

		AlignAttribute ITextBoxColumn.Align
		{
			get { return Align; }
		}
		string ITextBoxColumn.CssClass
		{
			get { return CssClass; }
		}
		string ITextBoxColumn.TextBoxCssClass
		{
			get { return TextBoxCssClass; }
		}
		string ITextBoxColumn.HeaderText
		{
			get { return HeaderText; }
		}
		AlignAttribute ITextBoxColumn.HeaderAlign
		{
			get { return HeaderAlign; }
		}
		string ITextBoxColumn.HeaderCssClass
		{
			get { return HeaderCssClass; }
		}
	}
}