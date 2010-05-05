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
			string controllerName = Reflection.GetControllerName<TControllerType>();
			string actionName = Reflection.GetMethodName(targetControllerAction);
			var linkData = To(controllerName, actionName);
			var actionParameters = Reflection.GetMethodCallData(targetControllerAction).ParameterValues.Values.ToList();
			linkData.AddUrlParameters(actionParameters);
			linkData.Id = controllerName + (!actionName.IsNullOrEmpty() ? "_" : "") + actionName + (actionParameters.Any() ? "_" : "") + actionParameters.Join("_");
			return linkData;
		}

		public static LinkData To(string controllerName, string controllerExtension, string actionName)
		{
			var pathUtility = Configuration.PathUtility;
			string virtualDirectory = String.Format("{0}{1}/{2}", controllerName, controllerExtension, actionName);
			string url = pathUtility != null ? pathUtility.GetUrl(virtualDirectory) : virtualDirectory;
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