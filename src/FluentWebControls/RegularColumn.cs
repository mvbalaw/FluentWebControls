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
	public class RegularColumn<T> : GridColumnBuilder
	{
		private readonly Func<T, string> _getItemValueFunction;

		public RegularColumn(Func<T, string> getItemValueFunction, string fieldName, string columnHeader)
			: base(columnHeader, fieldName)
		{
			_getItemValueFunction = getItemValueFunction;
		}

		public override GridColumnType Type => GridColumnType.Regular;

		public string GetItemValue(T item)
		{
			return _getItemValueFunction(item);
		}
	}
}