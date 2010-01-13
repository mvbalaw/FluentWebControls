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

		[Obsolete("use TextBox.For(source, x=>x.Value).WithValidationFrom(x=>x.Value)")]
		public static TextBoxData For(Expression<Func<string>> forValueIdAndValidationMetadata)
		{
			var getvalue = forValueIdAndValidationMetadata.Compile();
			var textBoxData = new TextBoxData(getvalue())
				.WithId(forValueIdAndValidationMetadata)
				.WithValidationFrom(forValueIdAndValidationMetadata);
			return textBoxData;
		}

		[Obsolete("use TextBox.For(source, x=>x.Value.ToString(), x=>x.Value).WithValidationFrom(x=>x.Value)")]
		public static TextBoxData For<T>(Expression<Func<T>> forValueIdAndValidationMetadata) where T : struct
		{
			var getValue = forValueIdAndValidationMetadata.Compile();
			var textBoxData = new TextBoxData(getValue().ToString())
				.WithId(forValueIdAndValidationMetadata)
				.WithValidationFrom(forValueIdAndValidationMetadata);
			return textBoxData;
		}

		[Obsolete("use TextBox.For(source, x=>x.Value==null?\"\":x.Value.ToString(), x=>x.Value).WithValidationFrom(x=>x.Value)")]
		public static TextBoxData For<T>(Expression<Func<T?>> forValueIdAndValidationMetadata) where T : struct
		{
			var getValue = forValueIdAndValidationMetadata.Compile();
			var value = getValue();
			string v = value == null ? "" : value.ToString();
			var textBoxData = new TextBoxData(v)
				.WithId(forValueIdAndValidationMetadata)
				.WithValidationFrom(forValueIdAndValidationMetadata);
			return textBoxData;
		}
	}
}