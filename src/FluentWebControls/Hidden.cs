using System;
using System.Linq.Expressions;

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public static class Hidden
	{
		public static HiddenData For<T, K>(T source, Func<T, string> getValue, Expression<Func<T, K>> forId)
		{
			string value = getValue(source);
			return new HiddenData().WithId(forId).Text(value);
		}

		public static HiddenData For<T>(T source, Expression<Func<T, string>> forIdAndValue)
		{
			var getValue = forIdAndValue.Compile();
			return For(source, getValue, forIdAndValue);
		}

		public static HiddenData For<T, K>(Expression<Func<T, K>> forId)
		{
			return new HiddenData().WithId(forId);
		}
	}
}