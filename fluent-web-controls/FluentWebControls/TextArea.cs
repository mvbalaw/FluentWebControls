using System;
using System.Linq.Expressions;

using FluentWebControls.Extensions;
using FluentWebControls.Tools;

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

		[Obsolete("use .For(source, x=>x.Value, x=>x.Value).WithValidationFrom(x=>x.Value)")]
		public static TextAreaData For(Expression<Func<string>> getValueIdAndValidationMetadata)
		{
			var getValue = getValueIdAndValidationMetadata.Compile();
			string value = getValue();
			var textAreaData = new TextAreaData(value)
				.WithId(NameUtility.GetPropertyName(getValueIdAndValidationMetadata))
				.WithValidationFrom(getValueIdAndValidationMetadata);
			return textAreaData;
		}

		[Obsolete("use .For(source, x=>x.Value, x=>x.Value).WithValidationFrom(x=>x.Value)")]
		public static TextAreaData For<T>(T source, Expression<Func<T, string>> getValueIdAndValidationMetadata)
		{
			var getValue = getValueIdAndValidationMetadata.Compile();
			string value = getValue(source);
			var textAreaData = new TextAreaData(value)
				.WithId(NameUtility.GetPropertyName(getValueIdAndValidationMetadata))
				.WithValidationFrom(getValueIdAndValidationMetadata);
			return textAreaData;
		}
	}
}