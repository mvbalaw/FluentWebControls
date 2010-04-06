using FluentWebControls.Mapping;

namespace FluentWebControls.Extensions
{
	public static class IBooleanMapExtensions
	{
		public static CheckBoxData AsCheckBox(this IBooleanMap input)
		{
			return new CheckBoxData(input.IsChecked)
				.WithId(input.Id);
		}
	}
}