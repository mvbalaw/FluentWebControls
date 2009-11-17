using System;
using System.Linq.Expressions;

using FluentAssert;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class CommandGridColumnTest
	{
		[TestFixture]
		public class When_asked_to_create_a_command_column_for_a_string_function
		{
			private CommandColumn<TestData.Item> _commandColumn;

			[SetUp]
			public void BeforeEachTest()
			{
				Expression<Func<TestData.Item, string>> expr = item => item.ItemName;
				_commandColumn = CommandGridColumn.For(expr, "Edit");
			}

			[Test]
			public void Should_call_the_function_when_GetItemId_is_called()
			{
				_commandColumn.GetItemId(new TestData.Item(10, "Item")).ShouldBeEqualTo("Item");
			}

			[Test]
			public void Should_set_the_ActionName()
			{
				_commandColumn.ActionName.ShouldBeEqualTo("Edit");
			}

			[Test]
			public void Should_set_the_FieldName()
			{
				_commandColumn.FieldName.ShouldBeEqualTo("itemName");
			}

			[Test]
			public void Should_throw_an_exception_if_getItemId_expression_is_null()
			{
				Assert.Throws<NullReferenceException>(() => CommandGridColumn.For((Expression<Func<TestData.Item, string>>)null, "Edit"));
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_command_column_for_a_string_function_with_Controller_function
		{
			private CommandColumn<TestData.Item> _commandColumn;

			public class FakeController
			{
				public string Edit()
				{
					return "";
				}
			}

			[SetUp]
			public void BeforeEachTest()
			{
				TestData.Item source = new TestData.Item(6, "James");
				_commandColumn = CommandGridColumn.For<TestData.Item, FakeController>(x => x.ItemName, x => x.ItemName, c => c.Edit());
			}

			[Test]
			public void Should_call_the_function_when_GetItemId_is_called()
			{
				_commandColumn.GetItemId(new TestData.Item(10, "Item")).ShouldBeEqualTo("Item");
			}

			[Test]
			public void Should_set_the_ActionName()
			{
				_commandColumn.ActionName.ShouldBeEqualTo("Edit");
			}

			[Test]
			public void Should_set_the_FieldName()
			{
				_commandColumn.FieldName.ShouldBeEqualTo("itemName");
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_command_column_for_an_int_function
		{
			private CommandColumn<TestData.Item> _commandColumn;

			[SetUp]
			public void BeforeEachTest()
			{
				Expression<Func<TestData.Item, int>> expr = item => item.ItemId;
				_commandColumn = CommandGridColumn.For(expr, "Edit");
			}

			[Test]
			public void Should_call_the_function_when_GetItemId_is_called()
			{
				_commandColumn.GetItemId(new TestData.Item(10, "Item")).ShouldBeEqualTo("10");
			}

			[Test]
			public void Should_set_the_ActionName()
			{
				_commandColumn.ActionName.ShouldBeEqualTo("Edit");
			}

			[Test]
			public void Should_set_the_FieldName()
			{
				_commandColumn.FieldName.ShouldBeEqualTo("itemId");
			}

			[Test]
			public void Should_throw_an_exception_if_getItemId_expression_is_null()
			{
				Assert.Throws<NullReferenceException>(() => CommandGridColumn.For((Expression<Func<TestData.Item, int>>)null, "Edit"));
			}
		}
	}
}