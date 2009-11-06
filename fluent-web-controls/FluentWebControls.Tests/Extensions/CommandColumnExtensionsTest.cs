using FluentAssert;

using FluentWebControls.Extensions;

using NUnit.Framework;

namespace FluentWebControls.Tests.Extensions
{
	public class CommandColumnExtensionsTest
	{
		public abstract class CommandColumnExtensionsTestBase
		{
			protected CommandColumn<TestData.Item> _commandColumn;

			[SetUp]
			public void BeforeEachTest()
			{
				_commandColumn = new CommandColumn<TestData.Item>(item => item.ItemName, "FieldName", "ActionName");
			}
		}

		[TestFixture]
		public class When_asked_to_align_center : CommandColumnExtensionsTestBase
		{
			[Test]
			public void Should_return_a_CommandColumn_with_align_set_to_center()
			{
				var column = _commandColumn.AlignCenter();
				Assert.AreSame(_commandColumn, column);
				column.Align.ShouldBeEqualTo(AlignAttribute.Center);
			}
		}

		[TestFixture]
		public class When_asked_to_align_left : CommandColumnExtensionsTestBase
		{
			[Test]
			public void Should_return_a_CommandColumn_with_align_set_to_left()
			{
				var column = _commandColumn.AlignLeft();
				Assert.AreSame(_commandColumn, column);
				column.Align.ShouldBeEqualTo(AlignAttribute.Left);
			}
		}

		[TestFixture]
		public class When_asked_to_align_right : CommandColumnExtensionsTestBase
		{
			[Test]
			public void Should_return_a_CommandColumn_with_align_set_to_right()
			{
				var column = _commandColumn.AlignRight();
				Assert.AreSame(_commandColumn, column);
				column.Align.ShouldBeEqualTo(AlignAttribute.Right);
			}
		}
	}
}