using FluentWebControls.Interfaces;

namespace FluentWebControls
{
	public static class Configuration
	{
		public static IPathUtility PathUtility { get; set; }
		public static IBusinessObjectPropertyMetaDataFactory ValidationMetaDataFactory { get; set; }
	}
}