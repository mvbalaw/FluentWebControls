namespace FluentWebControls.Extensions
{
	public static class RadioButtonDataExtensions
	{
		public static RadioButtonData IsChecked(this RadioButtonData checkBoxData, bool @checked)
		{
			checkBoxData.Checked = @checked;
			return checkBoxData;
		}

		public static RadioButtonData WithLabel(this RadioButtonData checkBoxData, string labelText)
		{
			var label = Label.ForIt().WithText(labelText);
			return checkBoxData.WithLabel(label);
		}

		public static RadioButtonData WithLabel(this RadioButtonData checkBoxData, LabelData label)
		{
			checkBoxData.Label = label;
			return checkBoxData;
		}

		public static RadioButtonData WithLabelAlignedLeft(this RadioButtonData checkBoxData, LabelData label)
		{
			checkBoxData.LabelAlignAttribute = AlignAttribute.Left;
			return checkBoxData.WithLabel(label);
		}

		public static RadioButtonData WithLabelAlignedLeft(this RadioButtonData checkBoxData, string labelText)
		{
			checkBoxData.LabelAlignAttribute = AlignAttribute.Left;
			return checkBoxData.WithLabel(labelText);
		}

		public static RadioButtonData WithValue(this RadioButtonData checkBoxData, string value)
		{
			checkBoxData.Value = value;
			return checkBoxData;
		}

		public static RadioButtonData WithTabIndex(this RadioButtonData checkBoxData, string tabIndex)
		{
			checkBoxData.TabIndex = tabIndex;
			return checkBoxData;
		}
	}
}