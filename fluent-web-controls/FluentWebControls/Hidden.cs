using System;
using System.Linq.Expressions;
using FluentWebControls.Extensions;
using FluentWebControls.Tools;

namespace FluentWebControls
{
	public static class Hidden
	{
		public static HiddenData For(Expression<Func<string>> id)
		{
			return new HiddenData(NameUtility.GetPropertyName(id).ToCamelCase()).Text(id.Compile()());
		}

		public static HiddenData For<T>(Expression<Func<T>> id) where T : struct
		{
			return new HiddenData(NameUtility.GetPropertyName(id).ToCamelCase()).Text(id.Compile()().ToString());
		}

		public static HiddenData For<T>(Expression<Func<T?>> id) where T : struct
		{
			T? value = id.Compile()();
			return new HiddenData(NameUtility.GetPropertyName(id).ToCamelCase()).Text(value == null ? "" : value.ToString());
		}
	}
}