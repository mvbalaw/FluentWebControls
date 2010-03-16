namespace FluentWebControls.Mapping
{
	public class BooleanMap
	{
		public BooleanMap(string id, bool value)
		{
			Id = id;
			IsChecked = value;
		}

		public string Id { get; set;}
		public bool IsChecked { get; set;}
	}
}