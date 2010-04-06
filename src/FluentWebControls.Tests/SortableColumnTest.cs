using FluentAssert;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class SortableColumnTest
	{
		[TestFixture]
		public class When_asked_for_its_Type
		{
			[Test]
			public void Should_return_Sortable()
			{
				var column = new SortableColumn<int>(i => i.ToString(), "Bar", "Int");
				column.Type.ShouldBeEqualTo(GridColumnType.Sortable);
			}
		}
	}
}