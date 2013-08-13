//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

namespace FluentWebControls.Extensions
{
	public static class RegularColumnExtensions
	{
		public static RegularColumn<T> AddSorterType<T>(this RegularColumn<T> regularColumn, string sorterType)
		{
			regularColumn.Sorter = sorterType;
			return regularColumn;
		}

		public static RegularColumn<T> AlignCenter<T>(this RegularColumn<T> regularColumn)
		{
			regularColumn.Align = AlignAttribute.Center;
			return regularColumn;
		}

		public static RegularColumn<T> AlignLeft<T>(this RegularColumn<T> regularColumn)
		{
			regularColumn.Align = AlignAttribute.Left;
			return regularColumn;
		}

		public static RegularColumn<T> AlignRight<T>(this RegularColumn<T> regularColumn)
		{
			regularColumn.Align = AlignAttribute.Right;
			return regularColumn;
		}

		public static RegularColumn<T> NotClientSideSortable<T>(this RegularColumn<T> regularColumn)
		{
			regularColumn.IsClientSideSortable = false;
			return regularColumn;
		}
	}
}