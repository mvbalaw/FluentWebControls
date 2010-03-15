using System;

namespace FluentWebControls
{
	public interface IBooleanControl : IWebControl
	{
		bool Checked { get; }
		string IdWithPrefix { get; }
		string Value { get; }
	}

	public class BooleanControl : WebControlBase, IBooleanControl
	{
		internal bool Checked { private get; set; }
		internal string Value { private get; set; }
		string IBooleanControl.IdWithPrefix
		{
			get { return IdWithPrefix; }
		}
		bool IBooleanControl.Checked
		{
			get { return Checked; }
		}
		string IBooleanControl.Value
		{
			get { return Value; }
		}

		public override string ToString()
		{
			throw new NotImplementedException("You must choose the form of the output control with .As[something]()");
		}
	}
}