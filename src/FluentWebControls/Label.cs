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
	public static class Label
	{
		public static LabelData For(Expression<Func<string>> id)
		{
			return new LabelData(Reflection.GetPropertyName(id).ToCamelCase());
		}

		public static LabelData For<T>(Expression<Func<T>> id) where T : struct
		{
			return new LabelData(Reflection.GetPropertyName(id).ToCamelCase());
		}

		public static LabelData For<T>(Expression<Func<T?>> id) where T : struct
		{
			return new LabelData(Reflection.GetPropertyName(id).ToCamelCase());
		}

		public static LabelData For(string id)
		{
			return new LabelData(id);
		}

		public static LabelData ForIt()
		{
			return new LabelData();
		}
	}
}