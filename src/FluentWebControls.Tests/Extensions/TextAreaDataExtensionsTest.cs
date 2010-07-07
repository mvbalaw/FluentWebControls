using System;

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
			protected TestData.Item Item;
			protected IPropertyMetaData PropertyMetaData;
			protected TextAreaData TextAreaData;

			[SetUp]
			public void BeforeEachTest()
			{
				Item = new TestData.Item(1, "ItemName");
				PropertyMetaData = PropertyMetaDataMocker.CreateStub("Name", false, null, 100, null, 100, Item.ItemId.GetType());
				TextAreaData = new TextAreaData("value").WithValidationFrom(PropertyMetaData);
			}
		}

		[TestFixture]
		public class When_asked_to_add_TabIndex : TextAreaDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_TextAreaData_With_TabIndex_initialized()
			{
				const string tabIndex = "1";

				var tArea = TextAreaData.WithTabIndex(tabIndex);
				Assert.AreSame(TextAreaData, tArea);
				tArea.ToString().ParseHtmlTag()["tabindex"].ShouldBeEqualTo(tabIndex);
				tArea.ToString().Contains(tabIndex).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Columns : TextAreaDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_TextAreaData_With_CssClass_initialized()
			{
				const int cols = 10;

				var tArea = TextAreaData.Cols(cols);
				Assert.AreSame(TextAreaData, tArea);
				tArea.ToString().ParseHtmlTag()["cols"].ShouldBeEqualTo(cols.ToString());
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

				var tArea = TextAreaData.CssClass(cssClass);
				Assert.AreSame(TextAreaData, tArea);
				tArea.ToString().Contains(cssClass).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Id : TextAreaDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_TextAreaData_With_Id_initialized()
			{
				var tArea = TextAreaData.WithId(() => Item.ItemName);
				Assert.AreSame(TextAreaData, tArea);
				tArea.ToString().ParseHtmlTag()["id"].ShouldBeEqualTo(Item.ItemName.ToCamelCase());
				tArea.ToString().Contains(Item.ItemId.ToString()).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Label : TextAreaDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_TextAreaData_With_Width_initialized()
			{
				var label = new LabelData("Id");

				var tArea = TextAreaData.WithLabel(label);
				Assert.AreSame(TextAreaData, tArea);
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

				var tArea = TextAreaData.Rows(rows);
				Assert.AreSame(TextAreaData, tArea);
				tArea.ToString().ParseHtmlTag()["rows"].ShouldBeEqualTo(rows.ToString());
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

				var tArea = TextAreaData.Width(width);
				Assert.AreSame(TextAreaData, tArea);
				tArea.ToString().Contains(width).ShouldBeTrue();
			}
		}
	}
}