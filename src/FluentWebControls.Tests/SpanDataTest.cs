using System.Linq;

using FluentAssert;

using FluentWebControls.Extensions;

using LinqToHtml;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class SpanDataTest
	{
		[TestFixture]
		public class When_asked_to_create_a_span_field
		{
			private string _id;
			private string _label;
			private HTMLTag _result;
			private string _value;

			[SetUp]
			public void BeforeEachTest()
			{
				_id = "theId";
				_label = null;
				_result = null;
				_value = null;
			}

			[Test]
			public void Given_a_label()
			{
				Test.Verify(
					with_a_label,
					when_asked_to_create_a_span,
					should_include_a_label_with_the_label_text,
					should_set_the_label_for_attribute_to_the_id_of_the_textarea
					);
			}

			[Test]
			public void Given_a_non_empty_value_without_characters_that_need_to_be_escaped()
			{
				Test.Verify(
					with_a_non_empty_value_without_characters_that_need_to_be_escaped,
					when_asked_to_create_a_span,
					should_set_the_content_to_the_value
					);
			}

			[Test]
			public void Given_a_null_value()
			{
				Test.Verify(
					with_a_null_value,
					when_asked_to_create_a_span,
					should_set_the_content_to_empty_string
					);
			}

			[Test]
			public void Given_a_value_containing_characters_that_need_to_be_escaped()
			{
				Test.Verify(
					with_a_value_containing_characters_that_need_to_be_escaped,
					when_asked_to_create_a_span,
					should_set_the_content_to_the_escaped_value
					);
			}

			private HTMLTag Label
			{
				get { return _result.ChildTags.FirstOrDefault(x => x.Type == "label"); }
			}

			private HTMLTag Tag
			{
				get { return _result.ChildTags.Where(x => x.Type == "span").First(); }
			}

			private void should_include_a_label_with_the_label_text()
			{
				var label = Label;
				label.ShouldNotBeNull();
				label.Content.ShouldNotBeNull();
				label.Content.ShouldBeEqualTo(_label);
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

			private void when_asked_to_create_a_span()
			{
				var span = new SpanData(_value)
					.WithLabel(_label)
					.WithId(_id);
				string resultHtml = span.ToString();
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

			private void with_a_value_containing_characters_that_need_to_be_escaped()
			{
				_value = "<&>";
			}
		}
	}
}