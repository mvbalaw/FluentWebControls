namespace FluentWebControls.Mapping
{
	public interface IBooleanMap : IModelMap
	{
		string Id { get; set; }
		bool IsChecked { get; set; }
	}
}