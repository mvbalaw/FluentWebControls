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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public static class DropDownList
	{
		public static DropDownListData For<TListItemType, TContainerType, TPropertyType>(IEnumerable<TListItemType> listItemSource, Func<TListItemType, string> getListItemDisplayText, Func<TListItemType, string> getListItemValue, Expression<Func<TContainerType, TPropertyType>> forId)
		{
			var options = listItemSource
				.Select(item => new KeyValuePair<string, string>(getListItemDisplayText(item), getListItemValue(item)))
				.ToList();
			var dropDownListData = new DropDownListData(options)
				.WithId(forId);
			return dropDownListData;
		}

		public static DropDownListData For<TListItemType, TPropertyType>(IEnumerable<TListItemType> listItemSource, Func<TListItemType, string> getListItemDisplayText, Func<TListItemType, string> getListItemValue, Expression<Func<TPropertyType>> forId)
		{
			var options = listItemSource
				.Select(item => new KeyValuePair<string, string>(getListItemDisplayText(item), getListItemValue(item)))
				.ToList();
			var dropDownListData = new DropDownListData(options)
				.WithId(forId);
			return dropDownListData;
		}
	}
}