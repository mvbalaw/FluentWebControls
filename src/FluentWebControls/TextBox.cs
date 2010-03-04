using System;
using System.Linq.Expressions;

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public static class TextBox
	{
		public static TextBoxData For<T, K>(T source, Func<T, string> getValue, Expression<Func<T, K>> forId)
		{
			var textBoxData = new TextBoxData(getValue(source))
				.WithId(forId);
			return textBoxData;
		}

		public static TextBoxData For<T>(T source, Expression<Func<T, string>> forValueAndId)
		{
			var getValue = forValueAndId.Compile();
			return For(source, getValue, forValueAndId);
		}
	}
}