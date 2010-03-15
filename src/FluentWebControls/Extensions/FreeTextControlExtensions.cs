namespace FluentWebControls.Extensions
{
	public static class FreeTextControlExtensions
	{
		public static FreeTextControl WithValue(this FreeTextControl input, string value)
		{
			input.Value = value;
			return input;
		}

		public static HiddenData AsHidden(this FreeTextControl input)
		{
			IFreeTextControl data = input;
			return new HiddenData()
				.WithValue(data.Value)
				.WithId(data.Id)
				.WithIdPrefix(data.IdPrefix);
		}

		public static TextAreaData AsTextArea(this FreeTextControl input)
		{
			IFreeTextControl data = input;
			return new TextAreaData(data.Value)
				.WithId(data.Id)
				.WithIdPrefix(data.IdPrefix);
		}

		public static TextBoxData AsTextBox(this FreeTextControl input)
		{
			IFreeTextControl data = input;
			return new TextBoxData(data.Value)
				.WithId(data.Id)
				.WithIdPrefix(data.IdPrefix);
		}
	}
}