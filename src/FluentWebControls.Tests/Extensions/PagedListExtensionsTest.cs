//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using System.ComponentModel;

using FluentAssert;

using FluentWebControls.Extensions;
using MvbaCore.Interfaces;
using NUnit.Framework;

using Rhino.Mocks;

namespace FluentWebControls.Tests.Extensions
{
	public class PagedListExtensionsTest
	{
		public class PagedListExtensionsTestBase<TPagedListInterface>
			where TPagedListInterface : class
		{
			protected TPagedListInterface _pagedList;

			[TearDown]
			public void AfterEachTest()
			{
				_pagedList.VerifyAllExpectations();
			}

			[SetUp]
			public void BeforeEachTest()
			{
				_pagedList = MockRepository.GenerateMock<TPagedListInterface>();
			}
		}

		[TestFixture]
		public class When_asked_to_OrderBy_with_a_sort_property : PagedListExtensionsTestBase<IPagedList>
		{
			[Test]
			public void Should_set_the_SortProperty_property_on_the_PagedList()
			{
				const string sortProperty = "Name";

				_pagedList.Expect(x => x.SortProperty = sortProperty);

				var result = _pagedList.OrderBy(sortProperty);
				result.ShouldNotBeNull();
			}
		}

		[TestFixture]
		public class When_asked_to_OrderBy_with_a_sort_property_and_sort_direction : PagedListExtensionsTestBase<IPagedList>
		{
			[Test]
			public void Should_set_the_SortDirection_property_on_the_PagedList()
			{
				const string sortProperty = "Name";
				const string sortDirection = "Desc";

				_pagedList.Expect(x => x.SortDirection = ListSortDirection.Descending);

				var result = _pagedList.OrderBy(sortProperty, sortDirection);
				result.ShouldNotBeNull();
			}

			[Test]
			public void Should_set_the_SortProperty_property_on_the_PagedList()
			{
				const string sortProperty = "Name";
				const string sortDirection = "Desc";

				_pagedList.Expect(x => x.SortProperty = sortProperty);

				var result = _pagedList.OrderBy(sortProperty, sortDirection);
				result.ShouldNotBeNull();
			}
		}

		[TestFixture]
		public class When_asked_to_OrderBy_with_a_sort_property_and_sort_direction_TFilter1_TFilter2_TFilter3_TReturn : PagedListExtensionsTestBase<IPagedList<int, int, int, int>>
		{
			[Test]
			public void Should_set_the_SortDirection_property_on_the_PagedList()
			{
				const string sortProperty = "Name";
				const string sortDirection = "Desc";

				_pagedList.Expect(x => x.SortDirection = ListSortDirection.Descending);

				IPagedList result = _pagedList.OrderBy(sortProperty, sortDirection);
				result.ShouldNotBeNull();
			}

			[Test]
			public void Should_set_the_SortProperty_property_on_the_PagedList()
			{
				const string sortProperty = "Name";
				const string sortDirection = "Desc";

				_pagedList.Expect(x => x.SortProperty = sortProperty);

				IPagedList result = _pagedList.OrderBy(sortProperty, sortDirection);
				result.ShouldNotBeNull();
			}
		}

		[TestFixture]
		public class When_asked_to_OrderBy_with_a_sort_property_and_sort_direction_TFilter1_TFilter2_TReturn : PagedListExtensionsTestBase<IPagedList<int, int, int>>
		{
			[Test]
			public void Should_set_the_SortDirection_property_on_the_PagedList()
			{
				const string sortProperty = "Name";
				const string sortDirection = "Desc";

				_pagedList.Expect(x => x.SortDirection = ListSortDirection.Descending);

				IPagedList result = _pagedList.OrderBy(sortProperty, sortDirection);
				result.ShouldNotBeNull();
			}

			[Test]
			public void Should_set_the_SortProperty_property_on_the_PagedList()
			{
				const string sortProperty = "Name";
				const string sortDirection = "Desc";

				_pagedList.Expect(x => x.SortProperty = sortProperty);

				IPagedList result = _pagedList.OrderBy(sortProperty, sortDirection);
				result.ShouldNotBeNull();
			}
		}

