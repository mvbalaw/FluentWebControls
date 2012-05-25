namespace FluentWebControls.Extensions
{
	public static class RadioButtonDataExtensions
	{
		public static RadioButtonData IsChecked(this RadioButtonData radioButtonData, bool @checked)
		{
			radioButtonData.Checked = @checked;
			return radioButtonData;
		}

		public static RadioButtonData WithLabel(this RadioButtonData radioButtonData, string labelText)
		{
			var label = Label.ForIt().WithText(labelText);
			return radioButtonData.WithLabel(label);
		}
		
		public static RadioButtonData WithCssClass(this RadioButtonData radioButtonData, string cssClass)
		{
			radioButtonData.CssClass = cssClass;
			return radioButtonData;
		}

		public static RadioButtonData WithLabel(this RadioButtonData radioButtonData, LabelData label)
		{
			radioButtonData.Label = label;
			return radioButtonData;
		}

		public static RadioButtonData WithLabelAlignedLeft(this RadioButtonData radioButtonData, LabelData label)
		{
			radioButtonData.LabelAlignAttribute = AlignAttribute.Left;
			return radioButtonData.WithLabel(label);
		}

		public static RadioButtonData WithLabelAlignedLeft(this RadioButtonData radioButtonData, string labelText)
		{
			radioButtonData.LabelAlignAttribute = AlignAttribute.Left;
			return radioButtonData.WithLabel(labelText);
		}

		public static RadioButtonData WithValue(this RadioButtonData radioButtonData, string value)
		{
			radioButtonData.Value = value;
			return radioButtonData;
		}

		public static RadioButtonData WithTabIndex(this RadioButtonData checkBoxData, string tabIndex)
		{
			checkBoxData.TabIndex = tabIndex;
			return checkBoxData;
		}
	}
}