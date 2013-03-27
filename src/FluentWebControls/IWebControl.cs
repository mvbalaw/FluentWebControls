using Microsoft.Build.Framework.XamlTypes;

namespace FluentWebControls
{
	public interface IWebControl
	{
		string Id { get; set; }
		string IdPrefix { get; set; }
		string Name { get; set; }
		string NamePrefix { get; set; }
		NameValuePair Data { get; set; }
	}
}