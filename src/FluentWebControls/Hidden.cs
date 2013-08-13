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

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public static class Hidden
	{
		public static HiddenData For<T, K>(T source, Func<T, string> getValue, Expression<Func<T, K>> forId)
		{
			var value = getValue(source);
			return new HiddenData().WithId(forId).WithValue(value);
		}

		public static HiddenData For<T>(T source, Expression<Func<T, string>> forIdAndValue)
		{
			var getValue = forIdAndValue.Compile();
			return For(source, getValue, forIdAndValue);
		}

		public static HiddenData For<T, K>(Expression<Func<T, K>> forId)
		{
			return new HiddenData().WithId(forId);
		}
	}
}