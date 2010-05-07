using System;
using System.Collections.Generic;

namespace FluentWebControls.Mapping
{
	public abstract class ListUIMapWithItem<TItem, TDomain, TModel> : ListUIMap<TDomain, TModel>
		where TItem : class, new()
	{
		protected ListUIMapWithItem(TItem item, IEnumerable<TDomain> items)
			: base(items)
		{
			Item = item.ToNonNull();
		}

		public TItem Item { get; private set; }
	}
}