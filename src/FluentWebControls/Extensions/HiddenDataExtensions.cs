namespace FluentWebControls.Extensions
{
	public static class HiddenDataExtensions
	{
		public static HiddenData Text(this HiddenData hiddenData, string text)
		{
			hiddenData.Text = text;
			return hiddenData;
		}
	}
}