using FluentAssert;

using FluentWebControls.Extensions;

using MvbaCore;

using NUnit.Framework;

using StringExtensions = System.StringExtensions;

namespace FluentWebControls.Tests
{
	public class RadioButtonDataTest
	{
		[TestFixture]
		public class When_asked_to_create_a_radio_that_is_checked
		{
			private const string HtmlText = "<input type='radio' id='value' name='value' checked='checked' value='true'/>";

			[Test]
			public void Should_return_HTML_code_representing_a_radio_field_with_its_value_embedded_in_it()
			{
				bool value = true;
				var checkBoxData = new RadioButtonData(value)
					.WithId(StringExtensions.ToCamelCase(Reflection.GetPropertyName(() => value)));
				string actual = checkBoxData.ToString();
				actual.ShouldBeEqualTo(HtmlText, actual);
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_radio_that_is_not_checked
		{
			private const string HtmlText = "<input type='radio' id='value' name='value' value='true'/>";

			[Test]
			public void Should_return_HTML_code_representing_a_radio_field_with_its_value_embedded_in_it()
			{
				bool value = false;
				var checkBoxData = new RadioButtonData(value)
					.WithId(StringExtensions.ToCamelCase(Reflection.GetPropertyName(() => value)));
				string actual = checkBoxData.ToString();
				actual.ShouldBeEqualTo(HtmlText, actual);
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_radio_with_label_in_the_left
		{
			private const string HtmlText = "<label for='value' style='float:left;text-align:right'>Label</label><input type='radio' id='value' name='value' value='true'/>";

			[Test]
			public void Should_return_HTML_code_representing_a_radio_field_with_its_value_embedded_in_it()
			{
				bool value = false;
				var checkBoxData = new RadioButtonData(value)
					.WithId(StringExtensions.ToCamelCase(Reflection.GetPropertyName(() => value)));
				SetLabel(checkBoxData);
				string actual = checkBoxData.ToString();
				actual.ShouldBeEqualTo(HtmlText, actual);
			}

			private static void SetLabel(RadioButtonData checkBoxData)
			{
				var label = new LabelData
					{
						Text = "Label"
					};
				checkBoxData.WithLabel(label);
				checkBoxData.WithLabelAlignedLeft(label);
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_radio_with_label_in_the_right
		{
			private const string HtmlText = "<label style='float:left;text-align:right'>&nbsp;</label><input type='radio' id='value' name='value' value='true'/><label for='value'>Label</label>";

			[Test]
			public void Should_return_HTML_code_representing_a_radio_field_with_its_value_embedded_in_it()
			{
				bool value = false;
				var checkBoxData = new RadioButtonData(value)
					.WithId(StringExtensions.ToCamelCase(Reflection.GetPropertyName(() => value)));
				SetLabel(checkBoxData);
				string actual = checkBoxData.ToString();
				actual.ShouldBeEqualTo(HtmlText, actual);
			}

			private static void SetLabel(RadioButtonData checkBoxData)
			{
				var label = new LabelData
					{
						Text = "Label:"
					};
				checkBoxData.WithLabel(label);
			}
		}
	}
}