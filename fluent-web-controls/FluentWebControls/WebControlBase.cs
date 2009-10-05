using System;
using System.Web;
using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public abstract class WebControlBase
	{
		public string CreateQuotedAttribute<T>(string name, T? value) where T : struct
		{
			return CreateQuotedAttribute(name, value == null ? "" : value.ToString());
		}

		public string CreateQuotedAttribute<T>(string name, T value) where T : struct
		{
			return CreateQuotedAttribute(name, value.ToString());
		}

		public string CreateQuotedAttribute(string name, string value)
		{
			return String.Format(" {0}='{1}'", name, EscapeForTagAttribute(value));
		}

		public string EscapeForHtml(string value)
		{
			return value.EscapeForHtml();
		}

		public string EscapeForTagAttribute<T>(T value) where T : struct
		{
			return EscapeForTagAttribute(value.ToString());
		}

		public string EscapeForTagAttribute<T>(T? value) where T : struct
		{
			return value == null ? "" : EscapeForTagAttribute(value.ToString());
		}

		public string EscapeForTagAttribute(string value)
		{
			return value == null ? "" : value.Replace("&", "&amp;").Replace("\"", "&quot;").Replace("<", "&lt;");
		}

		public string EscapeForUrl(string value)
		{
			return HttpUtility.UrlEncode(value);
		}
	}
}