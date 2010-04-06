using System;
using System.Linq.Expressions;

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public static class TextArea
	{
		public static TextAreaData For<T, K>(T source, Func<T, string> getValue, Expression<Func<T, K>> forId)
		{
			string value = getValue(source);
			var textAreaData = new TextAreaData(value)
				.WithId(forId);
			return textAreaData;
		}
	}
}