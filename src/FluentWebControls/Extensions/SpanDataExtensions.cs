namespace FluentWebControls.Extensions
{
	public static class SpanDataExtensions
	{
		public static SpanData WithCssClass(this SpanData spanData, string cssClass)
		{
			spanData.CssClass = cssClass;
			return spanData;
		}
	}
}