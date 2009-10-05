using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace FluentWebControls.Extensions
{
	internal static class IEnumerableTExtensions
	{
		[NotNull]
		public static IEnumerable<T> ForEach<T>([NotNull] this IEnumerable<T> items, [NotNull] Action<T> action)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items", "collection cannot be null");
			}

			foreach (T item in items)
			{
				action(item);
			}
			return items;
		}

		[NotNull]
		public static string Join<T>([CanBeNull] this IEnumerable<T> items, [CanBeNull] string delimiter)
		{
			StringBuilder result = new StringBuilder();
			if (items != null && items.Any())
			{
				delimiter = delimiter ?? "";
				foreach (T item in items)
				{
					result.Append(item);
					result.Append(delimiter);
				}
				result.Length = result.Length - delimiter.Length;
			}
			return result.ToString();
		}
	}
}