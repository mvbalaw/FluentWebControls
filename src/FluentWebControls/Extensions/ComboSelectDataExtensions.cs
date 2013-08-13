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
using System.Linq.Expressions;

namespace FluentWebControls.Extensions
{
	public static class ComboSelectDataExtensions
	{
		public static ComboSelectData CssClass(this ComboSelectData comboSelectData, string cssClass)
		{
			comboSelectData.CssClass.Add(cssClass);
			return comboSelectData;
		}

		public static ComboSelectData WithLabel(this ComboSelectData comboSelectData, string labelText)
		{
			var label = Label.ForIt().WithText(labelText);
			return comboSelectData.WithLabel(label);
		}

		public static ComboSelectData WithLabel(this ComboSelectData comboSelectData, LabelData label)
		{
			comboSelectData.Label = label;
			return comboSelectData;
		}

		public static ComboSelectData WithSelectedValues<T>(this ComboSelectData comboSelectData, IList<T> items, Expression<Func<T, int>> getIdValue) where T : class
		{
			if (items != null)
			{
				foreach (var item in items)
				{
					comboSelectData.SelectedValues.Add(getIdValue.Compile()(item).ToString());
				}
			}
			return comboSelectData;
		}

		public static ComboSelectData WithSelectedValues<T>(this ComboSelectData comboSelectData, IEnumerable<T> items, Expression<Func<T, int>> getIdValue) where T : class
		{
			if (items != null)
			{
				foreach (var item in items)
				{
					comboSelectData.SelectedValues.Add(getIdValue.Compile()(item).ToString());
				}
			}
			return comboSelectData;
		}

		public static ComboSelectData WithSelectedValues(this ComboSelectData comboSelectData, IEnumerable<int> items)
		{
			if (items != null)
			{
				foreach (var item in items)
				{
					comboSelectData.SelectedValues.Add(item.ToString());
				}
			}
			return comboSelectData;
		}

		public static ComboSelectData WithSize(this ComboSelectData comboSelectData, int size)
		{
			comboSelectData.Size = size;
			return comboSelectData;
		}

		public static ComboSelectData WithTabIndex(this ComboSelectData comboSelectData, string tabIndex)
		{
			comboSelectData.TabIndex = tabIndex;
			return comboSelectData;
		}
	}
}