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
				var test = new Test("text");
				var textAreaData = TextArea.For(test, x => x.Value, x => x.Value);
				textAreaData.ToString().ShouldBeEqualTo("<textarea id='value' name='value' class='textbox'>text</textarea>");
			}
		}
	}
}