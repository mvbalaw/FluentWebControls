using System;
using System.Linq.Expressions;

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public static class CheckBox
	{
		public static CheckBoxData For<TSource, TModel>(TSource source, bool @checked, Func<TSource, string> getValue, Expression<Func<TModel, object>> forId)
		{
			var checkBoxData = new CheckBoxData(@checked)
				.WithValue(getValue(source))
				.WithId(forId);
			return checkBoxData;
		}

		public static CheckBoxData For<T>(T source, bool @checked, Expression<Func<T, object>> forId)
		{
			var checkBoxData = new CheckBoxData(@checked)
				.WithId(forId);
			return checkBoxData;
		}

		public static CheckBoxData For<T>(T source, Expression<Func<T, bool>> forCheckedAndId)
		{
			var getValue = forCheckedAndId.Compile();
			bool isChecked = getValue(source);
			var checkBoxData = new CheckBoxData(isChecked)
				.WithId(forCheckedAndId);
			return checkBoxData;
		}
	}
}