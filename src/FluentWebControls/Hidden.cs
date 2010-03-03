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

		[Obsolete("use Hidden.For(source, x=>x.Value)")]
		public static HiddenData For(Expression<Func<string>> forIdAndValue)
		{
			var getValue = forIdAndValue.Compile();
			return new HiddenData().WithId(forIdAndValue).Text(getValue());
		}

		public static HiddenData For<T, K>(Expression<Func<T, K>> forId)
		{
			return new HiddenData().WithId(forId);
		}

		[Obsolete("use Hidden.For(source, x=>x.Value.ToString(), x=>x.Value)")]
		public static HiddenData For<T>(Expression<Func<T>> forIdAndValue) where T : struct
		{
			var getValue = forIdAndValue.Compile();
			return new HiddenData().WithId(forIdAndValue).Text(getValue().ToString());
		}

		[Obsolete("use Hidden.For(source, x=>x.Value==null?\"\":x.Value.ToString(), x=>x.Value)")]
		public static HiddenData For<T>(Expression<Func<T?>> forIdAndValue) where T : struct
		{
			var getValue = forIdAndValue.Compile();
			var value = getValue();
			return new HiddenData().WithId(forIdAndValue).Text(value == null ? "" : value.ToString());
		}
	}
}