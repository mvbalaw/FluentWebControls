using FluentAssert;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class HiddenTest
	{
		[TestFixture]
		public class When_asked_to_create_a_hidden_field_for_a_property_of_type_int
		{
			[Test]
			public void Should_return_html_code_representing_a_hidden_field_with_its_value_embedded_in_it()
			{
// ReSharper disable ConvertToConstant
				int value = 10;
// ReSharper restore ConvertToConstant
				HiddenData hiddenData = Hidden.For(() => value);
				hiddenData.ToString().ShouldBeEqualTo("<input type='hidden' id='value' name='value' value='" + value + "'/>");
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_hidden_field_for_a_property_of_type_nullable_int
		{
			[Test]
			public void Should_return_html_code_representing_a_hidden_field_with_its_value_embedded_in_it()
			{
				int? value = 10;
				HiddenData hiddenData = Hidden.For(() => value);
				hiddenData.ToString().ShouldBeEqualTo("<input type='hidden' id='value' name='value' value='" + value + "'/>");
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_hidden_field_for_a_property_of_type_string
		{
			[Test]
			public void Should_return_html_code_representing_a_hidden_field_with_its_value_embedded_in_it()
			{
// ReSharper disable ConvertToConstant
				string value = "Bar";
// ReSharper restore ConvertToConstant
				HiddenData hiddenData = Hidden.For(() => value);
				hiddenData.ToString().ShouldBeEqualTo("<input type='hidden' id='value' name='value' value='" + value + "'/>");
			}
		}
	}
}