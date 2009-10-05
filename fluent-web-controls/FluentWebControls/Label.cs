using System;
using System.Linq.Expressions;
using FluentWebControls.Extensions;
using FluentWebControls.Tools;

namespace FluentWebControls
{
	public static class Label
	{
		public static LabelData For(Expression<Func<string>> id)
		{
			return new LabelData(NameUtility.GetPropertyName(id).ToCamelCase());
		}

		public static LabelData For<T>(Expression<Func<T>> id) where T : struct
		{
			return new LabelData(NameUtility.GetPropertyName(id).ToCamelCase());
		}

		public static LabelData For<T>(Expression<Func<T?>> id) where T : struct
		{
			return new LabelData(NameUtility.GetPropertyName(id).ToCamelCase());
		}

		public static LabelData For(string id)
		{
			return new LabelData(id);
		}

		public static LabelData ForIt()
		{
			return new LabelData();
		}
	}
}