using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

namespace FluentWebControls.Extensions
{
	public static class KeyValuePairExtensions
	{
		public static string ToQueryString([CanBeNull] this IEnumerable<KeyValuePair<string, string>> parameters)
		{
			if (parameters == null)
			{
				return "";
			}
			if (parameters.Any(x => x.Key.IsNullOrEmpty(true)))
			{
				throw new ArgumentException("Keys cannot be null");
			}
			return parameters.Select(x => x.Key.EscapeForUrl() + "=" + x.Value.EscapeForUrl()).Join("&");
		}
	}
}