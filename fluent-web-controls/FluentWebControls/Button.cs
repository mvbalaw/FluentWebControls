using FluentWebControls.Interfaces;
using FluentWebControls.Tools;

namespace FluentWebControls
{
	public static class Button
	{
		public static ButtonData For(ButtonData.ButtonType buttonType, string controllerName)
		{
			return new ButtonData(buttonType, IoCUtility.GetInstance<IPathUtility>(), controllerName);
		}
	}
}