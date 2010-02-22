using System;
using System.Linq.Expressions;

using FluentWebControls.Extensions;

using MvbaCore;

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

		[Obsolete("use CheckBox.For(source, x=>x.Value")]
		public static CheckBoxData For(Expression<Func<bool>> forCheckedAndId)
		{
			var getValue = forCheckedAndId.Compile();
			var checkBoxData = new CheckBoxData(getValue())
				.WithId(Reflection.GetPropertyName(forCheckedAndId));
			return checkBoxData;
		}
	}
}