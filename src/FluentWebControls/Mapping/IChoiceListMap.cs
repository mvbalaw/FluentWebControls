using System.Collections.Generic;

namespace FluentWebControls.Mapping
{
	public interface IChoiceListMap
	{
		string Id { get; }
		IEnumerable<KeyValuePair<string, string>> ListItems { get; }
		string SelectedValue { get; }
	}
}