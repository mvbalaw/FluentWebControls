using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public interface ILinkData
	{
		string Alt { get; }
		string ControllerExtension { get; }
		string CssClass { get; }
		bool Disabled { get; }
		string Href { get; }
		string Id { get; }
		string IdWithPrefix { get; }
		string ImageUrl { get; }
		string LinkText { get; }
		string MouseOverText { get; }
		string Rel { get; }
		string Url { get; }
	}

	public class LinkData : WebControlBase, ILinkData
	{
		private readonly Dictionary<string, string> _queryStringData = new Dictionary<string, string>();
		private readonly List<string> _urlParameters = new List<string>();
		private string _url;

		public LinkData()
		{
			Visible = true;
		}

		internal string Alt { private get; set; }
		internal string ControllerExtension { private get; set; }
		internal string CssClass { private get; set; }
		internal bool Disabled { private get; set; }
		internal string Id { private get; set; }
		internal string ImageUrl { private get; set; }
		internal string LinkText { private get; set; }
		internal string MouseOverText { private get; set; }
		internal string Rel { private get; set; }
		internal string Url
		{
			private get
			{
				if (ControllerExtension == null || _url.IsNullOrEmpty())
				{
					return _url;
				}
				var parts = _url.Split('/');
				if (parts.Length == 1)
				{
					parts[0] += ControllerExtension;
				}
				else
				{
					parts[parts.Length - 1 - 1] += ControllerExtension;
				}
				return parts.Join("/");
			}
			set { _url = value; }
		}
		public bool Visible { private get; set; }
		string ILinkData.ControllerExtension
		{
			get { return ControllerExtension; }
		}
		string ILinkData.CssClass
		{
			get { return CssClass; }
		}
		bool ILinkData.Disabled
		{
			get { return Disabled; }
		}
		string ILinkData.IdWithPrefix
		{
			get { return IdWithPrefix; }
		}
		string ILinkData.Url
		{
			get { return Url; }
		}
		string ILinkData.Alt
		{
			get { return Alt; }
		}

		public string Href
		{
			get { return Url + BuildUrlParameters() + BuildQueryString(); }
		}

		string ILinkData.Id
		{
			get { return Id; }
		}

		string ILinkData.LinkText
		{
			get { return LinkText; }
		}

		string ILinkData.ImageUrl
		{
			get { return ImageUrl; }
		}

		string ILinkData.MouseOverText
		{
			get { return MouseOverText; }
		}

		string ILinkData.Rel
		{
			get { return Rel; }
		}

		public void AddQueryStringData(string key, string value)
		{
			_queryStringData.Add(key, value);
		}

		public void AddUrlParameters(string parameter)
		{
			_urlParameters.Add(parameter);
		}

		public void AddUrlParameters(List<string> parameters)
		{
			_urlParameters.AddRange(parameters);
		}

		private string BuildQueryString()
		{
			if (_queryStringData.Count == 0)
			{
				return "";
			}
			var sb = new StringBuilder();
			var keylessItems = _queryStringData.Where(x => x.Key == "").ToList();
			foreach (var item in keylessItems)
			{
				sb.Append('/')
					.Append(item.Value);
			}

			var keyedItems = _queryStringData.Where(x => x.Key.Length > 0);
			if (keyedItems.Any())
			{
				sb.Append('?');
				foreach (var keyValuePair in keyedItems)
				{
					sb.AppendFormat("{0}={1}&", keyValuePair.Key.EscapeForUrl(), keyValuePair.Value.EscapeForUrl());
				}
			}
			return sb.ToString();
		}

		private string BuildUrlParameters()
		{
			if (_urlParameters.Count <= 0)
			{
				return "";
			}
			return String.Format("/{0}", _urlParameters.Join("/"));
		}

		public override string ToString()
		{
			if (!Visible)
			{
				return "";
			}

			var sb = new StringBuilder();
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
				sb.AppendFormat(" href='{0}{1}{2}'", Url, BuildUrlParameters(), BuildQueryString());
			}
			if (Rel != null)
			{
				sb.Append(Rel.CreateQuotedAttribute("rel"));
			}
			if (CssClass != null)
			{
				sb.Append(CssClass.CreateQuotedAttribute("class"));
			}
			sb.Append(Data);
			if (MouseOverText != null)
			{
				sb.Append(MouseOverText.CreateQuotedAttribute("title"));
			}

			sb.Append('>');
			if (ImageUrl != null)
			{
				sb.Append("<img src='" + ImageUrl + "' alt='" + Alt + "'/>");
			}
			else
			{
				sb.Append(LinkText.EscapeForHtml());
			}
			sb.Append("</a>");
			return sb.ToString();
		}
	}
}