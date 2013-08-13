//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FluentWebControls.Interfaces
{
	internal class PagedList<TReturn> : IPagedList<TReturn>
	{
		private readonly IEnumerable<TReturn> _list;
		private int? _total;

		public PagedList(IEnumerable<TReturn> list)
		{
			_list = list;
			_total = null;
		}

		public int? PageNumber { set; private get; }
		public int? PageSize { set; private get; }
		public ListSortDirection SortDirection { set; private get; }
		public string SortProperty { set; private get; }

		public IEnumerable<TReturn> ToList()
		{
			IEnumerable<TReturn> temp;
			if (SortProperty != null)
			{
				var sortPropertyInfo = typeof(TReturn).GetProperty(SortProperty);
				Func<TReturn, object> selector = x => sortPropertyInfo.GetValue(x, null);
				temp = SortDirection == ListSortDirection.Ascending ? _list.OrderBy(selector) : _list.OrderByDescending(selector);
			}
			else
			{
				temp = _list;
			}

			if (!PageNumber.HasValue)
			{
				PageNumber = 0;
			}
			if (PageNumber > 0)
			{
				if (!PageSize.HasValue)
				{
					PageSize = Total();
				}
				if (PageNumber * PageSize > Total())
				{
					return temp.Skip(Total() - PageSize.Value);
				}
				return temp.Skip(PageNumber.Value * PageSize.Value).Take(PageSize.Value);
			}
			return temp;
		}

		public int Total()
		{
			if (_total == null)
			{
				_total = _list.Count();
			}
			return _total.Value;
		}
	}
}