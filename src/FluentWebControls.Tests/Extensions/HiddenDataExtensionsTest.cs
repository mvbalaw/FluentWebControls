using FluentAssert;

using FluentWebControls.Extensions;

using NUnit.Framework;

namespace FluentWebControls.Tests.Extensions
{
	public class HiddenDataExtensionsTest
	{
		public abstract class HiddenDataExtensionsTestBase
		{
			protected HiddenData _hiddenData;

			[SetUp]
			public void BeforeEachTest()
			{
				_hiddenData = new HiddenData().WithId("Id");
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Text : HiddenDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_HiddenData_With_Text_initialized()
			{
				const string text = "Text";
				var hiddenData = _hiddenData.WithValue(text);
				Assert.AreSame(_hiddenData, hiddenData);
				TestWebControlsUtility.HtmlParser(_hiddenData.ToString())["value"].ShouldBeEqualTo(text);
				hiddenData.ToString().Contains(text).ShouldBeTrue();
			}
		}
	}
}