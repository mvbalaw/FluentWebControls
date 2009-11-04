using FluentAssert;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;

using NUnit.Framework;

namespace FluentWebControls.Tests.Extensions
{
	public class TextBoxDataExtensionsTest
	{
		public abstract class TextBoxDataExtensionsTestBase
		{
			protected TestData.Item _item;
			protected IPropertyMetaData _propertyMetaData;
			protected TextBoxData _textBoxData;

			[SetUp]
			public void BeforeEachTest()
			{
				_item = new TestData.Item(1, "ItemName");
				_propertyMetaData = PropertyMetaDataMocker.CreateStub("Name", false, null, 100, null, 100, _item.ItemId.GetType());
				_textBoxData = new TextBoxData("value", _propertyMetaData);
			}
		}

		[TestFixture]
		public class When_asked_to_assign_CssClass : TextBoxDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_TextBoxData_With_CssClass_initialized()
			{
				const string cssClass = "textBox";

				TextBoxData tBox = _textBoxData.CssClass(cssClass);
				Assert.AreSame(_textBoxData, tBox);
				tBox.ToString().Contains(cssClass).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Id_with_lambda : TextBoxDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_TextBoxData_With_Id_initialized()
			{
				TextBoxData tBox = _textBoxData.WithId(() => _item.ItemName);
				Assert.AreSame(_textBoxData, tBox);
				TestWebControlsUtility.HtmlParser(tBox.ToString())["id"].ShouldBeEqualTo(_item.ItemName.ToCamelCase());
				tBox.ToString().Contains(_item.ItemId.ToString()).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Id_with_string : TextBoxDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_TextBoxData_With_Id_initialized()
			{
				const string expectedId = "Bar";
				TextBoxData tBox = _textBoxData.WithId(expectedId);
				Assert.AreSame(_textBoxData, tBox);
				TestWebControlsUtility.HtmlParser(tBox.ToString())["id"].ShouldBeEqualTo(expectedId.ToCamelCase());
				tBox.ToString().Contains(_item.ItemId.ToString()).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Label : TextBoxDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_TextBoxData_With_Width_initialized()
			{
				LabelData label = new LabelData("Id");

				TextBoxData tBox = _textBoxData.WithLabel(label);
				Assert.AreSame(_textBoxData, tBox);
				tBox.ToString().Contains(label.ToString()).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_MaxValue : TextBoxDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_TextBoxData_With_CssClass_initialized()
			{
				const int maxValue = 10;

				TextBoxData tBox = _textBoxData.MaxValue(maxValue);
				Assert.AreSame(_textBoxData, tBox);
				TestWebControlsUtility.HtmlParser(tBox.ToString())[ValidatableWebControlBase.JQueryFieldValidationType.MaxValue.Text].ShouldBeEqualTo(_propertyMetaData.MaxValue.ToString());
				tBox.ToString().Contains(maxValue.ToString()).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_MinValue : TextBoxDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_TextBoxData_With_CssClass_initialized()
			{
				const int minValue = 1;

				TextBoxData tBox = _textBoxData.MinValue(minValue);
				Assert.AreSame(_textBoxData, tBox);
				TestWebControlsUtility.HtmlParser(tBox.ToString())[ValidatableWebControlBase.JQueryFieldValidationType.MinValue.Text].ShouldBeEqualTo(minValue.ToString());
				tBox.ToString().Contains(minValue.ToString()).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Width : TextBoxDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_TextBoxData_With_Width_initialized()
			{
				const string width = "32px";

				TextBoxData tBox = _textBoxData.Width(width);
				Assert.AreSame(_textBoxData, tBox);
				tBox.ToString().Contains(width).ShouldBeTrue();
			}
		}
	}
}