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
using System.ComponentModel;
using System.Linq;
using System.Web;

using JetBrains.Annotations;

namespace FluentWebControls.Extensions
{
	public static class StringExtensions
	{
		public static string CombineWithWebCompatibleSeparator(this string prefix, string suffix)
		{
			return prefix + Constants.WebCompatibleSeparator + suffix;
		}

		public static string CreateQuotedAttribute(this string value, string name)
		{
			return String.Format(" {0}='{1}'", name, EscapeForTagAttribute(value));
		}

		[CanBeNull]
		public static string EmptyToNull([CanBeNull] this string str, bool trimFirst)
		{
			if (str == null)
			{
				return str;
			}
			var value = str;
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
			return value == null ? "" : value.Replace("&", "&amp;").Replace("\"", "&quot;").Replace("<", "&lt;").Replace("\'", "&apos;");
		}

		public static string EscapeForUrl(this string value)
		{
			if (value == null)
			{
				return "";
			}

			var parts = value.Split(' ');
			var result = parts.Select(HttpUtility.UrlEncode).Join("%20");
			return result;
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