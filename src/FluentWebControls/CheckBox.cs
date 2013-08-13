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
	public static class CheckBox
	{
		public static CheckBoxData For<TSource, TModel>(TSource source, bool @checked, Func<TSource, string> getValue, Expression<Func<TModel, object>> forId)
		{
			var checkBoxData = new CheckBoxData(@checked)
				.WithValue(getValue(source))
				.WithId(forId);
			return checkBoxData;
		}

		public static CheckBoxData For<T>(T source, bool @checked, Expression<Func<T, object>> forId)
		{
			var checkBoxData = new CheckBoxData(@checked)
				.WithId(forId);
			return checkBoxData;
		}

		public static CheckBoxData For<T>(T source, Expression<Func<T, bool>> forCheckedAndId)
		{
			var getValue = forCheckedAndId.Compile();
			var isChecked = getValue(source);
			var checkBoxData = new CheckBoxData(isChecked)
				.WithId(forCheckedAndId);
			return checkBoxData;
		}
	}
}