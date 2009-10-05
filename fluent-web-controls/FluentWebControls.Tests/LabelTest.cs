using FluentAssert;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class LabelTest
	{
		[TestFixture]
		public class When_asked_to_create_a_label_for_a_property_of_type_int
		{
			[Test]
			public void Should_return_html_code_representing_a_label_with_its_value_embedded_in_it()
			{
// ReSharper disable ConvertToConstant
				int value = 10;
// ReSharper restore ConvertToConstant
				LabelData labelData = Label.For(() => value);
				labelData.ToString().ShouldBeEqualTo("<label for='value' style='float:left;text-align:right'></label>");
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_label_for_a_property_of_type_nullable_int
		{
			[Test]
			public void Should_return_html_code_representing_a_label_with_its_value_embedded_in_it()
			{
				int? value = 10;
				LabelData labelData = Label.For(() => value);
				labelData.ToString().ShouldBeEqualTo("<label for='value' style='float:left;text-align:right'></label>");
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_label_for_a_property_of_type_string
		{
			[Test]
			public void Should_return_html_code_representing_a_label_with_its_value_embedded_in_it()
			{
				string value = "Bar";
				LabelData labelData = Label.For(() => value);
				labelData.ToString().ShouldBeEqualTo("<label for='value' style='float:left;text-align:right'></label>");
			}
		}
	}
}