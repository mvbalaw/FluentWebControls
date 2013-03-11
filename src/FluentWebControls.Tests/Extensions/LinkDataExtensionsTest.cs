using System;

using FluentAssert;

using FluentWebControls.Extensions;

using MvbaCore;

using NUnit.Framework;

namespace FluentWebControls.Tests.Extensions
{
	public class LinkDataExtensionsTest
	{
		public abstract class LinkDataExtensionsTestBase
		{
			protected LinkData LinkData;

			[SetUp]
			public void BeforeEachTest()
			{
				LinkData = new LinkData();
			}
		}

		[TestFixture]
		public class When_asked_to_add_href : LinkDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_LinkData_with_href()
			{
				const string href = "LinkPage";
				var link = LinkData.WithUrl(href);
				Assert.AreSame(LinkData, link);
				link.ToString().ParseHtmlTag()["href"].ShouldBeEqualTo(href);
				LinkData.ToString().Contains(href).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_add_queryParameter : LinkDataExtensionsTestBase
		{
			private readonly TestData.Item _item = new TestData.Item(1, "ItemName");

			[Test]
			public void Should_return_a_LinkData_With_query_parameter_added_to_the_link_for_a_name_value_input_of_type_string()
			{
				var link = LinkData.WithQueryStringData("Name", "Value");
				Assert.AreSame(LinkData, link);
				LinkData.ToString().Contains("Name=Value").ShouldBeTrue();
			}

			[Test]
			public void Should_return_a_LinkData_With_query_parameter_added_to_the_link_for_an_expression_that_returns_int()
			{
				var link = LinkData.WithQueryStringData(() => _item.ItemId);
				Assert.AreSame(LinkData, link);
				LinkData.ToString().Contains(String.Format("{0}={1}", Reflection.GetPropertyName(() => _item.ItemId), _item.ItemId)).ShouldBeTrue();
			}

			[Test]
			public void Should_return_a_LinkData_With_query_parameter_added_to_the_link_for_an_expression_that_returns_int_and_separate_value_provided()
			{
				var link = LinkData.WithQueryStringData(() => _item.ItemId, 10);
				Assert.AreSame(LinkData, link);
				LinkData.ToString().Contains(String.Format("{0}=10", Reflection.GetPropertyName(() => _item.ItemId))).ShouldBeTrue();
			}

			[Test]
			public void Should_return_a_LinkData_With_query_parameter_added_to_the_link_for_an_expression_that_returns_nullable_int()
			{
				int? itemId = 5;
				var link = LinkData.WithQueryStringData(() => itemId);
				Assert.AreSame(LinkData, link);
				LinkData.ToString().Contains(String.Format("{0}={1}", Reflection.GetPropertyName(() => itemId), itemId)).ShouldBeTrue();
			}

			[Test]
			public void Should_return_a_LinkData_With_query_parameter_added_to_the_link_for_an_expression_that_returns_nullable_int_and_separate_value_provided()
			{
				int? itemId = 5;
				int? newValue = 10;
				var link = LinkData.WithQueryStringData(() => itemId, newValue);
				Assert.AreSame(LinkData, link);
				LinkData.ToString().Contains(String.Format("{0}=10", Reflection.GetPropertyName(() => itemId))).ShouldBeTrue();
			}

			[Test]
			public void Should_return_a_LinkData_With_query_parameter_added_to_the_link_for_an_expression_that_returns_string()
			{
				var link = LinkData.WithQueryStringData(() => _item.ItemName);
				Assert.AreSame(LinkData, link);
				LinkData.ToString().Contains(String.Format("{0}={1}", Reflection.GetPropertyName(() => _item.ItemName), _item.ItemName)).ShouldBeTrue();
			}

			[Test]
			public void Should_return_a_LinkData_With_query_parameter_added_to_the_link_for_an_expression_that_returns_string_and_separate_value_provided()
			{
				var link = LinkData.WithQueryStringData(() => _item.ItemName, "Value");
				Assert.AreSame(LinkData, link);
				LinkData.ToString().Contains(String.Format("{0}=Value", Reflection.GetPropertyName(() => _item.ItemName))).ShouldBeTrue();
			}

			[Test]
			public void Should_return_a_LinkData_With_query_parameter_added_to_the_link_for_an_object_that_contains_the_specified_property()
			{
				var link = LinkData.WithQueryStringData(_item, item => item.ItemId, item => item.ItemId.ToString());
				Assert.AreSame(LinkData, link);
				LinkData.ToString().Contains(String.Format("{0}={1}", Reflection.GetPropertyName(() => _item.ItemId), _item.ItemId)).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_add_rel : LinkDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_LinkData_with_rel()
			{
				const string rel = "external";
				var link = LinkData.WithRel(rel);
				Assert.AreSame(LinkData, link);
				link.ToString().ParseHtmlTag()["rel"].ShouldBeEqualTo(rel);
				LinkData.ToString().Contains(rel).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_CssClass : LinkDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_LinkData_With_CssClass_initialized()
			{
				const string cssClass = "Link";
				var link = LinkData.WithCssClass(cssClass);
				Assert.AreSame(LinkData, link);
				link.ToString().ParseHtmlTag()["class"].ShouldBeEqualTo(cssClass);
				LinkData.ToString().Contains(cssClass).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_MouseOverText : LinkDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_LinkData_With_MouseOverText_initialized()
			{
				const string text = "Text";
				var link = LinkData.WithMouseOverText(text);
				Assert.AreSame(LinkData, link);
				link.ToString().ParseHtmlTag()["title"].ShouldBeEqualTo(text);
				LinkData.ToString().Contains(text).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Text : LinkDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_LinkData_With_Text_initialized()
			{
				const string text = "Text";
				var link = LinkData.WithLinkText(text);
				Assert.AreSame(LinkData, link);
				LinkData.ToString().Contains(text).ShouldBeTrue();
			}
		}
		
		[TestFixture]
		public class When_asked_to_assign_Image_Url : LinkDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_LinkData_With_Image_Url_initialized()
			{
				const string imageUrl = "ImageUrl";
				const string alt = "alt";
				const string text = "Text";
				var link = LinkData.WithLinkText(text).WithLinkImageUrl(imageUrl, alt);
				Assert.AreSame(LinkData, link);
				LinkData.ToString().Contains(imageUrl).ShouldBeTrue();
				LinkData.ToString().Contains("<img src='" + imageUrl + "' alt='" + alt + "'/>").ShouldBeTrue();
			}
			
			[Test]
			public void Should_return_a_LinkData_Without_Text_initialized()
			{
				const string imageUrl = "ImageUrl";
				const string alt = "alt";
				const string text = "Text";
				var link = LinkData.WithLinkImageUrl(imageUrl, alt);
				Assert.AreSame(LinkData, link);
				LinkData.ToString().Contains(text).ShouldBeFalse();
			}
		}

		[TestFixture]
		public class When_asked_to_Disable : LinkDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_LinkData_that_is_disabled()
			{
				var link = LinkData.DisabledIf(true);
				Assert.AreSame(LinkData, link);
				LinkData.ToString().Contains("disabled").ShouldBeTrue();
			}
		}
	}
}