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
	public static class GridCommandColumnExtensions
	{
		public static GridCommandColumn<T> AlignCenter<T>(this GridCommandColumn<T> sortableColumn)
		{
			sortableColumn.Align = AlignAttribute.Center;
			return sortableColumn;
		}

		public static GridCommandColumn<T> AlignLeft<T>(this GridCommandColumn<T> sortableColumn)
		{
			sortableColumn.Align = AlignAttribute.Left;
			return sortableColumn;
		}

		public static GridCommandColumn<T> AlignRight<T>(this GridCommandColumn<T> sortableColumn)
		{
			sortableColumn.Align = AlignAttribute.Right;
			return sortableColumn;
		}
	}
}