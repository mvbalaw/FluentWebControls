using System.Collections.Generic;
using System.Text;

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public class LinkData : WebControlBase
	{
		private readonly Dictionary<string, string> _queryStringData = new Dictionary<string, string>();
		public string CssClass { private get; set; }
		public bool Disabled { get; set; }
		public string Href { get; set; }
		public string Id { get; set; }
		public string LinkText { get; set; }
		public string MouseOverText { get; set; }
		public string Rel { get; set; }

		public void AddQueryStringData(string key, string value)
		{
			_queryStringData.Add(key, value);
		}

		private string BuildQueryString()
		{
			if (_queryStringData.Count == 0)
			{
				return "";
			}
			StringBuilder sb = new StringBuilder();
			sb.Append('?');
			foreach (var keyValuePair in _queryStringData)
			{
				sb.AppendFormat("{0}={1}&", keyValuePair.Key.EscapeForUrl(), keyValuePair.Value.EscapeForUrl());
			}
			return sb.ToString();
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("<a");
			if (Id != null)
			{
				sb.Append(Id.CreateQuotedAttribute("id"));
				sb.Append(Id.CreateQuotedAttribute("name"));
			}

			if (Disabled)
			{
				sb.AppendFormat(" disabled");
			}
			else
			{
				sb.AppendFormat(" href='{0}{1}'", Href, BuildQueryString());
			}
			if (Rel != null)
			{
				sb.Append(Rel.CreateQuotedAttribute("rel"));
			}
			if (CssClass != null)
			{
				sb.Append(CssClass.CreateQuotedAttribute("class"));
			}
			if (MouseOverText != null)
			{
				sb.Append(MouseOverText.CreateQuotedAttribute("title"));
			}

			sb.Append('>');
			sb.Append(LinkText.EscapeForHtml());
			sb.Append("</a>");

			return sb.ToString();
		}
	}
}