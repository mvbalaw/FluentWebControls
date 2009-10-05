using System;
using System.Linq.Expressions;
using FluentWebControls.Extensions;
using FluentWebControls.Tools;

namespace FluentWebControls
{
	public static class CheckBox
	{
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