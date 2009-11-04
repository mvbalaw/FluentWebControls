namespace FluentWebControls.Extensions
{
	public static class CheckBoxDataExtensions
	{
		public static CheckBoxData IsChecked(this CheckBoxData checkBoxData, bool @checked)
		{
			checkBoxData.Checked = @checked;
			return checkBoxData;
		}

		public static CheckBoxData WithLabel(this CheckBoxData checkBoxData, LabelData label)
		{
			checkBoxData.Label = label;
			return checkBoxData;
		}

		public static CheckBoxData WithLabelAlignedLeft(this CheckBoxData checkBoxData, LabelData label)
		{
			checkBoxData.Label = label;
			checkBoxData.LabelAlignAttribute = AlignAttribute.Left;
			return checkBoxData;
		}
	}
}