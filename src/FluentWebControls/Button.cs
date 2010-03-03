using System;
using System.Linq;
using System.Linq.Expressions;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;
using FluentWebControls.Tools;

using MvbaCore;

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
			var buttonData = new ButtonData(buttonType, IoCUtility.GetInstance<IPathUtility>(), Reflection.GetControllerName<TController>())
				.WithAction(Reflection.GetMethodName(controllerAndActionName));
			if (buttonType == ButtonData.ButtonType.Link)
			{
				buttonData.AddUrlParameters(Reflection.GetMethodCallData(controllerAndActionName).ParameterValues.Values.ToList());
			}
			return buttonData;
		}
	}
}