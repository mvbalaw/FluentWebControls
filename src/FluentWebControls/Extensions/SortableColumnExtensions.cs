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
	public static class SortableColumnExtensions
	{
		public static SortableColumn<T> AlignCenter<T>(this SortableColumn<T> sortableColumn)
		{
			sortableColumn.Align = AlignAttribute.Center;
			return sortableColumn;
		}

		public static SortableColumn<T> AlignLeft<T>(this SortableColumn<T> sortableColumn)
		{
			sortableColumn.Align = AlignAttribute.Left;
			return sortableColumn;
		}

		public static SortableColumn<T> AlignRight<T>(this SortableColumn<T> sortableColumn)
		{
			sortableColumn.Align = AlignAttribute.Right;
			return sortableColumn;
		}

		public static SortableColumn<T> IsDefaultSortColumn<T>(this SortableColumn<T> sortableColumn)
		{
			sortableColumn.IsDefaultSortColumn = true;
			return sortableColumn;
		}

		public static SortableColumn<T> NotClientSideSortable<T>(this SortableColumn<T> sortableColumn)
		{
			sortableColumn.IsClientSideSortable = false;
			return sortableColumn;
		}
	}
}