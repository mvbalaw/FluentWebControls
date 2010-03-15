using System;
using System.Collections.Generic;

namespace FluentWebControls
{
	public interface IChoiceControl : IWebControl
	{
		string IdWithPrefix { get; }
		IEnumerable<KeyValuePair<string, string>> ListItems { get; }
		string SelectedValue { get; }
	}

	public class ChoiceControl : WebControlBase, IChoiceControl
	{
		internal IEnumerable<KeyValuePair<string, string>> ListItems { private
			get; set; }
		internal string SelectedValue { private get; set; }
		string IChoiceControl.SelectedValue
		{
			get { return SelectedValue; }
		}
		string IChoiceControl.IdWithPrefix
		{
			get { return IdWithPrefix; }
		}
		IEnumerable<KeyValuePair<string, string>> IChoiceControl.ListItems
		{
			get { return ListItems; }
		}

		public override string ToString()
		{
			throw new NotImplementedException("You must choose the form of the output control with .As[something]()");
		}
	}
}