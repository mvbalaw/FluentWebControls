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
using System.Linq.Expressions;

using FluentAssert;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;
using FluentWebControls.Tests.Extensions;

using MvbaCore;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class TextBoxDataTest
	{
		public abstract class TextBoxDataTestBase
		{
			protected abstract string HtmlText { get; }
			protected abstract string HtmlTextWithValidations { get; }
			protected abstract string Id { get; }
			protected abstract IPropertyMetaData PropertyMetaData { get; }
			protected abstract IPropertyMetaData PropertyMetaDataWithValidations { get; }
			protected abstract string Value { get; }

			private static void AssertAreEqual(string textToCompare, TextBoxData textBoxData)
			{
				textBoxData.ToString().ShouldBeEqualTo(textToCompare);
			}

			private TextBoxData GetTextBoxData(IPropertyMetaData propertyMetaData)
			{
				return new TextBoxData(Value)
					.WithValidationFrom(propertyMetaData)
					.WithId(Id);
			}

			[Test]
			public void Should_return_HTML_code_representing_a_TextBox_with_its_value_embedded_in_it()
			{
				AssertAreEqual(HtmlText, GetTextBoxData(PropertyMetaData));
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_TextBox_for_a_property_of_type_int : TextBoxDataTestBase
		{
// ReSharper disable InconsistentNaming
			private readonly int value = 10;
// ReSharper restore InconsistentNaming
			protected override string HtmlText => "<input type='text' id='value' name='value' class='textbox digits' value='10'/>";

			protected override string HtmlTextWithValidations => "<input type='text' id='value' name='value' style='width:" + PropertyMetaDataWithValidations.MaxLength * 4 + "px' class='required textbox digits' minlength='1' maxlength='10' max='100' value='10'/><em>*</em>";

			protected override string Id
			{
				get
				{
					Expression<Func<int>> expr = () => value;
					return Reflection.GetPropertyName(expr).ToCamelCase();
				}
			}

// ReSharper restore ConvertToConstant

			protected override IPropertyMetaData PropertyMetaData => PropertyMetaDataMocker.CreateStub(Id, false, null, null, null, null, typeof(int));

			protected override IPropertyMetaData PropertyMetaDataWithValidations => PropertyMetaDataMocker.CreateStub(Id, true, 1, 10, 0, 100, typeof(int));

			protected override string Value => value.ToString();
		}

		[TestFixture]
		public class When_asked_to_create_a_TextBox_for_a_property_of_type_nullable_date_time : TextBoxDataTestBase
		{
// ReSharper disable InconsistentNaming
			private DateTime? value = Convert.ToDateTime("12/1/2008");
// ReSharper restore InconsistentNaming
			protected override string HtmlText => "<input type='text' id='value' name='value' class='datebox date' value='12/1/2008 12:00:00 AM'/>";

			protected override string HtmlTextWithValidations => "<input type='text' id='value' name='value' class='required datebox date' value='12/1/2008 12:00:00 AM'/><em>*</em>";

			protected override string Id
			{
				get
				{
					Expression<Func<DateTime?>> expr = () => value;
					return Reflection.GetPropertyName(expr).ToCamelCase();
				}
			}

			protected override IPropertyMetaData PropertyMetaData => PropertyMetaDataMocker.CreateStub(Id, false, null, null, null, null, typeof(DateTime?));

			protected override IPropertyMetaData PropertyMetaDataWithValidations => PropertyMetaDataMocker.CreateStub(Id, true, null, null, null, null, typeof(DateTime?));

			protected override string Value => value.ToString();
		}

		[TestFixture]
		public class When_asked_to_create_a_TextBox_for_a_property_of_type_nullable_int : TextBoxDataTestBase
		{
// ReSharper disable RedundantDefaultFieldInitializer
// ReSharper disable InconsistentNaming
			private int? value = null;
// ReSharper restore InconsistentNaming
			protected override string HtmlText => "<input type='text' id='value' name='value' class='textbox digits' value=''/>";

			protected override string HtmlTextWithValidations => "<input type='text' id='value' name='value' style='width:" + PropertyMetaDataWithValidations.MaxLength * 4 + "px' class='required textbox digits' minlength='1' maxlength='10' max='100' value=''/><em>*</em>";

			protected override string Id
			{
				get
				{
					Expression<Func<int?>> expr = () => value;
					return Reflection.GetPropertyName(expr).ToCamelCase();
				}
			}

// ReSharper restore RedundantDefaultFieldInitializer

			protected override IPropertyMetaData PropertyMetaData => PropertyMetaDataMocker.CreateStub(Id, false, null, null, null, null, typeof(int?));

			protected override IPropertyMetaData PropertyMetaDataWithValidations => PropertyMetaDataMocker.CreateStub(Id, true, 1, 10, 0, 100, typeof(int?));

			protected override string Value => value.ToString();
		}

		[TestFixture]
		public class When_asked_to_create_a_TextBox_for_a_property_of_type_string : TextBoxDataTestBase
		{
// ReSharper disable once ConvertToConstant.Local
// ReSharper disable once FieldCanBeMadeReadOnly.Local
// ReSharper disable once InconsistentNaming
			private string value = "value";
			protected override string HtmlText => "<input type='text' id='value' name='value' class='textbox' value='value'/>";

			protected override string HtmlTextWithValidations => "<input type='text' id='value' name='value' style='width:" + PropertyMetaDataWithValidations.MaxLength * 4 + "px' class='required textbox' minlength='1' maxlength='10' value='value'/><em>*</em>";

			protected override string Id
			{
				get
				{
					Expression<Func<string>> expr = () => value;
					return Reflection.GetPropertyName(expr).ToCamelCase();
				}
			}

// ReSharper restore ConvertToConstant

			protected override IPropertyMetaData PropertyMetaData => PropertyMetaDataMocker.CreateStub(Id, false, null, null, null, null, typeof(string));

			protected override IPropertyMetaData PropertyMetaDataWithValidations => PropertyMetaDataMocker.CreateStub(Id, true, 1, 10, 0, null, typeof(string));

			protected override string Value => value;
		}

		[TestFixture]
		public class When_asked_to_make_the_TextBox_readonly
		{
			[Test]
			public void Should_add_the_READONLY_attribute_if_ReadOnly_is_true()
			{
				var textBox = new TextBoxData("World").AsReadOnly();
				var result = textBox.ToString();
				result.ShouldBeEqualTo("<input type='text' class='textbox' value='World' READONLY/>");
			}

			[Test]
			public void Should_not_add_the_READONLY_attribute_if_ReadOnly_is_false()
			{
				var textBox = new TextBoxData("World");
				var result = textBox.ToString();
				result.ShouldBeEqualTo("<input type='text' class='textbox' value='World'/>");
			}
		}
	}
}