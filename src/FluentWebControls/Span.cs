using System;
using System.Linq.Expressions;

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public static class Span
	{
		public static SpanData For<T, K>(Expression<Func<T, K>> forId, string value)
		{
			return new SpanData(value).WithId(forId);
		}

		public static SpanData For(string value)
		{
			return new SpanData(value);
		}
	}
}