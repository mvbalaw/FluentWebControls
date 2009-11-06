using System;
using System.Linq.Expressions;

using FluentAssert;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;
using FluentWebControls.Tests.Extensions;
using FluentWebControls.Tools;

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
			public void Should_return_HTML_code_representing_a_textbox_with_all_the_validations_embedded_in_it()
			{
			}

			[Test]
			public void Should_return_HTML_code_representing_a_textbox_with_its_value_embedded_in_it()
			{
				AssertAreEqual(HtmlText, GetTextBoxData(PropertyMetaData));
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_textbox_for_a_property_of_type_int : TextBoxDataTestBase
		{
// ReSharper disable ConvertToConstant
			private readonly int value = 10;
			protected override string HtmlText
			{
				get { return "<input type='text' id='value' name='value' class='textbox digits' value='10'/>"; }
			}
			protected override string HtmlTextWithValidations
			{
				get { return "<input type='text' id='value' name='value' style='width:" + PropertyMetaDataWithValidations.MaxLength * 4 + "px' class='required textbox digits' minlength='1' maxlength='10' max='100' value='10'/><em>*</em>"; }
			}
			protected override string Id
			{
				get
				{
					Expression<Func<int>> expr = () => value;
					return NameUtility.GetPropertyName(expr).ToCamelCase();
				}
			}
// ReSharper restore ConvertToConstant

			protected override IPropertyMetaData PropertyMetaData
			{
				get { return PropertyMetaDataMocker.CreateStub(Id, false, null, null, null, null, typeof(int)); }
			}
			protected override IPropertyMetaData PropertyMetaDataWithValidations
			{
				get { return PropertyMetaDataMocker.CreateStub(Id, true, 1, 10, 0, 100, typeof(int)); }
			}
			protected override string Value
			{
				get { return value.ToString(); }
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_textbox_for_a_property_of_type_nullable_date_time : TextBoxDataTestBase
		{
			private DateTime? value = Convert.ToDateTime("12/1/2008");
			protected override string HtmlText
			{
				get { return "<input type='text' id='value' name='value' class='datebox date' value='12/1/2008 12:00:00 AM'/>"; }
			}
			protected override string HtmlTextWithValidations
			{
				get { return "<input type='text' id='value' name='value' class='required datebox date' value='12/1/2008 12:00:00 AM'/><em>*</em>"; }
			}
			protected override string Id
			{
				get
				{
					Expression<Func<DateTime?>> expr = () => value;
					return NameUtility.GetPropertyName(expr).ToCamelCase();
				}
			}

			protected override IPropertyMetaData PropertyMetaData
			{
				get { return PropertyMetaDataMocker.CreateStub(Id, false, null, null, null, null, typeof(DateTime?)); }
			}
			protected override IPropertyMetaData PropertyMetaDataWithValidations
			{
				get { return PropertyMetaDataMocker.CreateStub(Id, true, null, null, null, null, typeof(DateTime?)); }
			}
			protected override string Value
			{
				get { return value.ToString(); }
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_textbox_for_a_property_of_type_nullable_int : TextBoxDataTestBase
		{
// ReSharper disable RedundantDefaultFieldInitializer
			private int? value = null;
			protected override string HtmlText
			{
				get { return "<input type='text' id='value' name='value' class='textbox digits' value=''/>"; }
			}
			protected override string HtmlTextWithValidations
			{
				get { return "<input type='text' id='value' name='value' style='width:" + PropertyMetaDataWithValidations.MaxLength * 4 + "px' class='required textbox digits' minlength='1' maxlength='10' max='100' value=''/><em>*</em>"; }
			}
			protected override string Id
			{
				get
				{
					Expression<Func<int?>> expr = () => value;
					return NameUtility.GetPropertyName(expr).ToCamelCase();
				}
			}
// ReSharper restore RedundantDefaultFieldInitializer

			protected override IPropertyMetaData PropertyMetaData
			{
				get { return PropertyMetaDataMocker.CreateStub(Id, false, null, null, null, null, typeof(int?)); }
			}
			protected override IPropertyMetaData PropertyMetaDataWithValidations
			{
				get { return PropertyMetaDataMocker.CreateStub(Id, true, 1, 10, 0, 100, typeof(int?)); }
			}
			protected override string Value
			{
				get { return value.ToString(); }
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_textbox_for_a_property_of_type_string : TextBoxDataTestBase
		{
// ReSharper disable ConvertToConstant
// ReSharper disable ConvertToConstant.Local
			private string value = "value";
// ReSharper restore ConvertToConstant.Local
			protected override string HtmlText
			{
				get { return "<input type='text' id='value' name='value' class='textbox' value='value'/>"; }
			}
			protected override string HtmlTextWithValidations
			{
				get { return "<input type='text' id='value' name='value' style='width:" + PropertyMetaDataWithValidations.MaxLength * 4 + "px' class='required textbox' minlength='1' maxlength='10' value='value'/><em>*</em>"; }
			}
			protected override string Id
			{
				get
				{
					Expression<Func<string>> expr = () => value;
					return NameUtility.GetPropertyName(expr).ToCamelCase();
				}
			}
// ReSharper restore ConvertToConstant

			protected override IPropertyMetaData PropertyMetaData
			{
				get { return PropertyMetaDataMocker.CreateStub(Id, false, null, null, null, null, typeof(string)); }
			}
			protected override IPropertyMetaData PropertyMetaDataWithValidations
			{
				get { return PropertyMetaDataMocker.CreateStub(Id, true, 1, 10, 0, null, typeof(string)); }
			}
			protected override string Value
			{
				get { return value; }
			}
		}
	}
}