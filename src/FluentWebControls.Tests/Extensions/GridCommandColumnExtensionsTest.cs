using FluentAssert;

using FluentWebControls.Extensions;

using NUnit.Framework;

namespace FluentWebControls.Tests.Extensions
{
	public class GridCommandColumnExtensionsTest
	{
		public abstract class GridCommandColumnExtensionsTestBase
		{
			protected GridCommandColumn<TestData.Item> GridCommandColumn;

			[SetUp]
			public void BeforeEachTest()
			{
				GridCommandColumn = new GridCommandColumn<TestData.Item>(item => item.ItemName, "FieldName", "ActionName");
			}
		}

		[TestFixture]
		public class When_asked_to_align_center : GridCommandColumnExtensionsTestBase
		{
			[Test]
			public void Should_return_a_CommandColumn_with_align_set_to_center()
			{
				var column = GridCommandColumn.AlignCenter();
				Assert.AreSame(GridCommandColumn, column);
				column.Align.ShouldBeEqualTo(AlignAttribute.Center);
			}
		}

		[TestFixture]
		public class When_asked_to_align_left : GridCommandColumnExtensionsTestBase
		{
			[Test]
			public void Should_return_a_CommandColumn_with_align_set_to_left()
			{
				var column = GridCommandColumn.AlignLeft();
				Assert.AreSame(GridCommandColumn, column);
				column.Align.ShouldBeEqualTo(AlignAttribute.Left);
			}
		}

		[TestFixture]
		public class When_asked_to_align_right : GridCommandColumnExtensionsTestBase
		{
			[Test]
			public void Should_return_a_CommandColumn_with_align_set_to_right()
			{
				var column = GridCommandColumn.AlignRight();
				Assert.AreSame(GridCommandColumn, column);
				column.Align.ShouldBeEqualTo(AlignAttribute.Right);
			}
		}
	}
}