//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using System;

using FluentAssert;

using FluentWebControls.Extensions;
using FluentWebControls.Tests.Extensions;
using MvbaCore.Interfaces;
using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class TestPathUtility : IPathUtility
	{
		public string GetUrl(string virtualDirectory)
		{
			return $"/someapp/{virtualDirectory}";
		}
	}

	public class LinkTest
	{
		public class TestController
		{
			public object Action(int id, string name)
			{
				return 0;
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_link
		{
			private IPathUtility _pathUtility;

			[SetUp]
			public void BeforeEachTest()
			{
				_pathUtility = new TestPathUtility();
				Configuration.PathUtility = _pathUtility;
			}

			[Test]
			public void Should_return_html_code_representing_a_link()
			{
				var linkData = Link.To("AdminTest", ".mvc", "");
				var expected = _pathUtility.GetUrl("AdminTest.mvc/");
				var s = linkData.ToString();
				Console.WriteLine(s);
				s.ParseHtmlTag()["href"].ShouldBeEqualTo(expected);
			}

			[Test]
			public void Should_return_html_code_representing_a_link_with_the_full_url_if_PathUtility_is_configured()
			{
				var linkData = Link.To("AdminTest", ".mvc", "Test");
				var expected = _pathUtility.GetUrl("AdminTest.mvc/Test");
				linkData.ToString().ParseHtmlTag()["href"].ShouldBeEqualTo(expected);
			}

			[Test]
			public void Should_return_html_code_representing_a_link_with_the_partial_url_if_PathUtility_is_not_configured()
			{
				Configuration.PathUtility = null;
				var linkData = Link.To("AdminTest", ".mvc", "Test");
				const string expected = "AdminTest.mvc/Test";
				linkData.ToString().ParseHtmlTag()["href"].ShouldBeEqualTo(expected);
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_link_using_a_lambda
		{
			private IPathUtility _pathUtility;

			[SetUp]
			public void BeforeEachTest()
			{
				_pathUtility = new TestPathUtility();
				Configuration.PathUtility = _pathUtility;
			}

			[Test]
			public void Should_return_html_code_representing_a_link_when_argument_values_are_properties_of_an_object()
			{
				var test = new Test
				           {
					           Id = 4,
					           Name = "Name"
				           };
				var linkData = Link.To((TestController controller) => controller.Action(test.Id, test.Name));
				var expected = _pathUtility.GetUrl("Test/Action/4/Name");
				linkData.ToString().ParseHtmlTag()["href"].ShouldBeEqualTo(expected);
			}

			[Test]
			public void Should_return_html_code_representing_a_link_when_argument_values_are_variables()
			{
				const int id = 4;
				const string name = "Name";
				var linkData = Link.To((TestController controller) => controller.Action(id, name));
				var expected = _pathUtility.GetUrl("Test/Action/4/Name");
				linkData.ToString().ParseHtmlTag()["href"].ShouldBeEqualTo(expected);
			}

			[Test]
			public void Should_return_html_code_representing_a_link_when_arguments_are_constants()
			{
				const int id = 4;
				const string name = "Name";
				var linkData = Link.To((TestController controller) => controller.Action(id, name));
				var expected = _pathUtility.GetUrl("Test/Action/4/Name");
				linkData.ToString().ParseHtmlTag()["href"].ShouldBeEqualTo(expected);
			}

			[Test]
			public void Should_set_the_id_to_the_data_from_the_lambda()
			{
				const int id = 4;
				const string name = "Name";
				var linkData = Link.To((TestController controller) => controller.Action(id, name))
					.WithControllerExtension(".mvc");
				const string expected = "Test_Action_4_Name";
				linkData.ToString().ParseHtmlTag()["id"].ShouldBeEqualTo(expected);
			}

			[Test]
			public void Should_use_the_controller_extension_if_given()
			{
				const int id = 4;
				const string name = "Name";
				var linkData = Link.To((TestController controller) => controller.Action(id, name))
					.WithControllerExtension(".mvc");
				var expected = _pathUtility.GetUrl("Test.mvc/Action/4/Name");
				linkData.ToString().ParseHtmlTag()["href"].ShouldBeEqualTo(expected);
			}

			public class Test
			{
				public int Id { get; set; }
				public string Name { get; set; }
			}
		}
	}
}