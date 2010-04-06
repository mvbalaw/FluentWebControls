using FluentAssert;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class AlignAttributeTest
	{
		[TestFixture]
		public class When_you_call_ToString_on_AlignAttribute
		{
			[Test]
			public void Should_return_the_text_with_align_set_to_the_Default_Text_selected()
			{
				AlignAttribute.Left.ToString().ShouldBeEqualTo(" align='left'");
			}
		}
	}
}