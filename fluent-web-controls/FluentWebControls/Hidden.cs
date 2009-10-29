using System;
using System.Linq.Expressions;

using FluentWebControls.Extensions;
using FluentWebControls.Tools;

namespace FluentWebControls
{
	public static class Hidden
	{
		public static HiddenData For<T, K>(T source, Func<T, string> getValue, Expression<Func<T, K>> getNameAndMetadata)
		{
			string name = NameUtility.GetPropertyName(getNameAndMetadata).ToCamelCase();
			string value = getValue(source);
			return new HiddenData(name).Text(value);
		}

		public static HiddenData For<T>(T source, Expression<Func<T, string>> getValueAndValidationMetadata)
		{
			return For(source, getValueAndValidationMetadata.Compile(), getValueAndValidationMetadata);
		}

		[Obsolete("use Hidden.For(source, x=>x.Value)")]
		public static HiddenData For(Expression<Func<string>> id)
		{
			return new HiddenData(NameUtility.GetPropertyName(id).ToCamelCase()).Text(id.Compile()());
		}

		public static HiddenData For<T,K>(Expression<Func<T,K>> id)
		{
			return new HiddenData(NameUtility.GetPropertyName(id).ToCamelCase());
		}

		[Obsolete("use Hidden.For(source, x=>x.Value.ToString(), x=>x.Value)")]
		public static HiddenData For<T>(Expression<Func<T>> id) where T : struct
		{
			return new HiddenData(NameUtility.GetPropertyName(id).ToCamelCase()).Text(id.Compile()().ToString());
		}

		[Obsolete("use Hidden.For(source, x=>x.Value==null?\"\":x.Value.ToString(), x=>x.Value)")]
		public static HiddenData For<T>(Expression<Func<T?>> id) where T : struct
		{
			var value = id.Compile()();
			return new HiddenData(NameUtility.GetPropertyName(id).ToCamelCase()).Text(value == null ? "" : value.ToString());
		}
	}
}