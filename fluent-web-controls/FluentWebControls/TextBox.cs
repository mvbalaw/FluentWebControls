using System;
using System.Linq.Expressions;
using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;
using FluentWebControls.Tools;

namespace FluentWebControls
{
	public static class TextBox
	{
		public static TextBoxData For<T,K>(T source, Func<T,string> getValue, Expression<Func<T,K>> forNameAndValidationMetadata)
		{
			IPropertyMetaData propertyMetaData = IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(forNameAndValidationMetadata);
			TextBoxData textBoxData = new TextBoxData(getValue(source), propertyMetaData)
				{
					Id = NameUtility.GetPropertyName(forNameAndValidationMetadata).ToCamelCase()
				};
			return textBoxData;
		}
		public static TextBoxData For<T>(T source, Expression<Func<T,string>> getValueAndValidationMetadata)
		{
			return For(source, getValueAndValidationMetadata.Compile(), getValueAndValidationMetadata);
		}

		[Obsolete("use TextBox.For(source, x=>x.Value)")]
		public static TextBoxData For(Expression<Func<string>> getValue)
		{
			IPropertyMetaData propertyMetaData = IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(getValue);
			TextBoxData textBoxData = new TextBoxData(getValue.Compile()(), propertyMetaData)
				{
					Id = NameUtility.GetPropertyName(getValue).ToCamelCase()
				};
			return textBoxData;
		}

		[Obsolete("use TextBox.For(source, x=>x.Value.ToString(), x=>x.Value)")]
		public static TextBoxData For<T>(Expression<Func<T>> getValue) where T : struct
		{
			TextBoxData textBoxData = new TextBoxData(getValue.Compile()().ToString(), IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(getValue))
				{
					Id = NameUtility.GetPropertyName(getValue).ToCamelCase()
				};
			return textBoxData;
		}

		[Obsolete("use TextBox.For(source, x=>x.Value==null?\"\":x.Value.ToString(), x=>x.Value)")]
		public static TextBoxData For<T>(Expression<Func<T?>> getValue) where T : struct
		{
			T? value = getValue.Compile()();
			TextBoxData textBoxData = new TextBoxData(value == null ? "" : value.ToString(), IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(getValue))
				{
					Id = NameUtility.GetPropertyName(getValue).ToCamelCase()
				};
			return textBoxData;
		}
	}
}