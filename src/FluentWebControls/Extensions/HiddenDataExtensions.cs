using System;

namespace FluentWebControls.Extensions
{
	public static class HiddenDataExtensions
	{
		[Obsolete("use .WithValue(foo)")]
		public static HiddenData Text(this HiddenData hiddenData, string text)
		{
			return hiddenData.WithValue(text);
		}

		public static HiddenData WithValue(this HiddenData hiddenData, string value)
		{
			hiddenData.Value = value;
			return hiddenData;
		}
	}
}