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
	}
}