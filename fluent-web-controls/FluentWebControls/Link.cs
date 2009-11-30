using System;
using System.Linq.Expressions;

using FluentWebControls.Interfaces;
using FluentWebControls.Tools;

namespace FluentWebControls
{
	public static class Link
	{
		public static LinkData To<TControllerType>(Expression<Func<TControllerType, object>> targetControllerAction)
		{
			var linkData = To(NameUtility.GetControllerName<TControllerType>(), NameUtility.GetMethodName(targetControllerAction));
			linkData.AddUrlParameters(NameUtility.GetArguments(targetControllerAction));
			return linkData;
		}

		public static LinkData To(string controllerName, string controllerExtension, string actionName)
		{
			return new LinkData
				{
					Href = IoCUtility.GetInstance<IPathUtility>().GetUrl(String.Format("{0}{1}/{2}", controllerName, controllerExtension, actionName))
				};
		}

		public static LinkData To(string controllerName, string actionName)
		{
			return To(controllerName, "", actionName);
		}

		[Obsolete("Use .To(controllerName, \".mvc\", \"\")")]
		public static LinkData To(string controllerName)
		{
			return To(controllerName, ".mvc", "");
		}

		public static LinkData To()
		{
			return new LinkData();
		}
	}
}