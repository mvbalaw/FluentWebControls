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
	public class RegularGridColumn
	{
		public static RegularColumn<T> For<T>(Expression<Func<T, int>> columnName)
		{
			var name = Reflection.GetPropertyName(columnName);
			return For(columnName, name);
		}

		public static RegularColumn<T> For<T>(Expression<Func<T, int?>> columnName)
		{
			var name = Reflection.GetPropertyName(columnName);
			return For(columnName, name);
		}

		public static RegularColumn<T> For<T>(Expression<Func<T, bool>> columnName)
		{
			var name = Reflection.GetPropertyName(columnName);
			return For(columnName, name);
		}

		public static RegularColumn<T> For<T>(Expression<Func<T, string>> columnName)
		{
			var name = Reflection.GetPropertyName(columnName);
			return For(columnName, name);
		}

		public static RegularColumn<T> For<T>(Expression<Func<T, int?>> columnName, string columnHeader)
		{
			return new RegularColumn<T>(t =>
			{
				var getValue = columnName.Compile();
				var i = getValue(t);
				return i == null ? "" : i.ToString();
			}, Reflection.GetPropertyName(columnName), columnHeader);
		}

		public static RegularColumn<T> For<T>(Expression<Func<T, int>> columnName, string columnHeader)
		{
			var getValue = columnName.Compile();
			return new RegularColumn<T>(t => getValue(t).ToString(), Reflection.GetPropertyName(columnName), columnHeader);
		}

		public static RegularColumn<T> For<T>(Expression<Func<T, bool>> columnName, string columnHeader)
		{
			var getValue = columnName.Compile();
			return new RegularColumn<T>(t => getValue(t).ToString(), Reflection.GetPropertyName(columnName), columnHeader);
		}

		public static RegularColumn<T> For<T>(Expression<Func<T, string>> columnName, string columnHeader)
		{
			return new RegularColumn<T>(columnName.Compile(), Reflection.GetPropertyName(columnName), columnHeader);
		}

		public static RegularColumn<T> For<T, TReturn>(Expression<Func<T, TReturn>> columnName, string columnHeader, Func<T, string> columnValue)
		{
			return new RegularColumn<T>(columnValue, Reflection.GetPropertyName(columnName), columnHeader);
		}
	}
}