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
using MvbaCore.Interfaces;
using NUnit.Framework;

namespace FluentWebControls.Tests.Extensions
{
	public class TextBoxDataExtensionsTest
	{
		public abstract class TextBoxDataExtensionsTestBase
		{
			protected TestData.Item Item;
			protected IPropertyMetaData PropertyMetaData;
			protected TextBoxData TextBoxData;

			[SetUp]
			public void BeforeEachTest()
			{
				Item = new TestData.Item(1, "ItemName");
				PropertyMetaData = PropertyMetaDataMocker.CreateStub("Name", false, null, 100, null, 100, Item.ItemId.GetType());
				TextBoxData = new TextBoxData("value").WithValidationFrom(PropertyMetaData);
			}
		}

		[TestFixture]
		public class When_asked_to_add_TabIndex : TextBoxDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_TextBoxData_With_TabIndex_initialized()
			{
				const string tabIndex = "1";

				var tBox = TextBoxData.WithTabIndex(tabIndex);
				Assert.AreSame(TextBoxData, tBox);
				tBox.ToString().ParseHtmlTag()["tabindex"].ShouldBeEqualTo(tabIndex);
				tBox.ToString().Contains(tabIndex).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_CssClass : TextBoxDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_TextBoxData_With_CssClass_initialized()
			{
				const string cssClass = "textBox";

				var tBox = TextBoxData.CssClass(cssClass);
				Assert.AreSame(TextBoxData, tBox);
				tBox.ToString().Contains(cssClass).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Id_with_lambda : TextBoxDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_TextBoxData_With_Id_initialized()
			{
				var tBox = TextBoxData.WithId(() => Item.ItemName);
				Assert.AreSame(TextBoxData, tBox);
				tBox.ToString().ParseHtmlTag()["id"].ShouldBeEqualTo(Item.ItemName.ToCamelCase());
				tBox.ToString().Contains(Item.ItemId.ToString()).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Id_with_string : TextBoxDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_TextBoxData_With_Id_initialized()
			{
				const string expectedId = "Bar";
				var tBox = TextBoxData.WithId(expectedId);
				Assert.AreSame(TextBoxData, tBox);
				tBox.ToString().ParseHtmlTag()["id"].ShouldBeEqualTo(expectedId.ToCamelCase());
				tBox.ToString().Contains(Item.ItemId.ToString()).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Label : TextBoxDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_TextBoxData_with_Label_initialized_given_a_Label_object()
			{
				var label = new LabelData
				            {
					            Text = "Id"
				            };

				var tBox = TextBoxData.WithLabel(label);
				Assert.AreSame(TextBoxData, tBox);
				var textBox = tBox.ToString();
				textBox.Contains(label.ToString()).ShouldBeTrue();
			}

			[Test]
			public void Should_return_a_TextBoxData_with_Label_initialized_given_a_string_label_text()
			{
				const string labelText = "Id";

				var tBox = TextBoxData.WithLabel(labelText);
				Assert.AreSame(TextBoxData, tBox);
				var label = new LabelData
				            {
					            Text = labelText
				            };
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

				var tBox = TextBoxData.MaxValue(maxValue);
				Assert.AreSame(TextBoxData, tBox);
				tBox.ToString().ParseHtmlTag()[ValidatableWebControlBase.JQueryFieldValidationType.MaxValue.Text].ShouldBeEqualTo(PropertyMetaData.MaxValue.ToString());
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

				var tBox = TextBoxData.MinValue(minValue);
				Assert.AreSame(TextBoxData, tBox);
				tBox.ToString().ParseHtmlTag()[ValidatableWebControlBase.JQueryFieldValidationType.MinValue.Text].ShouldBeEqualTo(minValue.ToString());
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

				var tBox = TextBoxData.Width(width);
				Assert.AreSame(TextBoxData, tBox);
				tBox.ToString().Contains(width).ShouldBeTrue();
			}
		}
	}
}