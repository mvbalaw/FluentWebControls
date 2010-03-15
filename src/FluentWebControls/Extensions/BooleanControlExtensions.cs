namespace FluentWebControls.Extensions
{
	public static class BooleanControlExtensions
	{
		public static CheckBoxData AsCheckBox(this BooleanControl input)
		{
			IBooleanControl data = input;
			return new CheckBoxData(data.Checked)
				.WithValue(data.Value)
				.WithId(data.Id)
				.WithIdPrefix(data.IdPrefix);
		}
	}
}