namespace FluentWebControls.Extensions
{
	public static class TextAreaDataExtensions
	{
		public static TextAreaData Cols(this TextAreaData textAreaData, int cols)
		{
			textAreaData.Cols = cols;
			return textAreaData;
		}

		public static TextAreaData CssClass(this TextAreaData textAreaData, string cssClass)
		{
			textAreaData.CssClass = cssClass;
			return textAreaData;
		}

		public static TextAreaData Rows(this TextAreaData textAreaData, int rows)
		{
			textAreaData.Rows = rows;
			return textAreaData;
		}

		public static TextAreaData Width(this TextAreaData textAreaData, string width)
		{
			textAreaData.Width = width;
			return textAreaData;
		}

		public static TextAreaData WithLabel(this TextAreaData textAreaData, string labelText)
		{
			return textAreaData.WithLabel(Label.ForIt().WithText(labelText));
		}

		public static TextAreaData WithLabel(this TextAreaData textAreaData, LabelData label)
		{
			textAreaData.Label = label;
			return textAreaData;
		}
	}
}