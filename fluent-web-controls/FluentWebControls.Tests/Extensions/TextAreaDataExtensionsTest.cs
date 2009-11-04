using FluentAssert;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;

using NUnit.Framework;

namespace FluentWebControls.Tests.Extensions
{
	public class TextAreaDataExtensionsTest
	{
		public abstract class TextAreaDataExtensionsTestBase
		{
			protected TestData.Item _item;
			protected IPropertyMetaData _propertyMetaData;
			protected TextAreaData _textAreaData;

			[SetUp]
			public void BeforeEachTest()
			{
				_item = new TestData.Item(1, "ItemName");
				_propertyMetaData = PropertyMetaDataMocker.CreateStub("Name", false, null, 100, null, 100, _item.ItemId.GetType());
				_textAreaData = new TextAreaData("value", _propertyMetaData);
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Columns : TextAreaDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_TextAreaData_With_CssClass_initialized()
			{
				const int cols = 10;

				TextAreaData tArea = _textAreaData.Cols(cols);
				Assert.AreSame(_textAreaData, tArea);
				TestWebControlsUtility.HtmlParser(tArea.ToString())["cols"].ShouldBeEqualTo(cols.ToString());
				tArea.ToString().Contains(cols.ToString()).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_CssClass : TextAreaDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_TextAreaData_With_CssClass_initialized()
			{
				const string cssClass = "textBox";

				TextAreaData tArea = _textAreaData.CssClass(cssClass);
				Assert.AreSame(_textAreaData, tArea);
				tArea.ToString().Contains(cssClass).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Id : TextAreaDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_TextAreaData_With_Id_initialized()
			{
				TextAreaData tArea = _textAreaData.WithId(() => _item.ItemName);
				Assert.AreSame(_textAreaData, tArea);
				TestWebControlsUtility.HtmlParser(tArea.ToString())["id"].ShouldBeEqualTo(_item.ItemName.ToCamelCase());
				tArea.ToString().Contains(_item.ItemId.ToString()).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Label : TextAreaDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_TextAreaData_With_Width_initialized()
			{
				LabelData label = new LabelData("Id");

				TextAreaData tArea = _textAreaData.WithLabel(label);
				Assert.AreSame(_textAreaData, tArea);
				tArea.ToString().Contains(label.ToString()).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Rows : TextAreaDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_TextAreaData_With_CssClass_initialized()
			{
				const int rows = 10;

				TextAreaData tArea = _textAreaData.Rows(rows);
				Assert.AreSame(_textAreaData, tArea);
				TestWebControlsUtility.HtmlParser(tArea.ToString())["rows"].ShouldBeEqualTo(rows.ToString());
				tArea.ToString().Contains(rows.ToString()).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Width : TextAreaDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_TextAreaData_With_Width_initialized()
			{
				const string width = "32px";

				TextAreaData tArea = _textAreaData.Width(width);
				Assert.AreSame(_textAreaData, tArea);
				tArea.ToString().Contains(width).ShouldBeTrue();
			}
		}
	}
}