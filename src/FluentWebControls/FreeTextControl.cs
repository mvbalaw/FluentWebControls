using System;

namespace FluentWebControls
{
	public interface IFreeTextControl : IWebControl
	{
		string IdWithPrefix { get; }
		string Value { get; }
	}

	public class FreeTextControl : WebControlBase, IFreeTextControl
	{
		internal string Value { private get; set; }
		string IFreeTextControl.IdWithPrefix
		{
			get { return IdWithPrefix; }
		}
		string IFreeTextControl.Value
		{
			get { return Value; }
		}

		public override string ToString()
		{
			throw new NotImplementedException("You must choose the form of the output control with .As[something]()");
		}
	}
}