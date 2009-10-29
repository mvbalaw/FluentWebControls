using System;
using System.Linq.Expressions;
using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;
using FluentWebControls.Tools;

namespace FluentWebControls
{
	public static class TextArea
	{
		public static TextAreaData For(Expression<Func<string>> getValueAndValidationMetadata)
		{
			IPropertyMetaData propertyMetaData = IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(getValueAndValidationMetadata);
			string value = getValueAndValidationMetadata.Compile()();
			TextAreaData textAreaData = new TextAreaData(value, propertyMetaData)
				{
					Id = NameUtility.GetPropertyName(getValueAndValidationMetadata).ToCamelCase()
				};
			return textAreaData;
		}

		public static TextAreaData For<T>(T source, Expression<Func<T, string>> getValueAndValidationMetadata)
		{
			IPropertyMetaData propertyMetaData = IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(getValueAndValidationMetadata);
			string value = getValueAndValidationMetadata.Compile()(source);
			TextAreaData textAreaData = new TextAreaData(value, propertyMetaData)
				{
					Id = NameUtility.GetPropertyName(getValueAndValidationMetadata).ToCamelCase()
				};
			return textAreaData;
		}
	}
}