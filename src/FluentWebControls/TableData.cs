using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using FluentWebControls.Extensions;

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
        public string RowCssClass { get; set; }
        public string RowIdPrefix { get; set; }
        public Func<T, int> GetRowId { get; set; }

        public GridLines GridLines { get; set; }
        internal string Id { get; set; }
        public Dictionary<string, string> Style { get; set; }
        public Func<T, string> GetLink { private get; set; }

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
                        CssClass = ""
                    };

                    CssClass.ForEach(x => table.CssClass = $"{table.CssClass} {x}");
                    table.CssClass = table.CssClass.Trim(' ');

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
                            BeginTableRow(item, writer);
                            foreach (var column in _columns)
                            {
                                column.Render(item, rowIndex, writer);
                            }
                            EndTableRow(writer);
                            rowIndex++;
                        }
                        writer.Write("</tbody>");
                    }
                    table.RenderEndTag(writer);
                }
            }
            return Encoding.UTF8.GetString(stream.ToArray());
        }

        // ReSharper disable once SuggestBaseTypeForParameter
        private static void EndTableRow(HtmlTextWriter writer)
        {
            var tableRow = new StringBuilder();
            tableRow.Append('<');
            tableRow.Append('/');
            tableRow.Append("tr");
            tableRow.Append('>');
            writer.Write(tableRow);
        }

        private string GetTableRowId(T item)
        {
            return $"{RowIdPrefix}{GetRowId(item)}";
        }

        // ReSharper disable once SuggestBaseTypeForParameter
        private void BeginTableRow(T item, HtmlTextWriter writer)
        {
            var tableRow = new StringBuilder();
            tableRow.Append('<');
            tableRow.Append("tr");

            if (GetRowId != null)
            {
                tableRow.Append(GetTableRowId(item).Trim().CreateQuotedAttribute("id"));
            }
            if (RowCssClass != null)
            {
                tableRow.Append(RowCssClass.Trim().CreateQuotedAttribute("class"));
            }
            if (GetLink != null)
            {
                tableRow.Append(GetLink(item).CreateQuotedAttribute("data-link"));
            }
            tableRow.Append('>');
            writer.Write(tableRow);
        }
    }
}