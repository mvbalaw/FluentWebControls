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
	public static class SortableGridColumn
	{
		public static SortableColumn<T> For<T, TReturn>(Expression<Func<T, TReturn>> columnName) where TReturn : struct
		{
			var name = Reflection.GetPropertyName(columnName);
			return For(columnName, name);
		}

		public static SortableColumn<T> For<T, TReturn>(Expression<Func<T, TReturn?>> columnName) where TReturn : struct
		{
			var name = Reflection.GetPropertyName(columnName);
			return For(columnName, name);
		}

		public static SortableColumn<T> For<T>(Expression<Func<T, string>> columnName)
		{
			var name = Reflection.GetPropertyName(columnName);
			return For(columnName, name);
		}

		public static SortableColumn<T> For<T>(Expression<Func<T, int?>> columnName, string columnHeader)
		{
			return new SortableColumn<T>(t =>
			{
				var i = columnName.Compile()(t);
				return i == null ? "" : i.ToString();
			}, Reflection.GetPropertyName(columnName), columnHeader);
		}

		public static SortableColumn<T> For<T, TReturn>(Expression<Func<T, TReturn>> columnName, string columnHeader) where TReturn : struct
		{
			return new SortableColumn<T>(t => columnName.Compile()(t).ToString(), Reflection.GetPropertyName(columnName), columnHeader);
		}

		public static SortableColumn<T> For<T, TReturn>(Expression<Func<T, TReturn?>> columnName, string columnHeader) where TReturn : struct
		{
			return new SortableColumn<T>(t => columnName.Compile()(t).ToString(), Reflection.GetPropertyName(columnName), columnHeader);
		}

		public static SortableColumn<T> For<T>(Expression<Func<T, string>> columnName, string columnHeader)
		{
			return new SortableColumn<T>(columnName.Compile(), Reflection.GetPropertyName(columnName), columnHeader);
		}

		public static SortableColumn<T> For<T, TReturn>(Expression<Func<T, TReturn>> columnNameForSorting, string columnHeader, Func<T, string> columnValue)
		{
			return new SortableColumn<T>(columnValue, Reflection.GetPropertyName(columnNameForSorting), columnHeader);
		}
	}
}