using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using FluentWebControls.Extensions;

using MvbaCore;

namespace FluentWebControls
{
	public class DataColumn
	{
		public static DataColumn<T> For<T>(Func<T, string> getColumnText, string columnName)
		{
			return new DataColumn<T>(getColumnText, columnName);
		}
	}

	public interface IDataColumn
	{
		AlignAttribute Align { get; }
		string CssClass { get; }
		AlignAttribute HeaderAlign { get; }
		string HeaderCssClass { get; }
		string HeaderText { get; }
		string InputCssClass { get; }
	}

	public class DataColumn<T> : IDataColumn, IHtmlColumn<T>
	{
		protected readonly string ColumnName;
		protected readonly Func<T, string> GetColumnText;

		public DataColumn(Func<T, string> getColumnText, string columnName)
		{
			GetColumnText = getColumnText;
			ColumnName = columnName;
			Align = AlignAttribute.Left;
			HeaderAlign = AlignAttribute.Center;
		}

		internal AlignAttribute Align { private get; set; }
		internal ColumnTextType ColumnTextType { get; set; }
		internal string CssClass { get; set; }
		internal Func<T, string> GetItemId { get; set; }
		internal AlignAttribute HeaderAlign { private get; set; }
		internal string HeaderCssClass { private get; set; }
		internal string HeaderText { private get; set; }
		internal string InputCssClass { get; set; }
		internal string InputTextId { get; set; }

		AlignAttribute IDataColumn.Align
		{
			get { return Align; }
		}
		string IDataColumn.CssClass
		{
			get { return CssClass; }
		}
		string IDataColumn.InputCssClass
		{
			get { return InputCssClass; }
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

		public virtual void Render(T item, HtmlTextWriter writer)
		{
			var cell = new TableCell
				{
					HorizontalAlign = Align.ToHorizontalAlign(),
					Text = GetColumnWithInput(item),
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

		private string GetColumnWithInput(T item)
		{
			if (ColumnTextType == ColumnTextType.TextBox)
			{
				return new TextBoxData(GetColumnText(item)).WithId(GetId(item)).CssClass(InputCssClass).ToString();
			}
			if (ColumnTextType == ColumnTextType.Hidden)
			{
				return new HiddenData().WithId(GetId(item)).WithValue(GetColumnText(item)).ToString();
			}
			if (ColumnTextType == ColumnTextType.Span)
			{
				return new SpanData(GetColumnText(item)).WithId(GetId(item)).WithCssClass(InputCssClass).ToString();
			}
			return GetColumnText(item);
		}

		private string GetId(T item)
		{
			if (InputTextId != null)
			{
				return InputTextId;
			}

			if (GetItemId == null)
			{
				return InputTextId ?? ColumnName;
			}
			return String.Format("{0}_{1}", ColumnName, GetItemId(item));
		}
	}

	public class ColumnTextType : NamedConstant<ColumnTextType>
	{
		[DefaultKey]
		public static ColumnTextType ColumnText = new ColumnTextType("column");
		public static ColumnTextType Hidden = new ColumnTextType("hidden");
		public static ColumnTextType Span = new ColumnTextType("span");
		public static ColumnTextType TextBox = new ColumnTextType("textbox");

		private ColumnTextType(string key)
		{
			Key = key;
		}
	}
}