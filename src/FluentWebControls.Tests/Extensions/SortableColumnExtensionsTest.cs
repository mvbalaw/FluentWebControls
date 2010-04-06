using FluentAssert;

using FluentWebControls.Extensions;

using NUnit.Framework;

namespace FluentWebControls.Tests.Extensions
{
	public class SortableColumnExtensionsTest
	{
		public abstract class SortableColumnExtensionsTestBase
		{
			protected SortableColumn<TestData.Item> _sortableColumn;

			[SetUp]
			public void BeforeEachTest()
			{
				_sortableColumn = new SortableColumn<TestData.Item>(item => item.ItemName, "FieldName", "ColumnHeader");
			}
		}

		[TestFixture]
		public class When_asked_to_align_center : SortableColumnExtensionsTestBase
		{
			[Test]
			public void Should_return_a_SortableColumn_with_align_set_to_center()
			{
				var column = _sortableColumn.AlignCenter();
				Assert.AreSame(_sortableColumn, column);
				column.Align.ShouldBeEqualTo(AlignAttribute.Center);
			}
		}

		[TestFixture]
		public class When_asked_to_align_left : SortableColumnExtensionsTestBase
		{
			[Test]
			public void Should_return_a_SortableColumn_with_align_set_to_left()
			{
				var column = _sortableColumn.AlignLeft();
				Assert.AreSame(_sortableColumn, column);
				column.Align.ShouldBeEqualTo(AlignAttribute.Left);
			}
		}

		[TestFixture]
		public class When_asked_to_align_right : SortableColumnExtensionsTestBase
		{
			[Test]
			public void Should_return_a_SortableColumn_with_align_set_to_right()
			{
				var column = _sortableColumn.AlignRight();
				Assert.AreSame(_sortableColumn, column);
				column.Align.ShouldBeEqualTo(AlignAttribute.Right);
			}
		}
	}
}