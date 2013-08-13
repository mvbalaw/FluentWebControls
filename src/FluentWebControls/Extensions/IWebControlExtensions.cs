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

using Microsoft.Build.Framework.XamlTypes;

using MvbaCore;

namespace FluentWebControls.Extensions
{
	public static class IWebControlExtensions
	{
		public static T WithData<T>(this T webControl, string name, string value) where T : IWebControl
		{
			webControl.Data = new NameValuePair
			                  {
				                  Name = name,
				                  Value = value
			                  };
			return webControl;
		}

		public static T WithId<T>(this T webControl, string id) where T : IWebControl
		{
			webControl.Id = id;
			return webControl;
		}

		public static T WithId<T, TFuncResult>(this T webControl, Expression<Func<TFuncResult>> id) where T : IWebControl
		{
			return webControl.WithId(Reflection.GetPropertyName(id));
		}

		public static T WithId<T, TFuncInput, TFuncResult>(this T webControl, Expression<Func<TFuncInput, TFuncResult>> id)
			where T : IWebControl
		{
			return webControl.WithId(Reflection.GetPropertyName(id));
		}

		public static T WithIdPrefix<T>(this T webControl, string idPrefix) where T : IWebControl
		{
			webControl.IdPrefix = idPrefix.ToCamelCase();
			return webControl;
		}

		public static T WithName<T>(this T webControl, string name) where T : IWebControl
		{
			webControl.Name = name;
			return webControl;
		}

		public static T WithName<T, TFuncInput, TFuncResult>(this T webControl, Expression<Func<TFuncInput, TFuncResult>> name)
			where T : IWebControl
		{
			return webControl.WithName(Reflection.GetPropertyName(name));
		}

		public static T WithNamePrefix<T>(this T webControl, string namePrefix) where T : IWebControl
		{
			webControl.NamePrefix = namePrefix.ToCamelCase();
			return webControl;
		}
	}
}