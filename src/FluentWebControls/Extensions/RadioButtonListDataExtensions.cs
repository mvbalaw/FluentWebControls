namespace FluentWebControls.Extensions
{
	public static class RadioButtonListDataExtensions
	{
		public static RadioButtonListData WithLabel(this RadioButtonListData radioButtonListData, string labelText)
		{
			var label = Label.ForIt().WithText(labelText);
			return radioButtonListData.WithLabel(label);
		}

		public static RadioButtonListData WithLabel(this RadioButtonListData radioButtonData, LabelData label)
		{
			radioButtonData.Label = label;
			return radioButtonData;
		}

	}
}