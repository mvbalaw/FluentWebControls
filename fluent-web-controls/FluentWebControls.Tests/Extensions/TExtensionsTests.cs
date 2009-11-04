using System;

using FluentAssert;

using FluentWebControls.Extensions;

using NUnit.Framework;

namespace FluentWebControls.Tests.Extensions
{
	public class TExtensionsTests
	{
		[TestFixture]
		public class When_asked_to_CreateQuotedAttribute
		{
			[Test]
			public void Should_produce_correct_output_given_a_null_string_value()
			{
				const string value = null;
				var result = value.CreateQuotedAttribute("name");
				result.ShouldBeEqualTo(" name=''");
			}

			[Test]
			public void Should_produce_correct_output_given_a_string_value()
			{
				const string value = "value";
				var result = value.CreateQuotedAttribute("name");
				result.ShouldBeEqualTo(" name='value'");
			}

			[Test]
			public void Should_produce_correct_output_given_an_int_value()
			{
				const int value = 1;
				var result = value.CreateQuotedAttribute("name");
				result.ShouldBeEqualTo(" name='1'");
			}

			[Test]
			public void Should_produce_correct_output_given_an_null_nullable_int_value()
			{
				int? value = null;
				var result = value.CreateQuotedAttribute("name");
				result.ShouldBeEqualTo(" name=''");
			}
		}

		[TestFixture]
		public class When_asked_to_escape_for_tag_attribute
		{
			private const string Lesser = "&lt;";
			private const string Quote = "&quot;";

			[Test]
			public void Should_produce_correct_output_given_a_null_nullable_integer_value()
			{
				int? value = null;
				var result = value.CreateQuotedAttribute("name");
				result.ShouldBeEqualTo(" name=''");
			}

			[Test]
			public void Should_produce_correct_output_given_a_null_string_value()
			{
				const string value = null;
				var result = value.CreateQuotedAttribute("name");
				result.ShouldBeEqualTo(" name=''");
			}

			[Test]
			public void Should_produce_correct_output_given_a_string_value()
			{
				const string value = "<value\"";
				var result = value.CreateQuotedAttribute("name");
				result.ShouldBeEqualTo(String.Format(" name='{0}value{1}'", Lesser, Quote));
			}

			[Test]
			public void Should_produce_correct_output_given_an_integer_value()
			{
				const int value = 1;
				var result = value.CreateQuotedAttribute("name");
				result.ShouldBeEqualTo(" name='1'");
			}
		}
	}
}