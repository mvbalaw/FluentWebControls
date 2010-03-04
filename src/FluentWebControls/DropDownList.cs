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