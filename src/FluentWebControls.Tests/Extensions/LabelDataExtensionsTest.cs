using FluentAssert;

using FluentWebControls.Extensions;

using NUnit.Framework;

namespace FluentWebControls.Tests.Extensions
{
	public class LabelDataExtensionsTest
	{
		public abstract class LabelDataExtensionsTestBase
		{
			protected LabelData _labelData;

			[SetUp]
			public void BeforeEachTest()
			{
				_labelData = new LabelData("Id");
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Text : LabelDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_LabelData_With_Text_initialized()
			{
				const string labelText = "Text";
				var labelData = _labelData.WithText(labelText);
				Assert.AreSame(_labelData, labelData);
				labelData.ToString().Contains(labelText).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Width : LabelDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_LabelData_With_Width_initialized()
			{
				const string width = "32px";

				var labelData = _labelData.Width(width);
				Assert.AreSame(_labelData, labelData);
				labelData.ToString().Contains(width).ShouldBeTrue();
			}
		}
	}
}