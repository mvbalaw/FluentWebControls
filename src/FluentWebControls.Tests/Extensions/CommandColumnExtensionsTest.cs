using FluentAssert;

using FluentWebControls.Extensions;

using NUnit.Framework;

namespace FluentWebControls.Tests.Extensions
{
	public class CommandColumnExtensionsTest
	{
		public abstract class CommandColumnExtensionsTestBase
		{
			protected GridCommandColumn<TestData.Item> _gridCommandColumn;

			[SetUp]
			public void BeforeEachTest()
			{
				_gridCommandColumn = new GridCommandColumn<TestData.Item>(item => item.ItemName, "FieldName", "ActionName");
			}
		}

		[TestFixture]
		public class When_asked_to_align_center : CommandColumnExtensionsTestBase
		{
			[Test]
			public void Should_return_a_CommandColumn_with_align_set_to_center()
			{
				var column = _gridCommandColumn.AlignCenter();
				Assert.AreSame(_gridCommandColumn, column);
				column.Align.ShouldBeEqualTo(AlignAttribute.Center);
			}
		}

		[TestFixture]
		public class When_asked_to_align_left : CommandColumnExtensionsTestBase
		{
			[Test]
			public void Should_return_a_CommandColumn_with_align_set_to_left()
			{
				var column = _gridCommandColumn.AlignLeft();
				Assert.AreSame(_gridCommandColumn, column);
				column.Align.ShouldBeEqualTo(AlignAttribute.Left);
			}
		}

		[TestFixture]
		public class When_asked_to_align_right : CommandColumnExtensionsTestBase
		{
			[Test]
			public void Should_return_a_CommandColumn_with_align_set_to_right()
			{
				var column = _gridCommandColumn.AlignRight();
				Assert.AreSame(_gridCommandColumn, column);
				column.Align.ShouldBeEqualTo(AlignAttribute.Right);
			}
		}
	}
}