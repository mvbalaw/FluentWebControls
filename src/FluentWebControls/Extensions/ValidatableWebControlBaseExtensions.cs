//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using System;
using System.Linq.Expressions;

using MvbaCore.Interfaces;

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