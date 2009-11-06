using System;

using FluentWebControls.Interfaces;
using FluentWebControls.Tools;

namespace FluentWebControls
{
	public static class Link
	{
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