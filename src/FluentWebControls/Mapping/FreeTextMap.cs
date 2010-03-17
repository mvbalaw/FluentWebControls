using System;

namespace FluentWebControls.Mapping
{
	public class FreeTextMap<TDomain> : IFreeTextMap
	{
		private readonly Func<TDomain, string> _getValue;
		private readonly TDomain _item;

		public FreeTextMap(TDomain item, string id, Func<TDomain, string> getValue)
		{
			Id = id;
			_item = item;
			_getValue = getValue;
		}

		public string Id { get; private set; }

		public string Value
		{
			get { return _getValue(_item); }
		}
	}
}