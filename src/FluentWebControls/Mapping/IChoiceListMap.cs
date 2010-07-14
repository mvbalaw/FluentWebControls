using System.Collections.Generic;

using FluentWebControls.Interfaces;

namespace FluentWebControls.Mapping
{
	public interface IChoiceListMap
	{
		string Id { get; }
		IEnumerable<KeyValuePair<string, string>> ListItems { get; }
		string SelectedValue { get; }
		IPropertyMetaData Validation { get; }
		IEnumerable<string> SelectedValues { get; }
	}
}