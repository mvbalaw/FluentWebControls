using System;

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
			[ExpectedException(typeof(NullReferenceException))]
			public void Should_throw_an_exception_for_null_pagedList()
			{
				ScrollableGrid.For<TestData.Item>(null, null, ControllerName, ActionName);
			}
		}
	}
}