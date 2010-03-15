using System;

namespace FluentWebControls.Mapping
{
	public class UIColumn<TDomain>
	{
		private readonly Func<TDomain, string> _getText;

		public UIColumn(Func<TDomain, string> getText)
		{
			_getText = getText;
		}

		public Func<TDomain, string> TextMethod
		{
			get { return _getText; }
		}
	}
}