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
				_commandColumn.FieldName.ShouldBeEqualTo("ItemName");
			}

			[Test]
			[ExpectedException(typeof(NullReferenceException))]
			public void Should_throw_an_exception_if_getItemId_expression_is_null()
			{
				CommandGridColumn.For((Expression<Func<TestData.Item, string>>)null, "Edit");
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
				_commandColumn.FieldName.ShouldBeEqualTo("ItemId");
			}

			[Test]
			[ExpectedException(typeof(NullReferenceException))]
			public void Should_throw_an_exception_if_getItemId_expression_is_null()
			{
				CommandGridColumn.For((Expression<Func<TestData.Item, int>>)null, "Edit");
			}
		}
	}
}