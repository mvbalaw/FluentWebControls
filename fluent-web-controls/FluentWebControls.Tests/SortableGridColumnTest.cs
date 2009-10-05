using System;
using System.Linq.Expressions;

using FluentAssert;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class SortableGridColumnTest
	{
		[TestFixture]
		public class When_asked_to_create_a_sortable_column
		{
			private SortableColumn<Item> _sortableColumn;

			[SetUp]
			public void BeforeEachTest()
			{
				Expression<Func<Item, int?>> expr = item => item.ItemId;
				_sortableColumn = SortableGridColumn.For(expr);
			}

			[Test]
			public void Should_call_the_function_when_GetItemValue_is_called()
			{
				_sortableColumn.GetItemValue(new Item(10)).ShouldBeEqualTo("10");
			}

			[Test]
			public void Should_set_the_FieldName()
			{
				_sortableColumn.FieldName.ShouldBeEqualTo("ItemId");
			}

			[Test]
			[ExpectedException(typeof(NullReferenceException))]
			public void Should_throw_an_exception_if_columnName_expression_is_null()
			{
				SortableGridColumn.For((Expression<Func<Item, int?>>)null);
			}

			public class Item
			{
				public Item(int itemId)
				{
					ItemId = itemId;
				}

				public int? ItemId { get; set; }
				public string ItemName { get; set; }
			}
		}
	}
}