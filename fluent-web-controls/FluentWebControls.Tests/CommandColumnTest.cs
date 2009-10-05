using FluentAssert;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class CommandColumnTest
	{
		[TestFixture]
		public class When_asked_for_its_Type
		{
			[Test]
			public void Should_return_Sortable()
			{
				CommandColumn<int> column = new CommandColumn<int>(i => i.ToString(), "Bar", "Int");
				column.Type.ShouldBeEqualTo(GridColumnType.Command);
			}
		}
	}
}