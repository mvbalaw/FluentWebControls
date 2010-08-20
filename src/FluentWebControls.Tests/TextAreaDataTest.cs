using System;
using System.Linq;

using FluentAssert;

using FluentWebControls.Extensions;
using FluentWebControls.Tests.Extensions;

using LinqToHtml;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class TextAreaDataTest
	{
		[TestFixture]
		public class When_asked_to_create_a_textarea_for_a_property
		{
			private string _id;
			private bool _isRequired;
			private string _label;
			private int? _maxLength;
			private int? _maxValue;
			private int? _minLength;
			private int? _minValue;
			private HTMLDocument _result;
			private Type _type;
			private string _value;

			[SetUp]
			public void BeforeEachTest()
			{
				_isRequired = false;
				_minLength = null;
				_maxLength = null;
				_minValue = null;
				_maxValue = null;
				_type = typeof(string);
				_id = "theId";
				_value = "value";
			}

			[Test]
			public void Given_a_label()
			{
				Test.Verify(
					with_a_label,
					when_asked_to_create_a_textarea_for_a_property,
					should_include_a_label_with_the_label_text,
					should_set_the_label_for_attribute_to_the_id_of_the_textarea
					);
			}

			[Test]
			public void Given_a_non_empty_value_without_characters_that_need_to_be_escaped()
			{
				Test.Verify(
					with_a_non_empty_value_without_characters_that_need_to_be_escaped,
					when_asked_to_create_a_textarea_for_a_property,
					should_set_the_content_to_the_value
					);
			}

			[Test]
			public void Given_a_null_value()
			{
				Test.Verify(
					with_a_null_value,
					when_asked_to_create_a_textarea_for_a_property,
					should_set_the_content_to_empty_string
					);
			}

			[Test]
			public void Given_a_value_containing_characters_that_need_to_be_escaped()
			{
				Test.Verify(
					with_a_value_containing_characters_that_need_to_be_escaped,
					when_asked_to_create_a_textarea_for_a_property,
					should_set_the_content_to_the_escaped_value
					);
			}

			[Test]
			public void Given_the_Property_does_not_have_a_maximum_length()
			{
				Test.Verify(
					with_a_property_that_does_not_have_a_maximum_length,
					when_asked_to_create_a_textarea_for_a_property,
					should_not_mark_the_textarea_with_the_maximum_length
					);
			}

			[Test]
			public void Given_the_Property_does_not_have_a_minimum_length()
			{
				Test.Verify(
					with_a_property_that_does_not_have_a_minimum_length,
					when_asked_to_create_a_textarea_for_a_property,
					should_not_mark_the_textarea_with_the_minimum_length
					);
			}

			[Test]
			public void Given_the_Property_has_a_maximum_length()
			{
				Test.Verify(
					with_a_property_that_has_a_maximum_length,
					when_asked_to_create_a_textarea_for_a_property,
					should_mark_the_textarea_with_the_maximum_length
					);
			}

			[Test]
			public void Given_the_Property_has_a_minimum_length()
			{
				Test.Verify(
					with_a_property_that_has_a_minimum_length,
					when_asked_to_create_a_textarea_for_a_property,
					should_mark_the_textarea_with_the_minimum_length
					);
			}

			[Test]
			public void Given_the_Property_is_not_required()
			{
				Test.Verify(
					with_a_property_that_is_not_required,
					when_asked_to_create_a_textarea_for_a_property,
					should_not_mark_the_textarea_as_required,
					should_not_add_the_visual_required_indicator
					);
			}

			[Test]
			public void Given_the_Property_is_required()
			{
				Test.Verify(
					with_a_property_that_is_required,
					when_asked_to_create_a_textarea_for_a_property,
					should_mark_the_textarea_as_required,
					should_add_the_visual_required_indicator
					);
			}

			protected string Id
			{
				get { return _id.ToCamelCase(); }
			}

			private HTMLTag Label
			{
				get { return _result.ChildTags.FirstOrDefault(x => x.Type == "label"); }
			}
			private HTMLTag RequiredIndicator
			{
				get { return _result.ChildTags.FirstOrDefault(x => x.Type == "em"); }
			}

			private HTMLTag Tag
			{
				get { return _result.ChildTags.WithAttributeNamed("class").First(); }
			}

			private void should_add_the_visual_required_indicator()
			{
				var requiredIndicator = RequiredIndicator;
				requiredIndicator.ShouldNotBeNull();
				requiredIndicator.Content.ShouldBeEqualTo("*");
			}

			private void should_include_a_label_with_the_label_text()
			{
				var label = Label;
				label.ShouldNotBeNull();
				label.Content.ShouldNotBeNull();
				label.Content.ShouldBeEqualTo(_label);
			}

			private void should_mark_the_textarea_as_required()
			{
				var @class = Tag.Attributes.FirstOrDefault(x => x.Name == "class");
				@class.ShouldNotBeNull();
				@class.Value.ShouldBeEqualTo("required textbox");
			}

			private void should_mark_the_textarea_with_the_maximum_length()
			{
				var maxLength = Tag.Attributes.FirstOrDefault(x => x.Name == "maxlength");
				maxLength.ShouldNotBeNull();
				maxLength.Value.ShouldBeEqualTo(_maxLength.ToString());
			}

			private void should_mark_the_textarea_with_the_minimum_length()
			{
				var minLength = Tag.Attributes.FirstOrDefault(x => x.Name == "minlength");
				minLength.ShouldNotBeNull();
				minLength.Value.ShouldBeEqualTo(_minLength.ToString());
			}

			private void should_not_add_the_visual_required_indicator()
			{
				var requiredIndicator = RequiredIndicator;
				requiredIndicator.ShouldBeNull();
			}

			private void should_not_mark_the_textarea_as_required()
			{
				var @class = Tag.Attributes.FirstOrDefault(x => x.Name == "class");
				@class.ShouldNotBeNull();
				@class.Value.ShouldBeEqualTo("textbox");
			}

			private void should_not_mark_the_textarea_with_the_maximum_length()
			{
				var maxLength = Tag.Attributes.FirstOrDefault(x => x.Name == "maxlength");
				maxLength.ShouldBeNull();
			}

			private void should_not_mark_the_textarea_with_the_minimum_length()
			{
				var minLength = Tag.Attributes.FirstOrDefault(x => x.Name == "minlength");
				minLength.ShouldBeNull();
			}

			private void should_set_the_content_to_empty_string()
			{
				string content = Tag.Content;
				content.ShouldBeEqualTo("");
			}

			private void should_set_the_content_to_the_escaped_value()
			{
				string content = Tag.RawContent;
				content.ShouldBeEqualTo(_value.EscapeForHtml());
			}

			private void should_set_the_content_to_the_value()
			{
				string content = Tag.Content;
				content.ShouldBeEqualTo(_value);
			}

			private void should_set_the_label_for_attribute_to_the_id_of_the_textarea()
			{
				var label = Label;
				var @for = label.Attributes.FirstOrDefault(x => x.Name == "for");
				@for.ShouldNotBeNull();
				var tag = Tag;
				var id = tag.Attributes.FirstOrDefault(x => x.Name == "id");
				@for.Value.ShouldBeEqualTo(id.Value);
			}

			private void when_asked_to_create_a_textarea_for_a_property()
			{
				var propertyMetaData = PropertyMetaDataMocker.CreateStub(Id, _isRequired, _minLength, _maxLength, _minValue, _maxValue, _type);
				var textArea = new TextAreaData(_value)
					.WithValidationFrom(propertyMetaData)
					.WithLabel(_label)
					.WithId(Id);
				string resultHtml = textArea.ToString();
				_result = HTMLParser.Parse("<all>" + resultHtml + "</all>");
			}

			private void with_a_label()
			{
				_label = "Name:";
			}

			private void with_a_non_empty_value_without_characters_that_need_to_be_escaped()
			{
				_value = "The quick brown fox";
			}

			private void with_a_null_value()
			{
				_value = null;
			}

			private void with_a_property_that_does_not_have_a_maximum_length()
			{
				_maxLength = null;
			}

			private void with_a_property_that_does_not_have_a_minimum_length()
			{
				_minLength = null;
			}

			private void with_a_property_that_has_a_maximum_length()
			{
				_maxLength = 10;
			}

			private void with_a_property_that_has_a_minimum_length()
			{
				_minLength = 6;
			}

			private void with_a_property_that_is_not_required()
			{
				_isRequired = false;
			}

			private void with_a_property_that_is_required()
			{
				_isRequired = true;
			}

			private void with_a_value_containing_characters_that_need_to_be_escaped()
			{
				_value = "<&>";
			}
		}
	}
}