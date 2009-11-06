using System;

using FluentWebControls.Interfaces;
using FluentWebControls.Tools;

namespace FluentWebControls
{
	public static class Link
	{
		public static LinkData To(string controllerName, string actionName)
		{
			return new LinkData
				{
					Href = IoCUtility.GetInstance<IPathUtility>().GetUrl(String.Format("{0}.mvc/{1}", controllerName, actionName))
				};
		}

		public static LinkData To(string controller)
		{
			return new LinkData
				{
					Href = IoCUtility.GetInstance<IPathUtility>().GetUrl(String.Format("{0}.mvc", controller))
				};
		}

		public static LinkData To()
		{
			return new LinkData();
		}
	}
}