using System;
using System.Linq.Expressions;
using FluentWebControls.Extensions;
using FluentWebControls.Tools;

namespace FluentWebControls
{
	public static class CheckBox
	{
		public static CheckBoxData For<T>(T source, Expression<Func<T,bool>> getValueAndValidationMetadata)
		{
			bool isChecked = getValueAndValidationMetadata.Compile()(source);
			CheckBoxData checkBoxData = new CheckBoxData(isChecked)
				{
					Id = NameUtility.GetPropertyName(getValueAndValidationMetadata).ToCamelCase()
				};
			return checkBoxData;
		}

		[Obsolete("use CheckBox.For(source, x=>x.Value")]
		public static CheckBoxData For(Expression<Func<bool>> getValue)
		{
			CheckBoxData checkBoxData = new CheckBoxData(getValue.Compile()())
				{
					Id = NameUtility.GetPropertyName(getValue).ToCamelCase()
				};
			return checkBoxData;
		}
	}
}