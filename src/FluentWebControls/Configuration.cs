using FluentWebControls.Interfaces;

namespace FluentWebControls
{
	public static class Configuration
	{
		public static IBusinessObjectPropertyMetaDataFactory ValidationMetaDataFactory { get; set; }
		public static IPathUtility PathUtility { get; set; }
	}
}