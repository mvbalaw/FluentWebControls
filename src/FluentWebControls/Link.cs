using System;
using System.Linq;
using System.Linq.Expressions;

using MvbaCore;

namespace FluentWebControls
{
	public static class Link
	{
		public static LinkData To<TControllerType>(Expression<Func<TControllerType, object>> targetControllerAction) where TControllerType : class
		{
			var linkData = To(Reflection.GetControllerName<TControllerType>(), Reflection.GetMethodName(targetControllerAction));
			linkData.AddUrlParameters(Reflection.GetMethodCallData(targetControllerAction).ParameterValues.Values.ToList());
			return linkData;
		}

		public static LinkData To(string controllerName, string controllerExtension, string actionName)
		{
			var pathUtility = Configuration.PathUtility;
			string virtualDirectory = String.Format("{0}{1}/{2}", controllerName, controllerExtension, actionName);
			string url = pathUtility != null ? pathUtility.GetUrl(virtualDirectory) : virtualDirectory;
			return new LinkData
				{
					Url = url
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