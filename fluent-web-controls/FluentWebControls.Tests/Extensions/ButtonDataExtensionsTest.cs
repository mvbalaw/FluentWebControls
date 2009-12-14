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
				ButtonData button = _buttonData.WithQueryParameter(fileName, value);
				Assert.AreSame(_buttonData, button);
				string dictionary = TestWebControlsUtility.HtmlParser(_buttonData.ToString())["action"];
				dictionary.Contains(fileName).ShouldBeTrue();
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
				ButtonData button = _buttonData.WithAction(action);
				Assert.AreSame(_buttonData, button);
				TestWebControlsUtility.HtmlParser(_buttonData.ToString())["action"].Contains(action).ShouldBeTrue(_buttonData.ToString());
			}
		}

		[TestFixture]
		public class When_asked_to_assign_confirmation_message : ButtonDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_ButtonData_With_OnClick_attribute_set_to_to_the_new_message()
			{
				const string message = "Test";
				ButtonData button = _buttonData.Confirm(message);
				Assert.AreSame(_buttonData, button);
				TestWebControlsUtility.HtmlParser(_buttonData.ToString())["onClick"].Contains(message).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Id : ButtonDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_ButtonData_With_Id_initialized()
			{
				const string id = "Id";
				ButtonData button = _buttonData.WithId(id);
				Assert.AreSame(_buttonData, button);
				TestWebControlsUtility.HtmlParser(_buttonData.ToString())["id"].ShouldBeEqualTo("btn" + id);
				button.ToString().Contains("btn" + id).ShouldBeTrue();
			}

			[Test]
			public void Should_return_a_ButtonData_With_Text_initialized_if_no_Id_is_specified()
			{
				ButtonData button = _buttonData;
				Assert.AreSame(_buttonData, button);
				TestWebControlsUtility.HtmlParser(_buttonData.ToString())["id"].ShouldBeEqualTo("btn" + _buttonData.Text);
				button.ToString().Contains("btn" + _buttonData.Text).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Text : ButtonDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_ButtonData_With_Text_initialized()
			{
				const string text = "text";
				ButtonData button = _buttonData.WithText(text);
				Assert.AreSame(_buttonData, button);
				TestWebControlsUtility.HtmlParser(_buttonData.ToString())["value"].ShouldBeEqualTo(text);
				button.ToString().Contains(text).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Visibility : ButtonDataExtensionsTestBase
		{
			[Test]
			public void Should_return_ButtonData_if_visibility_is_set_to_true()
			{
				ButtonData button = _buttonData.VisibleIf(true);
				Assert.AreSame(_buttonData, button);
			}

			[Test]
			public void Should_return_empty_string_if_visibility_is_set_to_false()
			{
				ButtonData button = _buttonData.VisibleIf(false);
				button.ToString().ShouldBeEqualTo("");
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_button_with_OnClick : ButtonDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_ButtonData_With_OnClick_appended_to_the_new_value()
			{
				const string onClickFunction = "validate()";
				ButtonData button = _buttonData.WithOnClick(onClickFunction);
				Assert.AreSame(_buttonData, button);
				TestWebControlsUtility.HtmlParser(_buttonData.ToString())["onClick"].Contains(onClickFunction).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_button_with_width : ButtonDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_ButtonData_With_OnClick_appended_to_the_new_value()
			{
				const string width = "400px";
				ButtonData button = _buttonData.Width(width);
				Assert.AreSame(_buttonData, button);
				TestWebControlsUtility.HtmlParser(_buttonData.ToString())["style"].Contains(width).ShouldBeTrue();
			}
		}
	}
}