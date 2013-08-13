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

using MvbaCore;

namespace FluentWebControls
{
	public static class Link
	{
		public static LinkData To<TControllerType>(Expression<Func<TControllerType, object>> targetControllerAction) where TControllerType : class
		{
			var controllerName = Reflection.GetControllerName<TControllerType>();
			var actionName = Reflection.GetMethodName(targetControllerAction);
			var linkData = To(controllerName, actionName);
			var actionParameters = Reflection.GetMethodCallData(targetControllerAction).ParameterValues.Values.ToList();
			linkData.AddUrlParameters(actionParameters);
			linkData.Id = controllerName + (!actionName.IsNullOrEmpty() ? "_" : "") + actionName + (actionParameters.Any() ? "_" : "") + actionParameters.Join("_");
			return linkData;
		}

		public static LinkData To(string controllerName, string controllerExtension, string actionName)
		{
			var pathUtility = Configuration.PathUtility;
			var virtualDirectory = String.Format("{0}{1}/{2}", controllerName, controllerExtension, actionName);
			var url = pathUtility != null ? pathUtility.GetUrl(virtualDirectory) : virtualDirectory;
			return new LinkData
			       {
				       Url = url,
				       Id = controllerName + (!actionName.IsNullOrEmpty() ? "_" : "") + actionName
			       };
		}

		public static LinkData To(string controllerName, string actionName)
		{
			return To(controllerName, "", actionName);
		}

		public static LinkData To()
		{
			return new LinkData();
		}
	}
}