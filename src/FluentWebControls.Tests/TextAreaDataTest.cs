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
	public class TextAreaDataTest
	{
		public abstract class TextAreaDataTestBase
		{
			protected abstract string HtmlText { get; }
			protected abstract string HtmlTextWithValidations { get; }
			protected abstract string Id { get; }
			protected abstract IPropertyMetaData PropertyMetaData { get; }
			protected abstract IPropertyMetaData PropertyMetaDataWithValidations { get; }
			protected abstract string Value { get; }

			private static void AssertAreEqual(string textToCompare, TextAreaData textAreaData)
			{
				textAreaData.ToString().ShouldBeEqualTo(textToCompare);
			}

			private TextAreaData GetTextAreaData(IPropertyMetaData propertyMetaData)
			{
				return new TextAreaData(Value)
					.WithValidationFrom(propertyMetaData)
					.WithId(Id);
			}

			[Test]
			public void Should_return_HTML_code_representing_a_textarea_with_all_the_validations_embedded_in_it()
			{
				AssertAreEqual(HtmlTextWithValidations, GetTextAreaData(PropertyMetaDataWithValidations));
			}

			[Test]
			public void Should_return_HTML_code_representing_a_textarea_with_its_value_embedded_in_it()
			{
				AssertAreEqual(HtmlText, GetTextAreaData(PropertyMetaData));
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_textarea_for_a_property : TextAreaDataTestBase
		{
// ReSharper disable ConvertToConstant.Local
// ReSharper disable InconsistentNaming
			private string value = "value";
// ReSharper restore InconsistentNaming
// ReSharper restore ConvertToConstant.Local
			protected override string HtmlText
			{
				get { return "<textarea id='value' name='value' class='textbox'>value</textarea>"; }
			}
			protected override string HtmlTextWithValidations
			{
				get { return "<textarea id='value' name='value' class='required textbox' minlength='1' maxlength='10'>value</textarea><em>*</em>"; }
			}
			protected override string Id
			{
				get
				{
					Expression<Func<string>> expr = () => value;
					return Reflection.GetPropertyName(expr).ToCamelCase();
				}
			}

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