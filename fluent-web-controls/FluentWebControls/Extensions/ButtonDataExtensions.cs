namespace FluentWebControls.Extensions
{
	public static class ButtonDataExtensions
	{
		public static ButtonData Confirm(this ButtonData buttonData, string message)
		{
			buttonData.ConfirmMessage = message;
			return buttonData;
		}

		public static ButtonData VisibleIf(this ButtonData buttonData, bool visible)
		{
			buttonData.Visible = visible;
			return buttonData;
		}

		public static ButtonData Width(this ButtonData buttonData, string width)
		{
			buttonData.Width = width;
			return buttonData;
		}

		public static ButtonData WithAction(this ButtonData buttonData, string actionName)
		{
			buttonData.ActionName = actionName;
			return buttonData;
		}

		public static ButtonData WithOnClick(this ButtonData buttonData, string onClickMethod)
		{
			buttonData.OnClickMethod = onClickMethod;
			return buttonData;
		}

		public static ButtonData WithQueryParameter(this ButtonData buttonData, string fileName)
		{
			buttonData.QueryParameter = fileName;
			return buttonData;
		}

		public static ButtonData WithText(this ButtonData buttonData, string text)
		{
			buttonData.Text = text;
			return buttonData;
		}
	}
}