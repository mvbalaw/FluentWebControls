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

using MvbaCore;

namespace FluentWebControls
{
	public static class CommandGridColumn
	{
		public static GridCommandColumn<T> For<T, TController>(Expression<Func<T, string>> getValueAndPropertyName, Expression<Func<TController, object>> targetControllerAction) where T : class
		{
			return new GridCommandColumn<T>(getValueAndPropertyName.Compile(), Reflection.GetPropertyName(getValueAndPropertyName).ToCamelCase(), Reflection.GetMethodName(targetControllerAction));
		}

		public static GridCommandColumn<T> For<T, TController>(Func<T, string> getValue, Expression<Func<T, object>> forQueryStringId, Expression<Func<TController, object>> targetControllerAction) where T : class
		{
			return new GridCommandColumn<T>(getValue, Reflection.GetPropertyName(forQueryStringId).ToCamelCase(), Reflection.GetMethodName(targetControllerAction));
		}
	}
}