using FluentWebControls.Mapping;

namespace FluentWebControls.Extensions
{
	public static class IFreeTextMapExtensions
	{
		public static HiddenData AsHidden(this IFreeTextMap input)
		{
			return new HiddenData()
				.WithValue(input.Value)
				.WithId(input.Id);
		}

		public static TextAreaData AsTextArea(this IFreeTextMap input)
		{
			return new TextAreaData(input.Value)
				.WithId(input.Id);
		}

		public static TextBoxData AsTextBox(this IFreeTextMap input)
		{
			return new TextBoxData(input.Value)
				.WithId(input.Id);
		}
	}
}