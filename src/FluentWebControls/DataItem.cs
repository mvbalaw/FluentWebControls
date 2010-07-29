using System;
using System.Text;

using FluentWebControls.Extensions;

using MvbaCore;

namespace FluentWebControls
{
	public class DataItem
	{
		public static DataItem<T> For<T>(Func<T, string> getColumnText, string columnName)
		{
			return new DataItem<T>(getColumnText, columnName);
		}
	}

	public interface IDataItem
	{
		AlignAttribute Align { get; }
		string InputCssClass { get; }
	}

	public class DataItem<T> : IDataItem, IListItem<T>
	{
		protected readonly string ColumnName;
		protected readonly Func<T, string> GetColumnText;

		public DataItem(Func<T, string> getColumnText, string columnName)
		{
			GetColumnText = getColumnText;
			ColumnName = columnName;
			Align = AlignAttribute.Left;
		}

		internal AlignAttribute Align { private get; set; }
		internal ColumnTextType ColumnTextType { get; set; }
		internal Func<T, string> GetItemId { get; set; }
		internal string InputCssClass { get; set; }
		internal string InputTextId { get; set; }

		AlignAttribute IDataItem.Align
		{
			get { return Align; }
		}

		string IDataItem.InputCssClass
		{
			get { return InputCssClass; }
		}

		public StringBuilder Render(T item)
		{
			var listItem = new StringBuilder();
			listItem.Append("<");
			listItem.Append("div");
			listItem.Append(Align.Text.CreateQuotedAttribute("align"));
			listItem.Append(">");
			listItem.Append(GetColumnWithInput(item));
			listItem.Append("</div>");
			return listItem;
		}

		private string GetColumnWithInput(T item)
		{
			if (ColumnTextType == ColumnTextType.TextBox)
			{
				return new TextBoxData(GetColumnText(item)).WithId(GetId(item)).CssClass(InputCssClass).ToString();
			}
			if (ColumnTextType == ColumnTextType.Span)
			{
				return new SpanData(GetColumnText(item)).WithId(GetId(item)).WithCssClass(InputCssClass).ToString();
			}
			if (ColumnTextType == ColumnTextType.Hidden)
			{
				return new HiddenData().WithId(GetId(item)).WithValue(GetColumnText(item)).ToString();
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
			return String.Format("{0}_{1}_{2}", Reflection.GetClassName<T>(), ColumnName, GetItemId(item));
		}
	}
}