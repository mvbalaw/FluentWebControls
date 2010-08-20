using System;
using System.Linq;
using System.Linq.Expressions;

using FluentWebControls.Extensions;

using MvbaCore;

namespace FluentWebControls
{
	public static class Button
	{
		public static ButtonData For(IButtonType buttonType, string controllerName)
		{
			return new ButtonData(buttonType, Configuration.PathUtility, controllerName);
		}
		
		public static ButtonData For(IButtonType buttonType)
		{
			return new ButtonData(buttonType);
		}

		public static ButtonData For<TController>(IButtonType buttonType, Expression<Func<TController, object>> controllerAndActionName) where TController : class
		{
			var buttonData = new ButtonData(buttonType, Configuration.PathUtility, Reflection.GetControllerName<TController>())
				.WithAction(Reflection.GetMethodName(controllerAndActionName));
			if (buttonType == ButtonData.ButtonType.Link)
			{
				buttonData.AddUrlParameters(Reflection.GetMethodCallData(controllerAndActionName).ParameterValues.Values.ToList());
			}
			return buttonData;
		}
	}
}