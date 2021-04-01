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

namespace FluentWebControls
{
	public class GridCommandColumn<T> : GridColumnBuilder
	{
		private readonly Func<T, string> _getItemIdFunction;

		public GridCommandColumn(Func<T, string> getItemIdFunction, string fieldName, string actionName)
			: base("", fieldName)
		{
			_getItemIdFunction = getItemIdFunction;
			ActionName = actionName;
			IsClientSideSortable = false;
		}

		public string ActionName { get; }

		public override GridColumnType Type => GridColumnType.Command;

		public string GetItemId(T item)
		{
			return _getItemIdFunction(item);
		}
	}
}