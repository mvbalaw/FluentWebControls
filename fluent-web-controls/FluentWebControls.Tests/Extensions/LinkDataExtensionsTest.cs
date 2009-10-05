using System;

using FluentAssert;

using FluentWebControls.Extensions;
using FluentWebControls.Tools;

using NUnit.Framework;

namespace FluentWebControls.Tests.Extensions
{
	public class LinkDataExtensionsTest
	{
		public abstract class LinkDataExtensionsTestBase
		{
			protected LinkData _linkData;

			[SetUp]
			public void BeforeEachTest()
			{
				_linkData = new LinkData();
			}
		}

		[TestFixture]
		public class When_asked_to_add_href : LinkDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_LinkData_with_href()
			{
				const string href = "LinkPage";
				LinkData link = _linkData.WithHref(href);
				Assert.AreSame(_linkData, link);
				TestWebControlsUtility.HtmlParser(link.ToString())["href"].ShouldBeEqualTo(href);
				_linkData.ToString().Contains(href).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_add_queryParameter : LinkDataExtensionsTestBase
		{
			private readonly TestData.Item _item = new TestData.Item(1, "ItemName");

			[Test]
			public void Should_return_a_LinkData_With_query_parameter_added_to_the_link_for_a_name_value_input_of_type_string()
			{
				LinkData link = _linkData.WithData("Name", "Value");
				Assert.AreSame(_linkData, link);
				_linkData.ToString().Contains("Name=Value").ShouldBeTrue();
			}

			[Test]
			public void Should_return_a_LinkData_With_query_parameter_added_to_the_link_for_an_expression_that_returns_int()
			{
				LinkData link = _linkData.WithData(() => _item.ItemId);
				Assert.AreSame(_linkData, link);
				_linkData.ToString().Contains(String.Format("{0}={1}", NameUtility.GetPropertyName(() => _item.ItemId), _item.ItemId)).ShouldBeTrue();
			}

			[Test]
			public void Should_return_a_LinkData_With_query_parameter_added_to_the_link_for_an_expression_that_returns_int_and_separate_value_provided()
			{
				LinkData link = _linkData.WithData(() => _item.ItemId, 10);
				Assert.AreSame(_linkData, link);
				_linkData.ToString().Contains(String.Format("{0}=10", NameUtility.GetPropertyName(() => _item.ItemId))).ShouldBeTrue();
			}

			[Test]
			public void Should_return_a_LinkData_With_query_parameter_added_to_the_link_for_an_expression_that_returns_nullable_int()
			{
				int? itemId = 5;
				LinkData link = _linkData.WithData(() => itemId);
				Assert.AreSame(_linkData, link);
				_linkData.ToString().Contains(String.Format("{0}={1}", NameUtility.GetPropertyName(() => itemId), itemId)).ShouldBeTrue();
			}

			[Test]
			public void Should_return_a_LinkData_With_query_parameter_added_to_the_link_for_an_expression_that_returns_nullable_int_and_separate_value_provided()
			{
				int? itemId = 5;
				int? newValue = 10;
				LinkData link = _linkData.WithData(() => itemId, newValue);
				Assert.AreSame(_linkData, link);
				_linkData.ToString().Contains(String.Format("{0}=10", NameUtility.GetPropertyName(() => itemId))).ShouldBeTrue();
			}

			[Test]
			public void Should_return_a_LinkData_With_query_parameter_added_to_the_link_for_an_expression_that_returns_string()
			{
				LinkData link = _linkData.WithData(() => _item.ItemName);
				Assert.AreSame(_linkData, link);
				_linkData.ToString().Contains(String.Format("{0}={1}", NameUtility.GetPropertyName(() => _item.ItemName), _item.ItemName)).ShouldBeTrue();
			}

			[Test]
			public void Should_return_a_LinkData_With_query_parameter_added_to_the_link_for_an_expression_that_returns_string_and_separate_value_provided()
			{
				LinkData link = _linkData.WithData(() => _item.ItemName, "Value");
				Assert.AreSame(_linkData, link);
				_linkData.ToString().Contains(String.Format("{0}=Value", NameUtility.GetPropertyName(() => _item.ItemName))).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_add_rel : LinkDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_LinkData_with_rel()
			{
				const string rel = "external";
				LinkData link = _linkData.WithRel(rel);
				Console.WriteLine(link.ToString());
				Assert.AreSame(_linkData, link);
				TestWebControlsUtility.HtmlParser(link.ToString())["rel"].ShouldBeEqualTo(rel);
				_linkData.ToString().Contains(rel).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_CssClass : LinkDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_LinkData_With_CssClass_initialized()
			{
				const string cssClass = "Link";
				LinkData link = _linkData.CssClass(cssClass);
				Assert.AreSame(_linkData, link);
				TestWebControlsUtility.HtmlParser(link.ToString())["class"].ShouldBeEqualTo(cssClass);
				_linkData.ToString().Contains(cssClass).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_MouseOverText : LinkDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_LinkData_With_MouseOverText_initialized()
			{
				const string text = "Text";
				LinkData link = _linkData.WithMouseOverText(text);
				Assert.AreSame(_linkData, link);
				TestWebControlsUtility.HtmlParser(link.ToString())["title"].ShouldBeEqualTo(text);
				_linkData.ToString().Contains(text).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Text : LinkDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_LinkData_With_Text_initialized()
			{
				const string text = "Text";
				LinkData link = _linkData.WithLinkText(text);
				Assert.AreSame(_linkData, link);
				_linkData.ToString().Contains(text).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_Disable : LinkDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_LinkData_that_is_disabled()
			{
				LinkData link = _linkData.DisabledIf(true);
				Assert.AreSame(_linkData, link);
				_linkData.ToString().Contains("disabled").ShouldBeTrue();
			}
		}
	}
}