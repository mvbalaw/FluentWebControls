using FluentAssert;

using FluentWebControls.Interfaces;
using FluentWebControls.Tools;

using NUnit.Framework;

using Rhino.Mocks;

namespace FluentWebControls.Tests
{
	public class TextAreaTest
	{
		internal class Test
		{
			public Test(string value)
			{
				Value = value;
			}

			public string Value { get; set; }
		}

		[TestFixture]
		public class When_asked_to_create_a_textarea_for_a_property_of_type_string
		{
			[SetUp]
			public void BeforeEachTest()
			{
				IoCUtility.Inject(MockRepository.GenerateStub<IBusinessObjectPropertyMetaDataFactory>());
			}

			[Test]
			public void Should_return_html_code_representing_a_textarea_with_its_value_embedded_in_it()
			{
				Test test = new Test("text");
				TextAreaData textAreaData = TextArea.For(() => test.Value);
				textAreaData.ToString().ShouldBeEqualTo("<textarea id='value' name='value' class='textbox'>text</textarea>");
			}
		}
	}
}