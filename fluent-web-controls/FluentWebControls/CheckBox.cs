using System;
using System.Linq.Expressions;

using FluentWebControls.Extensions;
using FluentWebControls.Tools;

namespace FluentWebControls
{
	public static class CheckBox
	{
		public static CheckBoxData For<T>(T source, bool value, Expression<Func<T, bool>> forId)
		{
			bool isChecked = value;
			var checkBoxData = new CheckBoxData(isChecked)
				.WithId(forId);
			return checkBoxData;
		}

		public static CheckBoxData For<T>(T source, Expression<Func<T, bool>> forValueAndId)
		{
			var getValue = forValueAndId.Compile();
			bool isChecked = getValue(source);
			var checkBoxData = new CheckBoxData(isChecked)
				.WithId(forValueAndId);
			return checkBoxData;
		}

		[Obsolete("use CheckBox.For(source, x=>x.Value")]
		public static CheckBoxData For(Expression<Func<bool>> forValueAndId)
		{
			var getValue = forValueAndId.Compile();
			CheckBoxData checkBoxData = new CheckBoxData(getValue())
				.WithId(NameUtility.GetPropertyName(forValueAndId));
			return checkBoxData;
		}
	}
}