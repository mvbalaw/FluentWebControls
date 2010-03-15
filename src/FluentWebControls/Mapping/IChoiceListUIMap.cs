using System.Collections.Generic;

namespace FluentWebControls.Mapping
{
	public interface IChoiceListUIMap
	{
		string Id { get; }
		string IdPrefix { get; set; }
		IEnumerable<KeyValuePair<string, string>> ListItems { get; }
		string SelectedValue { get; }
	}
}