using System;
using System.Linq.Expressions;

using FluentWebControls.Extensions;
using FluentWebControls.Tools;

namespace FluentWebControls
{
	public static class CheckBox
	{
		public static CheckBoxData For<T>(T source, Expression<Func<T, bool>> getValueAndValidationMetadata)
		{
			bool isChecked = getValueAndValidationMetadata.Compile()(source);
			var checkBoxData = new CheckBoxData(isChecked)
				.WithId(NameUtility.GetPropertyName(getValueAndValidationMetadata));
			return checkBoxData;
		}

		[Obsolete("use CheckBox.For(source, x=>x.Value")]
		public static CheckBoxData For(Expression<Func<bool>> getValueAndValidationMetaData)
		{
			var getValue = getValueAndValidationMetaData.Compile();
			CheckBoxData checkBoxData = new CheckBoxData(getValue())
				.WithId(NameUtility.GetPropertyName(getValueAndValidationMetaData));
			return checkBoxData;
		}
	}
}