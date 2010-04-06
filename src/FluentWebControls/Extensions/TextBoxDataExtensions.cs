namespace FluentWebControls.Extensions
{
	public static class TextBoxDataExtensions
	{
		public static TextBoxData AsReadOnly(this TextBoxData textBoxData)
		{
			textBoxData.ReadOnly = true;
			return textBoxData;
		}

		public static TextBoxData CssClass(this TextBoxData textBoxData, string cssClass)
		{
			textBoxData.CssClass = cssClass;
			return textBoxData;
		}

		public static TextBoxData MaxValue(this TextBoxData textBoxData, int maxValue)
		{
			textBoxData.MaxValue = maxValue;
			return textBoxData;
		}

		public static TextBoxData MinValue(this TextBoxData textBoxData, int minValue)
		{
			textBoxData.MinValue = minValue;
			return textBoxData;
		}

		public static TextBoxData Width(this TextBoxData textBoxData, string width)
		{
			textBoxData.Width = width;
			return textBoxData;
		}

		public static TextBoxData WithLabel(this TextBoxData textBoxData, string labelText)
		{
			var label = Label.ForIt().WithText(labelText);
			return textBoxData.WithLabel(label);
		}

		public static TextBoxData WithLabel(this TextBoxData textBoxData, LabelData label)
		{
			textBoxData.Label = label;
			return textBoxData;
		}
	}
}