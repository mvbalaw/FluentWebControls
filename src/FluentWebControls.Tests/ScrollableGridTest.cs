using System;

using FluentWebControls.Interfaces;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class ScrollableGridTest
	{
		[TestFixture]
		public class When_given_a_PagedList
		{
			private const string ActionName = "ActionName";
			private const string ControllerName = "ControllerName";

			[Test]
			public void Should_throw_an_exception_for_null_pagedList()
			{
				Assert.Throws<NullReferenceException>(() => ScrollableGrid.For((IPagedList<TestData.Item>)null, null, ControllerName, ActionName));
			}
		}
	}
}