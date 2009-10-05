using System;
using System.Linq.Expressions;
using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;
using FluentWebControls.Tools;

namespace FluentWebControls
{
	public static class TextArea
	{
		public static TextAreaData For(Expression<Func<string>> getValue)
		{
			TextAreaData textAreaData = new TextAreaData(getValue.Compile()(), IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(getValue))
				{
					Id = NameUtility.GetPropertyName(getValue).ToCamelCase()
				};
			return textAreaData;
		}
	}
}