		[TestFixture]
		public class When_asked_to_OrderBy_with_a_sort_property_and_sort_direction_TFilter_TReturn : PagedListExtensionsTestBase<IPagedList<int, int>>
		{
			[Test]
			public void Should_set_the_SortDirection_property_on_the_PagedList()
			{
				const string sortProperty = "Name";
				const string sortDirection = "Desc";

				_pagedList.Expect(x => x.SortDirection = ListSortDirection.Descending);

				IPagedList result = _pagedList.OrderBy(sortProperty, sortDirection);
				result.ShouldNotBeNull();
			}

			[Test]
			public void Should_set_the_SortProperty_property_on_the_PagedList()
			{
				const string sortProperty = "Name";
				const string sortDirection = "Desc";

				_pagedList.Expect(x => x.SortProperty = sortProperty);

				IPagedList result = _pagedList.OrderBy(sortProperty, sortDirection);
				result.ShouldNotBeNull();
			}
		}

		[TestFixture]
		public class When_asked_to_OrderBy_with_a_sort_property_and_sort_direction_TReturn : PagedListExtensionsTestBase<IPagedList<int>>
		{
			[Test]
			public void Should_set_the_SortDirection_property_on_the_PagedList()
			{
				const string sortProperty = "Name";
				const string sortDirection = "Desc";

				_pagedList.Expect(x => x.SortDirection = ListSortDirection.Descending);

				IPagedList result = _pagedList.OrderBy(sortProperty, sortDirection);
				result.ShouldNotBeNull();
			}

			[Test]
			public void Should_set_the_SortProperty_property_on_the_PagedList()
			{
				const string sortProperty = "Name";
				const string sortDirection = "Desc";

				_pagedList.Expect(x => x.SortProperty = sortProperty);

				IPagedList result = _pagedList.OrderBy(sortProperty, sortDirection);
				result.ShouldNotBeNull();
			}
		}

		[TestFixture]
		public class When_asked_to_make_the_sort_direction_Ascending : PagedListExtensionsTestBase<IPagedList>
		{
			[Test]
			public void Should_set_the_SortDirection_property_on_the_PagedList()
			{
				_pagedList.Expect(x => x.SortDirection = ListSortDirection.Ascending);

				var result = _pagedList.Ascending();
				result.ShouldNotBeNull();
			}
		}

		[TestFixture]
		public class When_asked_to_make_the_sort_direction_Descending : PagedListExtensionsTestBase<IPagedList>
		{
			[Test]
			public void Should_set_the_SortDirection_property_on_the_PagedList()
			{
				_pagedList.Expect(x => x.SortDirection = ListSortDirection.Descending);

				var result = _pagedList.Descending();
				result.ShouldNotBeNull();
			}
		}

		[TestFixture]
		public class When_asked_to_set_the_page_number : PagedListExtensionsTestBase<IPagedList>
		{
			[Test]
			public void Should_set_the_PageNumber_property_on_the_PagedList()
			{
				const int pageNumber = 4;

				_pagedList.Expect(x => x.PageNumber = pageNumber);

				var result = _pagedList.PageNumber(pageNumber);
				result.ShouldNotBeNull();
			}
		}

		[TestFixture]
		public class When_asked_to_set_the_page_number_and_page_size : PagedListExtensionsTestBase<IPagedList>
		{
			[Test]
			public void Should_set_the_PageNumber_property_on_the_PagedList()
			{
				const int pageNumber = 4;
				const int pageSize = 5;

				_pagedList.Expect(x => x.PageNumber = pageNumber);

				var result = _pagedList.Page(pageNumber, pageSize);
				result.ShouldNotBeNull();
			}

			[Test]
			public void Should_set_the_PageSize_property_on_the_PagedList()
			{
				const int pageNumber = 4;
				const int pageSize = 5;

				_pagedList.Expect(x => x.PageSize = pageSize);

				var result = _pagedList.Page(pageNumber, pageSize);
				result.ShouldNotBeNull();
			}
		}

		[TestFixture]
		public class When_asked_to_set_the_page_size : PagedListExtensionsTestBase<IPagedList>
		{
			[Test]
			public void Should_set_the_PageSize_property_on_the_PagedList()
			{
				const int pageSize = 5;

				_pagedList.Expect(x => x.PageSize = pageSize);

				var result = _pagedList.PageSize(pageSize);
				result.ShouldNotBeNull();
			}
		}
	}
}