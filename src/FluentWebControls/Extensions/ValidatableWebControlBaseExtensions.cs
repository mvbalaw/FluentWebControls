using System;
using System.Linq.Expressions;

using FluentWebControls.Interfaces;

namespace FluentWebControls.Extensions
{
	public static class ValidatableWebControlBaseExtensions
	{
		public static T WithValidationFrom<T>(this T webControl, IPropertyMetaData propertyMetaData) where T : ValidatableWebControlBase
		{
			webControl.PropertyMetaData = propertyMetaData;
			return webControl;
		}

		public static T WithValidationFrom<T, TFuncReturn>(this T webControl, Expression<Func<TFuncReturn>> forValidationMetadata) where T : ValidatableWebControlBase
		{
			var factory = Configuration.ValidationMetaDataFactory;
			if (factory != null)
			{
				var propertyMetaData = factory.GetFor(forValidationMetadata);
				webControl.PropertyMetaData = propertyMetaData;
			}
			return webControl;
		}

		public static T WithValidationFrom<T, TInput, TFuncReturn>(this T webControl, Expression<Func<TInput, TFuncReturn>> forValidationMetadata) where T : ValidatableWebControlBase
		{
			var factory = Configuration.ValidationMetaDataFactory;
			if (factory != null)
			{
				var propertyMetaData = factory.GetFor(forValidationMetadata);
				webControl.PropertyMetaData = propertyMetaData;
			}
			return webControl;
		}
	}
}