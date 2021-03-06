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

using MvbaCore;

namespace FluentWebControls.Extensions
{
	public static class DropDownListDataExtensions
	{
		public static DropDownListData AsReadOnly(this DropDownListData dropDownListData)
		{
			dropDownListData.ReadOnly = true;
			return dropDownListData;
		}

		public static DropDownListData CssClass(this DropDownListData dropDownListData, string cssClass)
		{
			dropDownListData.CssClass = cssClass;
			return dropDownListData;
		}

		public static DropDownListData Exclude(this DropDownListData dropDownListData, Expression<Func<string>> getValue)
		{
			dropDownListData.Remove(getValue.Compile()());
			return dropDownListData;
		}

		public static DropDownListData Exclude<T>(this DropDownListData dropDownListData, Func<T> nullableParent, Func<T, string> getValue) where T : class
		{
			var parentValue = nullableParent();
			string value = null;
			if (parentValue != null)
			{
				value = getValue(parentValue);
			}
			dropDownListData.Remove(value);
			return dropDownListData;
		}

		public static DropDownListData SubmitOnChange(this DropDownListData dropDownListData)
		{
			dropDownListData.SubmitOnChange = true;
			return dropDownListData;
		}

		public static DropDownListData SubmitOnChange<T>(this DropDownListData dropDownListData, Expression<Func<T>> formFieldToSetBeforeSubmitting, T value)
		{
			dropDownListData.SubmitOnChange = true;
			dropDownListData.FormFieldToSetBeforeSubmitOnChange = new KeyValuePair<string, string>(Reflection.GetPropertyName(formFieldToSetBeforeSubmitting).ToCamelCase(), value.ToString());
			return dropDownListData;
		}

		public static DropDownListData WithAttribute(this DropDownListData dropDownListData, string attributeName, string attributeValue)
		{
			dropDownListData.Attributes.Add(new KeyValuePair<string, string>(attributeName, attributeValue));
			return dropDownListData;
		}

		public static DropDownListData WithDefault(this DropDownListData dropDownListData, string text, string value)
		{
			dropDownListData.Default = new KeyValuePair<string, string>(text, value);
			return dropDownListData;
		}

		public static DropDownListData WithLabel(this DropDownListData dropDownListData, string labelText)
		{
			return dropDownListData.WithLabel(Label.ForIt().WithText(labelText));
		}

		public static DropDownListData WithLabel(this DropDownListData dropDownListData, LabelData label)
		{
			dropDownListData.Label = label;
			return dropDownListData;
		}

		public static DropDownListData WithSelectedValue<T>(this DropDownListData dropDownListData, Func<T> nullableParent, Func<T, string> getValue) where T : class
		{
			var parentValue = nullableParent();
			string value = null;
			if (parentValue != null)
			{
				value = getValue(parentValue);
			}
			return dropDownListData.WithSelectedValue(value);
		}

		public static DropDownListData WithSelectedValue<T>(this DropDownListData dropDownListData, Func<T> nullableParent, Func<T, int> getValue) where T : class
		{
			var parentValue = nullableParent();
			string value = null;
			if (parentValue != null)
			{
				value = getValue(parentValue).ToString();
			}
			return dropDownListData.WithSelectedValue(value);
		}

		public static DropDownListData WithSelectedValue(this DropDownListData dropDownListData, Expression<Func<string>> getValue)
		{
			dropDownListData.SelectedValue = getValue.Compile()();
			return dropDownListData;
		}

		public static DropDownListData WithSelectedValue(this DropDownListData dropDownListData, string value)
		{
			dropDownListData.SelectedValue = value;
			return dropDownListData;
		}

		public static DropDownListData WithSelectedValue(this DropDownListData dropDownListData, int? value)
		{
			dropDownListData.SelectedValue = value == null ? "" : value.ToString();
			return dropDownListData;
		}

		public static DropDownListData WithSlaveDdl<TFuncInput, TFuncResult, TControllerType>(this DropDownListData dropDownListData, Expression<Func<TFuncInput, TFuncResult>> forSlaveId, Expression<Func<TControllerType, object>> targetControllerJsonActionToGetSlaveData) where TControllerType : class
		{
			dropDownListData.SlaveId = Reflection.GetPropertyName(forSlaveId);
			dropDownListData.SlaveDataSource = Reflection.GetMethodCallData(targetControllerJsonActionToGetSlaveData);
			return dropDownListData;
		}

		public static DropDownListData WithTabIndex(this DropDownListData dropDownListData, string tabIndex)
		{
			dropDownListData.TabIndex = tabIndex;
			return dropDownListData;
		}
	}
}