//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using FluentAssert;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;

using NUnit.Framework;

using Rhino.Mocks;

namespace FluentWebControls.Tests.Extensions
{
	public class ButtonDataExtensionsTest
	{
		public abstract class ButtonDataExtensionsTestBase
		{
			protected ButtonData _buttonData;

			protected IPathUtility _pathUtility;

			[TearDown]
			public void AfterEachTest()
			{
				_pathUtility.VerifyAllExpectations();
			}

			[SetUp]
			public void BeforeEachTest()
			{
				_pathUtility = MockRepository.GenerateMock<IPathUtility>();
				_buttonData = new ButtonData(ButtonData.ButtonType.Delete, _pathUtility, "Delete");
			}
		}

		[TestFixture]
		public class When_asked_to_add_a_download_file_as_a_query_parameter : ButtonDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_ButtonData_With_action_attribute_set_to_to_the_fileName()
			{
				const string fileName = "FileName";
				const string value = "Foo.doc";
				var button = _buttonData.WithQueryParameter(fileName, value);
				Assert.AreSame(_buttonData, button);
				var action = _buttonData.ToString().ParseHtmlTag()["action"];
				action.Contains(fileName).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_CssClass : ButtonDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_ButtonData_with_CssClass_initialized()
			{
				const string cssClass = "TestClass";
				var button = _buttonData.WithCssClass(cssClass);
				Assert.AreSame(_buttonData, button);
				button.ToString().Contains(cssClass).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Id : ButtonDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_ButtonData_with_Id_initialized()
			{
				const string id = "Id";
				var button = _buttonData.WithId(id);
				Assert.AreSame(_buttonData, button);
				_buttonData.ToString().ParseHtmlTag()["id"].ShouldBeEqualTo(id);
			}

			[Test]
			public void Should_return_a_ButtonData_with_Text_initialized_if_no_Id_is_specified()
			{
				var button = _buttonData;
				Assert.AreSame(_buttonData, button);
				_buttonData.ToString().ParseHtmlTag()["id"].ShouldBeEqualTo("btn" + _buttonData.Text);
				button.ToString().Contains("btn" + _buttonData.Text).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Text : ButtonDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_ButtonData_with_Text_initialized()
			{
				const string text = "text";
				var button = _buttonData.WithText(text);
				Assert.AreSame(_buttonData, button);
				_buttonData.ToString().ParseHtmlTag()["value"].ShouldBeEqualTo(text);
				button.ToString().Contains(text).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Visibility : ButtonDataExtensionsTestBase
		{
			[Test]
			public void Should_return_ButtonData_if_visibility_is_set_to_true()
			{
				var button = _buttonData.VisibleIf(true);
				Assert.AreSame(_buttonData, button);
			}

			[Test]
			public void Should_return_empty_string_if_visibility_is_set_to_false()
			{
				var button = _buttonData.VisibleIf(false);
				button.ToString().ShouldBeEqualTo("");
			}
		}

		[TestFixture]
		public class When_asked_to_assign_action : ButtonDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_ButtonData_With_Action_appended_to_the_new_value()
			{
				const string action = "Save";
				_pathUtility.Expect(x => x.GetUrl(null)).IgnoreArguments().Return(action);
				var button = _buttonData.WithAction(action);
				Assert.AreSame(_buttonData, button);
				_buttonData.ToString().ParseHtmlTag()["action"].Contains(action).ShouldBeTrue(_buttonData.ToString());
			}
		}

		[TestFixture]
		public class When_asked_to_assign_confirmation_message : ButtonDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_ButtonData_With_OnClick_attribute_set_to_to_the_new_message()
			{
				const string message = "Test";
				var button = _buttonData.Confirm(message);
				Assert.AreSame(_buttonData, button);
				_buttonData.ToString().ParseHtmlTag()["onClick"].Contains(message).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_button_with_OnClick : ButtonDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_ButtonData_with_OnClick_appended_to_the_new_value()
			{
				const string onClickFunction = "validate()";
				var button = _buttonData.WithOnClick(onClickFunction);
				Assert.AreSame(_buttonData, button);
				_buttonData.ToString().ParseHtmlTag()["onClick"].Contains(onClickFunction).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_button_with_width : ButtonDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_ButtonData_with_OnClick_appended_to_the_new_value()
			{
				const string width = "400px";
				var button = _buttonData.Width(width);
				Assert.AreSame(_buttonData, button);
				_buttonData.ToString().ParseHtmlTag()["style"].Contains(width).ShouldBeTrue();
			}
		}
	}
}