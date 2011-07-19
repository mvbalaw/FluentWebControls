using FluentWebControls.Interfaces;

namespace FluentWebControls.Mapping
{
	public interface IFreeTextMap : IModelMap
	{
		string Id { get; }
		IPropertyMetaData Validation { get; set; }
		string Value { get; }
	}
}