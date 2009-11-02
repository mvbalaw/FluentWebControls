using System;
using System.ComponentModel;
using System.Web;
using JetBrains.Annotations;

namespace FluentWebControls.Extensions
{
	public static class StringExtensions
	{
		[NotNull]
		public static string EscapeForHtml(this string input)
		{
			if (input == null)
			{
				return "";
			}
			return HttpUtility.HtmlEncode(input);
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
	}
}