using System;
using System.ComponentModel;
using System.Web;

using JetBrains.Annotations;

namespace FluentWebControls.Extensions
{
	public static class StringExtensions
	{
		public static string CreateQuotedAttribute(this string value, string name)
		{
			return String.Format(" {0}=\"{1}\"", name, EscapeForTagAttribute(value));
		}

		[CanBeNull]
		public static string EmptyToNull([CanBeNull] this string str, bool trimFirst)
		{
			if (str == null)
			{
				return str;
			}
			string value = str;
			if (trimFirst)
			{
				value = str.Trim();
			}
			return value.Length == 0 ? null : str;
		}

		[NotNull]
		public static string EscapeForHtml(this string input)
		{
			if (input == null)
			{
				return "";
			}
			return HttpUtility.HtmlEncode(input);
		}

		public static string EscapeForTagAttribute(this string value)
		{
			return value == null ? "" : value.Replace("&", "&amp;").Replace("\"", "\\\"").Replace("<", "&lt;").Replace(">", "&gt;");
		}

		public static string EscapeForUrl(this string value)
		{
			return HttpUtility.UrlEncode(value);
		}

		public static bool IsNullOrEmpty(this string item)
		{
			return item == null ? true : item.Length == 0 ? true : false;
		}

		public static string ToCamelCase([CanBeNull] this string str)
		{
			if (String.IsNullOrEmpty(str))
			{
				return str;
			}
			str = Char.ToLower(str[0]) + str.Substring(1);
			return str; //.Replace(".", "_");
		}

		[NotNull]
		public static ListSortDirection ToSortDirection([CanBeNull] this string sortDirection)
		{
			if (String.IsNullOrEmpty(sortDirection))
			{
				return ListSortDirection.Ascending;
			}
			switch (sortDirection.ToLower())
			{
				case "desc":
				case "descending":
					return ListSortDirection.Descending;
				default:
					return ListSortDirection.Ascending;
			}
		}
	}
}