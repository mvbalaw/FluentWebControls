using System;
using System.Web.UI;

using FluentWebControls.Extensions;

using MvbaCore;

namespace FluentWebControls
{
	public class DataColumnWithId
	{
		public static DataColumnWithId<T> For<T>(Func<T, string> getColumnText, Func<T, string> getItemId, string columnPrefix)
		{
			return new DataColumnWithId<T>(getColumnText, getItemId, columnPrefix);
		}
	}

	public interface IDataColumnWithId : IDataColumn
	{
		ColumnTextType ColumnTextType { get; }
	}

	public class DataColumnWithId<T> : DataColumn<T>, IDataColumnWithId
	{
		private readonly string _columnPrefix;
		private readonly Func<T, string> _getItemId;

		public DataColumnWithId(Func<T, string> getColumnText, Func<T, string> getItemId, string columnPrefix)
			: base(getColumnText)
		{
			_getItemId = getItemId;
			_columnPrefix = columnPrefix;
			ColumnTextType = ColumnTextType.ColumnText;
		}

		internal ColumnTextType ColumnTextType { private get; set; }
		internal string InputElementCssClass { private get; set; }

		ColumnTextType IDataColumnWithId.ColumnTextType
		{
			get { return ColumnTextType; }
		}

		public override void Render(T item, HtmlTextWriter writer)
		{
			var tableCell = GetDefaultTableCell();
			
			string tableCellId = GetId(item);
			tableCell.ID = tableCellId;
			tableCell.Text = GetColumnWithInput(GetColumnText(item), tableCellId);
			tableCell.RenderControl(writer);
		}

		private string GetColumnWithInput(string value, string id)
		{
			if (ColumnTextType == ColumnTextType.TextBox)
			{
				return new TextBoxData(value).WithId(id).CssClass(InputElementCssClass).ToString();
			}
			return value;
		}

		private string GetId(T item)
		{
			if (_getItemId == null)
			{
				return _columnPrefix;
			}
			return String.Format("{0}_{1}_{2}", Reflection.GetClassName<T>(), _columnPrefix, _getItemId(item));
		}
	}

	public class ColumnTextType : NamedConstant<ColumnTextType>
	{
		public static ColumnTextType ColumnText = new ColumnTextType("column");
		public static ColumnTextType TextBox = new ColumnTextType("textbox");

		private ColumnTextType(string key)
		{
			Key = key;
		}
	}
}