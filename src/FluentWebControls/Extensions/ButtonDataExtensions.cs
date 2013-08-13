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
using System.Web;

namespace FluentWebControls.Extensions
{
	public static class ButtonDataExtensions
	{
		public static ButtonData AsDefault(this ButtonData buttonData)
		{
			buttonData.Default = true;
			return buttonData;
		}

		public static ButtonData Confirm(this ButtonData buttonData, string message)
		{
			buttonData.ConfirmMessage = message;
			return buttonData;
		}

		public static ButtonData VisibleIf(this ButtonData buttonData, bool visible)
		{
			buttonData.Visible = visible;
			return buttonData;
		}

		public static ButtonData Width(this ButtonData buttonData, string width)
		{
			buttonData.Width = width;
			return buttonData;
		}

		public static ButtonData WithAction(this ButtonData buttonData, string actionName)
		{
			buttonData.ActionName = actionName;
			return buttonData;
		}

		public static ButtonData WithControllerExtension(this ButtonData buttonData, string controllerExtension)
		{
			buttonData.ControllerExtension = controllerExtension;
			return buttonData;
		}

		public static ButtonData WithCssClass(this ButtonData buttonData, string cssClass)
		{
			buttonData.CssClass = cssClass;
			return buttonData;
		}

		public static ButtonData WithId(this ButtonData buttonData, string id)
		{
			buttonData.Id = id;
			return buttonData;
		}

		public static ButtonData WithOnClick(this ButtonData buttonData, string onClickMethod)
		{
			buttonData.OnClickMethod = onClickMethod;
			return buttonData;
		}

		public static ButtonData WithQueryParameter(this ButtonData buttonData, string parameterName, string parameterValue)
		{
			if (!String.IsNullOrEmpty(buttonData.QueryParameter))
			{
				buttonData.QueryParameter += "&";
			}
			buttonData.QueryParameter = parameterName + "=" + HttpUtility.UrlEncode(parameterValue);
			return buttonData;
		}

		public static ButtonData WithText(this ButtonData buttonData, string text)
		{
			buttonData.Text = text;
			return buttonData;
		}
	}
}