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
			Style = new Dictionary<string, string>();
			CssClass = new List<string>();
		}

		public Unit BorderWidth { get; set; }

		public int CellSpacing { get; set; }
		public List<string> CssClass { get; set; }
		public GridLines GridLines { get; set; }
		internal string Id { get; set; }
		public Dictionary<string, string> Style { get; set; }

		public void AddColumn(DataColumn<T> dataColumn)
		{
			_columns.Add(dataColumn);
		}

		public void AddColumn(CommandColumn<T> commandColumn)
		{
			_columns.Add(commandColumn);
		}

		public void AddCssClass(string cssClass)
		{
			CssClass.Add(cssClass);
		}

		public override string ToString()
		{
			var stream = new MemoryStream();
			using (var streamWriter = new StreamWriter(stream))
			{
				using (var writer = new HtmlTextWriter(streamWriter))
				{
					var table = new System.Web.UI.WebControls.Table
					            {
						            ID = Id,
						            BorderWidth = BorderWidth,
						            CellSpacing = CellSpacing,
						            GridLines = GridLines,
						            CssClass = "",
					            };

					CssClass.ForEach(x => table.CssClass = String.Format("{0} {1}", table.CssClass, x));
					table.CssClass = table.CssClass.Trim(new[] { ' ' });

					foreach (var kvp in Style)
					{
						table.Style.Add(kvp.Key, kvp.Value);
					}

					table.RenderBeginTag(writer);
					{
						writer.Write("<thead>");
						var tableHeader = new TableHeaderRow();
						{
							tableHeader.RenderBeginTag(writer);
							foreach (var column in _columns)
							{
								column.RenderHeader(writer);
							}
						}
						tableHeader.RenderEndTag(writer);
						writer.Write("</thead>");

						writer.Write("<tbody>");
						var rowIndex = 0;
						foreach (var item in _items)
						{
							var tableRow = new TableRow();
							tableRow.RenderBeginTag(writer);
							{
								foreach(var column in _columns)
								{
									column.Render(item, rowIndex, writer);
								}
							}
							tableRow.RenderEndTag(writer);
							rowIndex++;
						}
						writer.Write("</tbody>");
					}
					table.RenderEndTag(writer);
				}
			}
			return Encoding.UTF8.GetString(stream.ToArray());
		}
	}
}