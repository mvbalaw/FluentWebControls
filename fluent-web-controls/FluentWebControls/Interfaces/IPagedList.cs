using System.Collections.Generic;
using System.ComponentModel;

namespace FluentWebControls.Interfaces
{
	public interface IPagedList
	{
		int? PageNumber { set; }
		int? PageSize { set; }
		ListSortDirection SortDirection { set; }
		string SortProperty { set; }
	}

	public interface IPagedList<TReturn> : IPagedList
	{
		IEnumerable<TReturn> ToList();
		int Total();
	}

	public interface IPagedList<TFilter, TReturn> : IPagedList
	{
		IEnumerable<TReturn> ToList(TFilter filter);
		int Total(TFilter filter);
	}

	public interface IPagedList<TFilter1, TFilter2, TReturn> : IPagedList
	{
		IEnumerable<TReturn> ToList(TFilter1 filter1, TFilter2 filter2);
		int Total(TFilter1 filter1, TFilter2 filter2);
	}

	public interface IPagedList<TFilter1, TFilter2, TFilter3, TReturn> : IPagedList
	{
		IEnumerable<TReturn> ToList(TFilter1 filter1, TFilter2 filter2, TFilter3 filter3);
		int Total(TFilter1 filter1, TFilter2 filter2, TFilter3 filter3);
	}
}