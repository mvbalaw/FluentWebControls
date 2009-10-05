using System;

using FluentAssert;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class WebControlBaseTest
	{
		public class WebControlTest : WebControlBase
		{
		}

		[TestFixture]
		public class When_asked_to_CreateQuotedAttribute
		{
			private readonly WebControlTest _webControlTest = new WebControlTest();

			[Test]
			public void Should_create_attribute_in_the_name_value_format()
			{
				_webControlTest.CreateQuotedAttribute("name", "value").ShouldBeEqualTo(" name='value'");
				_webControlTest.CreateQuotedAttribute("name", 1).ShouldBeEqualTo(" name='1'");
				_webControlTest.CreateQuotedAttribute("name", null).ShouldBeEqualTo(" name=''");
			}
		}

		[TestFixture]
		public class When_asked_to_escape_for_html
		{
			private const string Ampersand = "&amp;";
			private const string Greater = "&gt;";
			private const string Lesser = "&lt;";
			private const string TestString = "TestString";

			private readonly WebControlTest _webControlTest = new WebControlTest();

			[Test]
			public void Should_remove_characters_leading_to_Cross_Site_Scripting()
			{
				_webControlTest.EscapeForHtml("<" + TestString + ">&").ShouldBeEqualTo(Lesser + TestString + Greater + Ampersand);
			}
		}

		[TestFixture]
		public class When_asked_to_escape_for_tag_attribute
		{
			private const string Lesser = "&lt;";
			private const string Quote = "&quot;";

			private readonly WebControlTest _webControlTest = new WebControlTest();

			[Test]
			public void Should_create_attribute_in_the_name_value_format_with_special_characters_removed()
			{
				_webControlTest.CreateQuotedAttribute("name", "<value\"").ShouldBeEqualTo(String.Format(" name='{0}value{1}'", Lesser, Quote));
				_webControlTest.CreateQuotedAttribute("name", 1).ShouldBeEqualTo(" name='1'");
				_webControlTest.CreateQuotedAttribute("name", null).ShouldBeEqualTo(" name=''");
			}
		}

		[TestFixture]
		public class When_asked_to_escape_for_url_string
		{
			private const string Quote = "%22";
			private const string Space = "+";
			private const string TestString = "TestString";

			private readonly WebControlTest _webControlTest = new WebControlTest();

			[Test]
			public void Should_remove_characters_leading_to_Cross_Site_Scripting()
			{
				_webControlTest.EscapeForUrl(TestString + "\"   ").ShouldBeEqualTo(TestString + Quote + Space + Space + Space);
			}
		}
	}
}