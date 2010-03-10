using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FluentWebControls
{
	public class TableData<T>
	{
		private readonly List<IHtmlColumn<T>> _columns = new List<IHtmlColumn<T>>();
		private readonly IEnumerable<T> _items;

		public TableData(IEnumerable<T> items)
		{
			_items = items;
		}

		public void AddColumn(DataColumn<T> dataColumn)
		{
			_columns.Add(dataColumn);
		}

		public void AddColumn(CommandColumn<T> commandColumn)
		{
			_columns.Add(commandColumn);
		}

		public override string ToString()
		{
			var stream = new MemoryStream();
			using (var streamWriter = new StreamWriter(stream))
			{
				using (var writer = new HtmlTextWriter(streamWriter))
				{
					var table = new System.Web.UI.WebControls.Table();
					table.RenderBeginTag(writer);
					{
						var tableHeader = new TableHeaderRow();
						tableHeader.RenderBeginTag(writer);

						foreach (var column in _columns)
						{
							column.RenderHeader(writer);
						}

						tableHeader.RenderEndTag(writer);

						foreach (var item in _items)
						{
							var tableRow = new TableRow();
							tableRow.RenderBeginTag(writer);
							foreach (var column in _columns)
							{
								column.Render(item, writer);
							}
							tableRow.RenderEndTag(writer);
						}
					}
					table.RenderEndTag(writer);
				}
			}
			return Encoding.ASCII.GetString(stream.ToArray());
		}
	}
}