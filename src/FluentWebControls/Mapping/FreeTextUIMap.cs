using System;

namespace FluentWebControls.Mapping
{
	public class FreeTextUIMap<TDomain> : IFreeTextUIMap
	{
		private readonly Func<TDomain, string> _getValue;
		private readonly TDomain _item;

		public FreeTextUIMap(TDomain item, string id, Func<TDomain, string> getValue)
		{
			Id = id;
			_item = item;
			_getValue = getValue;
		}

		public string Id { get; private set; }

		public string IdPrefix { get; set; }
		public string Value
		{
			get { return _getValue(_item); }
		}
	}
}