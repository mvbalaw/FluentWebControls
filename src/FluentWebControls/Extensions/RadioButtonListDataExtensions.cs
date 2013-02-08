namespace FluentWebControls.Extensions
{
	public static class RadioButtonListDataExtensions
	{
		public static RadioButtonListData WithCssClass(this RadioButtonListData radioButtonListData, string cssClass)
		{
			radioButtonListData.CssClass = cssClass;
			return radioButtonListData;
		}

		public static RadioButtonListData WithLabel(this RadioButtonListData radioButtonListData, string labelText)
		{
			var label = Label.ForIt().WithText(labelText);
			return radioButtonListData.WithLabel(label);
		}

		public static RadioButtonListData WithLabel(this RadioButtonListData radioButtonListData, LabelData label)
		{
			radioButtonListData.Label = label;
			return radioButtonListData;
		}

		public static RadioButtonListData WithoutItemSeparator(this RadioButtonListData radioButtonListData)
		{
			radioButtonListData.UseItemSeparator = false;
			return radioButtonListData;
		}
	}
}