using System.ComponentModel;
using FluentWebControls.Interfaces;

namespace FluentWebControls.Extensions
{
	public static class PagedListExtensions
	{
		public static IPagedList Ascending(this IPagedList pagedList)
		{
			pagedList.SortDirection = ListSortDirection.Ascending;
			return pagedList;
		}

		public static IPagedList Descending(this IPagedList pagedList)
		{
			pagedList.SortDirection = ListSortDirection.Descending;
			return pagedList;
		}

		public static IPagedList<TReturn> OrderBy<TReturn>(this IPagedList<TReturn> pagedList, string sortProperty, string sortDirection)
		{
			IPagedList temp = pagedList;
			temp.OrderBy(sortProperty, sortDirection);
			return pagedList;
		}

		public static IPagedList<TFilter, TReturn> OrderBy<TFilter, TReturn>(this IPagedList<TFilter, TReturn> pagedList, string sortProperty, string sortDirection)
		{
			IPagedList temp = pagedList;
			temp.OrderBy(sortProperty, sortDirection);
			return pagedList;
		}

		public static IPagedList<TFilter1, TFilter2, TReturn> OrderBy<TFilter1, TFilter2, TReturn>(this IPagedList<TFilter1, TFilter2, TReturn> pagedList, string sortProperty, string sortDirection)
		{
			IPagedList temp = pagedList;
			temp.OrderBy(sortProperty, sortDirection);
			return pagedList;
		}

		public static IPagedList<TFilter1, TFilter2, TFilter3, TReturn> OrderBy<TFilter1, TFilter2, TReturn, TFilter3>(this IPagedList<TFilter1, TFilter2, TFilter3, TReturn> pagedList, string sortProperty, string sortDirection)
		{
			IPagedList temp = pagedList;
			temp.OrderBy(sortProperty, sortDirection);
			return pagedList;
		}

		public static IPagedList OrderBy(this IPagedList pagedList, string sortProperty, string sortDirection)
		{
			pagedList.SortProperty = sortProperty;
			pagedList.SortDirection = sortDirection.ToSortDirection();
			return pagedList;
		}

		public static IPagedList OrderBy(this IPagedList pagedList, string sortProperty)
		{
			pagedList.SortProperty = sortProperty;
			pagedList.SortDirection = ListSortDirection.Ascending;
			return pagedList;
		}

		public static IPagedList<TReturn> Page<TReturn>(this IPagedList<TReturn> pagedList, int pageNumber, int pageSize)
		{
			IPagedList temp = pagedList;
			temp.Page(pageNumber, pageSize);
			return pagedList;
		}

		public static IPagedList<TFilter, TReturn> Page<TFilter, TReturn>(this IPagedList<TFilter, TReturn> pagedList, int pageNumber, int pageSize)
		{
			IPagedList temp = pagedList;
			temp.Page(pageNumber, pageSize);
			return pagedList;
		}

		public static IPagedList<TFilter1, TFilter2, TReturn> Page<TFilter1, TFilter2, TReturn>(this IPagedList<TFilter1, TFilter2, TReturn> pagedList, int pageNumber, int pageSize)
		{
			IPagedList temp = pagedList;
			temp.Page(pageNumber, pageSize);
			return pagedList;
		}

		public static IPagedList<TFilter1, TFilter2, TFilter3, TReturn> Page<TFilter1, TFilter2, TFilter3, TReturn>(this IPagedList<TFilter1, TFilter2, TFilter3, TReturn> pagedList, int pageNumber, int pageSize)
		{
			IPagedList temp = pagedList;
			temp.Page(pageNumber, pageSize);
			return pagedList;
		}

		/// <summary>
		///		one based page number and page size
		/// </summary>
		public static IPagedList Page(this IPagedList pagedList, int pageNumber, int pageSize)
		{
			pagedList.PageNumber = pageNumber <= 0 ? 1 : pageNumber;
			pagedList.PageSize = pageSize <= 0 ? 5 : pageSize;
			return pagedList;
		}

		/// <summary>
		///		one based page number
		/// </summary>
		public static IPagedList PageNumber(this IPagedList pagedList, int pageNumber)
		{
			pagedList.PageNumber = pageNumber <= 0 ? 1 : pageNumber;
			return pagedList;
		}

		/// <summary>
		///		one based number of items per page
		/// </summary>
		public static IPagedList PageSize(this IPagedList pagedList, int pageSize)
		{
			pagedList.PageSize = pageSize <= 0 ? 5 : pageSize;
			return pagedList;
		}
	}
}