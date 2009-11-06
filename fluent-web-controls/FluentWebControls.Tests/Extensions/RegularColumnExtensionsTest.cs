using FluentAssert;

using FluentWebControls.Extensions;

using NUnit.Framework;

namespace FluentWebControls.Tests.Extensions
{
	public class RegularColumnExtensionsTest
	{
		public abstract class RegularColumnExtensionsTestBase
		{
			protected RegularColumn<TestData.Item> _regularColumn;

			[SetUp]
			public void BeforeEachTest()
			{
				_regularColumn = new RegularColumn<TestData.Item>(item => item.ItemName, "FieldName", "ColumnHeader");
			}
		}

		[TestFixture]
		public class When_asked_to_align_center : RegularColumnExtensionsTestBase
		{
			[Test]
			public void Should_return_a_RegularColumn_with_align_set_to_center()
			{
				var column = _regularColumn.AlignCenter();
				Assert.AreSame(_regularColumn, column);
				column.Align.ShouldBeEqualTo(AlignAttribute.Center);
			}
		}

		[TestFixture]
		public class When_asked_to_align_left : RegularColumnExtensionsTestBase
		{
			[Test]
			public void Should_return_a_RegularColumn_with_align_set_to_left()
			{
				var column = _regularColumn.AlignLeft();
				Assert.AreSame(_regularColumn, column);
				column.Align.ShouldBeEqualTo(AlignAttribute.Left);
			}
		}

		[TestFixture]
		public class When_asked_to_align_right : RegularColumnExtensionsTestBase
		{
			[Test]
			public void Should_return_a_RegularColumn_with_align_set_to_right()
			{
				var column = _regularColumn.AlignRight();
				Assert.AreSame(_regularColumn, column);
				column.Align.ShouldBeEqualTo(AlignAttribute.Right);
			}
		}
	}
}