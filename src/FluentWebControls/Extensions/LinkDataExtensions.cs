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
	public static class LinkDataExtensions
	{
		public static LinkData DisabledIf(this LinkData linkData, bool disabled)
		{
			linkData.Disabled = disabled;
			return linkData;
		}

		public static LinkData VisibleIf(this LinkData buttonData, bool visible)
		{
			buttonData.Visible = visible;
			return buttonData;
		}

		public static LinkData WithControllerExtension(this LinkData linkData, string controllerExtension)
		{
			linkData.ControllerExtension = controllerExtension;
			return linkData;
		}

		public static LinkData WithCssClass(this LinkData linkData, string cssClass)
		{
			linkData.CssClass = cssClass;
			return linkData;
		}

		public static LinkData WithId(this LinkData linkData, string id)
		{
			linkData.Id = id;
			return linkData;
		}

		public static LinkData WithLinkImageUrl(this LinkData linkData, string imageUrl)
		{
			return linkData.WithLinkImageUrl(imageUrl, "");
		}

		public static LinkData WithLinkImageUrl(this LinkData linkData, string imageUrl, string alt)
		{
			linkData.ImageUrl = imageUrl;
			linkData.Alt = alt;
			return linkData;
		}

		public static LinkData WithInnerHtml(this LinkData linkData, string innerHtml)
		{
			linkData.InnerHtml = innerHtml;
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

		public static LinkData WithQueryStringData(this LinkData linkData, Expression<Func<string>> fieldNameAndValue)
		{
			linkData.AddQueryStringData(Reflection.GetPropertyName(fieldNameAndValue), fieldNameAndValue.Compile()());
			return linkData;
		}

		public static LinkData WithQueryStringData(this LinkData linkData, Expression<Func<string>> fieldName, string value)
		{
			linkData.AddQueryStringData(Reflection.GetPropertyName(fieldName), value);
			return linkData;
		}

		public static LinkData WithQueryStringData(this LinkData linkData, string fieldName, string value)
		{
			linkData.AddQueryStringData(fieldName, value);
			return linkData;
		}

		public static LinkData WithQueryStringData(this LinkData linkData, IEnumerable<KeyValuePair<string, string>> items)
		{
			foreach (var item in items)
			{
				linkData.AddQueryStringData(item.Key, item.Value);
			}
			return linkData;
		}

		public static LinkData WithQueryStringData<T>(this LinkData linkData, Expression<Func<T>> fieldNameAndValue) where T : struct
		{
			linkData.AddQueryStringData(Reflection.GetPropertyName(fieldNameAndValue), fieldNameAndValue.Compile()().ToString());
			return linkData;
		}

		public static LinkData WithQueryStringData<T>(this LinkData linkData, Expression<Func<T>> fieldName, T value) where T : struct
		{
			linkData.AddQueryStringData(Reflection.GetPropertyName(fieldName), value.ToString());
			return linkData;
		}

		public static LinkData WithQueryStringData<T>(this LinkData linkData, Expression<Func<T?>> fieldNameAndValue) where T : struct
		{
			var value = fieldNameAndValue.Compile()();
			linkData.AddQueryStringData(Reflection.GetPropertyName(fieldNameAndValue), value == null ? "" : value.ToString());
			return linkData;
		}

		public static LinkData WithQueryStringData<T>(this LinkData linkData, Expression<Func<T?>> fieldName, T? value) where T : struct
		{
			linkData.AddQueryStringData(Reflection.GetPropertyName(fieldName), value == null ? "" : value.ToString());
			return linkData;
		}

		public static LinkData WithQueryStringData<T, K>(this LinkData linkData, T source, Expression<Func<T, K>> fieldName, Func<T, string> fieldValue)
		{
			linkData.AddQueryStringData(Reflection.GetPropertyName(fieldName), fieldValue(source));
			return linkData;
		}

		public static LinkData WithRel(this LinkData linkData, string rel)
		{
			linkData.Rel = rel;
			return linkData;
		}

		public static LinkData WithTarget(this LinkData linkData, string target)
		{
			linkData.Target = target;
			return linkData;
		}

		public static LinkData WithUrl(this LinkData linkData, string url)
		{
			linkData.Url = url;
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
	}
}