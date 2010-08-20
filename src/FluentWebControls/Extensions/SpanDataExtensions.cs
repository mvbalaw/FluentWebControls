namespace FluentWebControls.Extensions
{
	public static class SpanDataExtensions
	{
		public static SpanData WithCssClass(this SpanData spanData, string cssClass)
		{
			spanData.CssClass = cssClass;
			return spanData;
		}

		public static SpanData WithLabel(this SpanData spanData, string labelText)
		{
			return spanData.WithLabel(Label.ForIt().WithText(labelText));
		}

		public static SpanData WithLabel(this SpanData spanData, LabelData label)
		{
			spanData.Label = label;
			return spanData;
		}
	}
}