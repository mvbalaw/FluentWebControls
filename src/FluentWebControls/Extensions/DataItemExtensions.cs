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

namespace FluentWebControls.Extensions
{
	public static class DataItemExtensions
	{
		public static DataItem<T> AlignCenter<T>(this DataItem<T> dataItem)
		{
			dataItem.Align = AlignAttribute.Center;
			return dataItem;
		}

		public static DataItem<T> AlignRight<T>(this DataItem<T> dataItem)
		{
			dataItem.Align = AlignAttribute.Right;
			return dataItem;
		}

		public static DataItem<T> AsHidden<T>(this DataItem<T> dataItem, Func<T, string> forId)
		{
			dataItem.ColumnTextType = ColumnTextType.Hidden;
			dataItem.GetItemId = forId;
			return dataItem;
		}

		public static DataItem<T> AsHidden<T>(this DataItem<T> dataItem, string forId)
		{
			dataItem.ColumnTextType = ColumnTextType.Hidden;
			dataItem.InputTextId = forId;
			return dataItem;
		}

		public static DataItem<T> AsSpan<T>(this DataItem<T> dataItem, Func<T, string> forId)
		{
			dataItem.ColumnTextType = ColumnTextType.Span;
			dataItem.GetItemId = forId;
			return dataItem;
		}

		public static DataItem<T> AsTextBox<T>(this DataItem<T> dataItem, Func<T, string> forId)
		{
			dataItem.ColumnTextType = ColumnTextType.TextBox;
			dataItem.GetItemId = forId;
			return dataItem;
		}

		public static DataItem<T> WithContainerCssClass<T>(this DataItem<T> dataItem, string cssClass)
		{
			dataItem.ContainerCssClass = cssClass;
			return dataItem;
		}

		public static DataItem<T> WithDivId<T>(this DataItem<T> dataItem, string divId)
		{
			dataItem.HasDivId = true;
			dataItem.DivId = divId;
			return dataItem;
		}

		public static DataItem<T> WithInputCssClass<T>(this DataItem<T> dataItem, string inputCssClass)
		{
			dataItem.InputCssClass = inputCssClass;
			return dataItem;
		}

		public static DataItem<T> WithLabel<T>(this DataItem<T> dataItem, string label)
		{
			dataItem.LabelText = label;
			return dataItem;
		}

		public static DataItem<T> WithName<T>(this DataItem<T> dataColumn, string name)
		{
			dataColumn.InputTextName = name;
			return dataColumn;
		}

		public static DataItem<T> WrapWithSpan<T>(this DataItem<T> dataItem)
		{
			dataItem.WrapWithSpan = true;
			return dataItem;
		}
	}
}