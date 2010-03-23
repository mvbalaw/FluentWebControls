using FluentWebControls.Interfaces;

namespace FluentWebControls.Mapping
{
	public class BooleanMap : IBooleanMap, IFreeTextMap
	{
		public BooleanMap(string id, bool value)
		{
			Id = id;
			IsChecked = value;
		}

		public string Id { get; set; }
		public bool IsChecked { get; set; }
		string IFreeTextMap.Value
		{
			get { return IsChecked.ToString(); }
		}
		public IPropertyMetaData Validation { get; set; }
	}
}