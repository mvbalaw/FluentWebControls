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
// ReSharper disable ConvertToConstant.Local
				int value = 10;
// ReSharper restore ConvertToConstant.Local
// ReSharper restore ConvertToConstant
				var labelData = Label.For(() => value);
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
				var labelData = Label.For(() => value);
				labelData.ToString().ShouldBeEqualTo("<label for='value' style='float:left;text-align:right'></label>");
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_label_for_a_property_of_type_string
		{
			[Test]
			public void Should_return_html_code_representing_a_label_with_its_value_embedded_in_it()
			{
// ReSharper disable ConvertToConstant.Local
				string value = "Bar";
// ReSharper restore ConvertToConstant.Local
				var labelData = Label.For(() => value);
				labelData.ToString().ShouldBeEqualTo("<label for='value' style='float:left;text-align:right'></label>");
			}
		}
	}
}