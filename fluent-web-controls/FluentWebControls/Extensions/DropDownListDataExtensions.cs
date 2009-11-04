using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentWebControls.Tools;

namespace FluentWebControls.Extensions
{
	public static class DropDownListDataExtensions
	{
		public static DropDownListData CssClass(this DropDownListData dropDownListData, string cssClass)
		{
			dropDownListData.CssClass = cssClass;
			return dropDownListData;
		}

		public static DropDownListData Id(this DropDownListData dropDownListData, string id)
		{
			dropDownListData.Id = id.ToCamelCase();
			return dropDownListData;
		}

		public static DropDownListData WithIdPrefix(this DropDownListData dropDownListData, string idPrefix)
		{
			dropDownListData.IdPrefix = idPrefix.ToCamelCase();
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
			dropDownListData.FormFieldToSetBeforeSubmitOnChange = new KeyValuePair<string, string>(NameUtility.GetPropertyName(formFieldToSetBeforeSubmitting).ToCamelCase(), value.ToString());
			return dropDownListData;
		}

		public static DropDownListData WithDefault(this DropDownListData dropDownListData, string text, string value)
		{
			dropDownListData.Default = new KeyValuePair<string, string>(text, value);
			return dropDownListData;
		}

		public static DropDownListData WithLabel(this DropDownListData dropDownListData, LabelData label)
		{
			dropDownListData.Label = label;
			return dropDownListData;
		}

		public static DropDownListData WithSelectedValue<T>(this DropDownListData dropDownListData, Func<T> nullableParent, Func<T, string> getValue) where T : class
		{
			T parentValue = nullableParent();
			string value = null;
			if (parentValue != null)
			{
				value = getValue(parentValue);
			}
			return dropDownListData.WithSelectedValue(value);
		}

		public static DropDownListData WithSelectedValue<T>(this DropDownListData dropDownListData, Func<T> nullableParent, Func<T, int> getValue) where T : class
		{
			T parentValue = nullableParent();
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
	}
}