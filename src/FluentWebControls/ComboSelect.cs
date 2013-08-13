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
	public static class ComboSelect
	{
		public static ComboSelectData For<TListItemType, TContainerType, TPropertyType>(IEnumerable<TListItemType> itemSource,
			Func<TListItemType, string>
				getListItemDisplayText,
			Func<TListItemType, string>
				getListItemValue,
			Expression
				<Func<TContainerType, TPropertyType>>
				forId)
		{
			var options =
				itemSource.Select(item => new KeyValuePair<string, string>(getListItemDisplayText(item), getListItemValue(item))).
					ToList();
			var comboSelectData = new ComboSelectData(options)
				.WithId(forId);
			return comboSelectData;
		}

		public static ComboSelectData For<TListItemType, TPropertyType>(IEnumerable<TListItemType> itemSource,
			Func<TListItemType, string> getListItemDisplayText,
			Func<TListItemType, string> getListItemValue,
			Expression<Func<TPropertyType>> forId)
		{
			var options =
				itemSource.Select(item => new KeyValuePair<string, string>(getListItemDisplayText(item), getListItemValue(item))).
					ToList();
			var comboSelectData = new ComboSelectData(options)
				.WithId(forId);
			return comboSelectData;
		}
	}
}