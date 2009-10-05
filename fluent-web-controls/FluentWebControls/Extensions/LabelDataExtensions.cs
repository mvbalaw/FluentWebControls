namespace FluentWebControls.Extensions
{
	public static class LabelDataExtensions
	{
		public static LabelData Style(this LabelData labelData, string style)
		{
			labelData.Style = style;
			return labelData;
		}

		public static LabelData Width(this LabelData labelData, string width)
		{
			labelData.Width = width;
			return labelData;
		}

		public static LabelData WithText(this LabelData labelData, string text)
		{
			labelData.Text = text;
			return labelData;
		}
	}
}