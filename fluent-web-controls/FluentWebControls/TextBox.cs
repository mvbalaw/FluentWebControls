using System;
using System.Linq.Expressions;
using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;
using FluentWebControls.Tools;

namespace FluentWebControls
{
	public static class TextBox
	{
		public static TextBoxData For<T>(Expression<Func<T>> nullableParent, Expression<Func<T, string>> getValue) where T : class
		{
			Func<T> nullable = nullableParent.Compile();
			T parent = nullable();
			string value = null;
			if (parent != null)
			{
				value = getValue.Compile()(parent);
			}
			string name = NameUtility.GetCamelCaseMultiLevelPropertyName(NameUtility.GetPropertyName(nullableParent), NameUtility.GetPropertyName(getValue));

			IPropertyMetaData propertyMetaData = IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(getValue);
			propertyMetaData.Combine(IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(nullableParent));
			TextBoxData textBoxData = new TextBoxData(value, propertyMetaData)
				{
					Id = name.ToCamelCase()
				};
			return textBoxData;
		}

		public static TextBoxData For(Expression<Func<string>> getValue)
		{
			TextBoxData textBoxData = new TextBoxData(getValue.Compile()(), IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(getValue))
				{
					Id = NameUtility.GetPropertyName(getValue).ToCamelCase()
				};
			return textBoxData;
		}

		public static TextBoxData For<T>(Expression<Func<T>> getValue) where T : struct
		{
			TextBoxData textBoxData = new TextBoxData(getValue.Compile()().ToString(), IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(getValue))
				{
					Id = NameUtility.GetPropertyName(getValue).ToCamelCase()
				};
			return textBoxData;
		}

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