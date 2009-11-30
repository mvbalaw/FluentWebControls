using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using FluentWebControls.Tools;

namespace FluentWebControls.Extensions
{
	public static class LinkDataExtensions
	{
		public static LinkData WithCssClass(this LinkData linkData, string cssClass)
		{
			linkData.CssClass = cssClass;
			return linkData;
		}

		public static LinkData DisabledIf(this LinkData linkData, bool disabled)
		{
			linkData.Disabled = disabled;
			return linkData;
		}

		public static LinkData Id(this LinkData linkData, string id)
		{
			linkData.Id = id;
			return linkData;
		}

		public static LinkData WithData(this LinkData linkData, Expression<Func<string>> fieldNameAndValue)
		{
			linkData.AddQueryStringData(NameUtility.GetPropertyName(fieldNameAndValue), fieldNameAndValue.Compile()());
			return linkData;
		}

		public static LinkData WithData(this LinkData linkData, Expression<Func<string>> fieldName, string value)
		{
			linkData.AddQueryStringData(NameUtility.GetPropertyName(fieldName), value);
			return linkData;
		}

		public static LinkData WithData(this LinkData linkData, string fieldName, string value)
		{
			linkData.AddQueryStringData(fieldName, value);
			return linkData;
		}

		public static LinkData WithUrlParameters(this LinkData linkData, string urlParameter)
		{
			linkData.AddUrlParameters(urlParameter);
			return linkData;
		}

		public static LinkData WithUrlParameters(this LinkData linkData, List<string> urlParameters)
		{
			linkData.AddUrlParameters(urlParameters);
			return linkData;
		}

		public static LinkData WithData(this LinkData linkData, IEnumerable<KeyValuePair<string, string>> items)
		{
			foreach (var item in items)
			{
				linkData.AddQueryStringData(item.Key, item.Value);
			}
			return linkData;
		}

		public static LinkData WithData<T>(this LinkData linkData, Expression<Func<T>> fieldNameAndValue) where T : struct
		{
			linkData.AddQueryStringData(NameUtility.GetPropertyName(fieldNameAndValue), fieldNameAndValue.Compile()().ToString());
			return linkData;
		}

		public static LinkData WithData<T>(this LinkData linkData, Expression<Func<T>> fieldName, T value) where T : struct
		{
			linkData.AddQueryStringData(NameUtility.GetPropertyName(fieldName), value.ToString());
			return linkData;
		}

		public static LinkData WithData<T>(this LinkData linkData, Expression<Func<T?>> fieldNameAndValue) where T : struct
		{
			var value = fieldNameAndValue.Compile()();
			linkData.AddQueryStringData(NameUtility.GetPropertyName(fieldNameAndValue), value == null ? "" : value.ToString());
			return linkData;
		}

		public static LinkData WithData<T>(this LinkData linkData, Expression<Func<T?>> fieldName, T? value) where T : struct
		{
			linkData.AddQueryStringData(NameUtility.GetPropertyName(fieldName), value == null ? "" : value.ToString());
			return linkData;
		}

		public static LinkData WithData<T, K>(this LinkData linkData, T source, Expression<Func<T, K>> fieldName, Func<T, string> fieldValue)
		{
			linkData.AddQueryStringData(NameUtility.GetPropertyName(fieldName), fieldValue(source));
			return linkData;
		}

		public static LinkData WithHref(this LinkData linkData, string href)
		{
			linkData.Href = href;
			return linkData;
		}

		public static LinkData WithLinkText(this LinkData linkData, string linkText)
		{
			linkData.LinkText = linkText;
			return linkData;
		}

		public static LinkData WithMouseOverText(this LinkData linkData, string mouseOverText)
		{
			linkData.MouseOverText = mouseOverText;
			return linkData;
		}

		public static LinkData WithRel(this LinkData linkData, string rel)
		{
			linkData.Rel = rel;
			return linkData;
		}
	}
}