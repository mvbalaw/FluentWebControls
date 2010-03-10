using System;

using FluentAssert;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class CommandGridColumnTest
	{
		private class FakeController
		{
			public object Edit()
			{
				return "";
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_command_column_for_a_string_function
		{
			private GridCommandColumn<TestData.Item> _gridCommandColumn;

			[SetUp]
			public void BeforeEachTest()
			{
				_gridCommandColumn = CommandGridColumn.For<TestData.Item, FakeController>(item => item.ItemName, x => x.Edit());
			}

			[Test]
			public void Should_call_the_function_when_GetItemId_is_called()
			{
				_gridCommandColumn.GetItemId(new TestData.Item(10, "Item")).ShouldBeEqualTo("Item");
			}

			[Test]
			public void Should_set_the_ActionName()
			{
				_gridCommandColumn.ActionName.ShouldBeEqualTo("Edit");
			}

			[Test]
			public void Should_set_the_FieldName()
			{
				_gridCommandColumn.FieldName.ShouldBeEqualTo("itemName");
			}

			[Test]
			public void Should_throw_an_exception_if_getItemId_expression_is_null()
			{
				Assert.Throws<NullReferenceException>(() => CommandGridColumn.For<TestData.Item, FakeController>(null, x => x.Edit()));
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_command_column_for_a_string_function_with_Controller_function
		{
			private GridCommandColumn<TestData.Item> _gridCommandColumn;

			[SetUp]
			public void BeforeEachTest()
			{
				_gridCommandColumn = CommandGridColumn.For<TestData.Item, FakeController>(x => x.ItemName, x => x.ItemName, c => c.Edit());
			}

			[Test]
			public void Should_call_the_function_when_GetItemId_is_called()
			{
				_gridCommandColumn.GetItemId(new TestData.Item(10, "Item")).ShouldBeEqualTo("Item");
			}

			[Test]
			public void Should_set_the_ActionName()
			{
				_gridCommandColumn.ActionName.ShouldBeEqualTo("Edit");
			}

			[Test]
			public void Should_set_the_FieldName()
			{
				_gridCommandColumn.FieldName.ShouldBeEqualTo("itemName");
			}

			public class FakeController
			{
				public string Edit()
				{
					return "";
				}
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_command_column_for_an_int_function
		{
			private GridCommandColumn<TestData.Item> _gridCommandColumn;

			[SetUp]
			public void BeforeEachTest()
			{
				_gridCommandColumn = CommandGridColumn.For<TestData.Item, FakeController>(item => item.ItemId.ToString(), item => item.ItemId, x => x.Edit());
			}

			[Test]
			public void Should_call_the_function_when_GetItemId_is_called()
			{
				_gridCommandColumn.GetItemId(new TestData.Item(10, "Item")).ShouldBeEqualTo("10");
			}

			[Test]
			public void Should_set_the_ActionName()
			{
				_gridCommandColumn.ActionName.ShouldBeEqualTo("Edit");
			}

			[Test]
			public void Should_set_the_FieldName()
			{
				_gridCommandColumn.FieldName.ShouldBeEqualTo("itemId");
			}

			[Test]
			public void Should_throw_an_exception_if_getItemId_expression_is_null()
			{
				Assert.Throws<NullReferenceException>(() => CommandGridColumn.For<TestData.Item, FakeController>(null, x => x.Edit()));
			}
		}
	}
}