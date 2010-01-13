using System;
using System.Linq;
using System.Linq.Expressions;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;
using FluentWebControls.Tools;

namespace FluentWebControls
{
	public static class Button
	{
		public static ButtonData For(IButtonType buttonType, string controllerName)
		{
			return new ButtonData(buttonType, IoCUtility.GetInstance<IPathUtility>(), controllerName);
		}

		public static ButtonData For<TController>(IButtonType buttonType, Expression<Func<TController, object>> controllerAndActionName) where TController : class
		{
			var buttonData = new ButtonData(buttonType, IoCUtility.GetInstance<IPathUtility>(), NameUtility.GetControllerName<TController>())
				.WithAction(NameUtility.GetMethodName(controllerAndActionName));
			if (buttonType == ButtonData.ButtonType.Link)
			{
				buttonData.AddUrlParameters(ReflectionUtility.GetMethodCallData(controllerAndActionName).ParameterValues.Values.ToList());
			}
			return buttonData;
		}
	}
}