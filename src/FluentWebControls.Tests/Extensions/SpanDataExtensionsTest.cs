using FluentAssert;

using FluentWebControls.Extensions;

using NUnit.Framework;

namespace FluentWebControls.Tests.Extensions
{
	public class SpanDataExtensionsTest
	{
		public abstract class SpanDataExtensionsTestBase
		{
			protected SpanData SpanData;

			[SetUp]
			public void BeforeEachTest()
			{
				SpanData = new SpanData("value");
			}
		}

		[TestFixture]
		public class When_asked_to_assign_CssClass : SpanDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_SpanData_With_CssClass_initialized()
			{
				const string cssClass = "Link";
				var span = SpanData.WithCssClass(cssClass);
				Assert.AreSame(SpanData, span);
				span.ToString().ParseHtmlTag()["class"].ShouldBeEqualTo(cssClass);
				SpanData.ToString().Contains(cssClass).ShouldBeTrue();
			}
		}
	}
}