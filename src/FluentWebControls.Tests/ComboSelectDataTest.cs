using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using FluentAssert;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;
using FluentWebControls.Tests.Extensions;

using MvbaCore;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class ComboSelectDataTest
	{
		public abstract class ComboSelectDataTestBase
		{
			protected ComboSelectData _comboSelectData;
			protected abstract string HtmlText { get; }
			protected abstract string Id { get; }
			protected abstract IEnumerable<KeyValuePair<string, string>> Items { get; }
			protected abstract IPropertyMetaData PropertyMetaData { get; }

			private static void AssertAreEqual(string textToCompare, ComboSelectData comboSelectData)
			{
				comboSelectData.ToString().ShouldBeEqualTo(textToCompare);
			}

			protected virtual void SetSelected()
			{
			}

			[Test]
			public void Should_return_HTML_code_representing_a_ComboSelect_with_its_value_embedded_in_it()
			{
				_comboSelectData = new ComboSelectData(Items)
					.WithValidationFrom(PropertyMetaData)
					.WithId(Id);

				SetSelected();
				AssertAreEqual(HtmlText, _comboSelectData);
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_ComboSelect_for_a_property : ComboSelectDataTestBase
		{
			// cannot be const or Reflection.GetPropertyName won't handle it correctly
// ReSharper disable ConvertToConstant.Local
			private string _value = "value";
// ReSharper restore ConvertToConstant.Local
			protected override string HtmlText
			{
				get { return "<select name='_value' id='_value' class='required comboselect' multiple='multiple' size='6'><option value='Value1' selected='selected'>Name1</option><option value='Value2'>Name2</option><option value='Value3' selected='selected'>Name3</option><option value='Value4'>Name4</option></select><em>*</em>"; }
			}

			protected override string Id
			{
				get
				{
					Expression<Func<string>> expr = () => _value;
					return Reflection.GetPropertyName(expr).ToCamelCase();
				}
			}

			protected override IEnumerable<KeyValuePair<string, string>> Items
			{
				get
				{
					return new List<KeyValuePair<string, string>>
						{
							new KeyValuePair<string, string>("Name1", "Value1"),
							new KeyValuePair<string, string>("Name2", "Value2"),
							new KeyValuePair<string, string>("Name3", "Value3"),
							new KeyValuePair<string, string>("Name4", "Value4"),
						};
				}
			}

			protected override IPropertyMetaData PropertyMetaData
			{
				get { return PropertyMetaDataMocker.CreateStub(Id, true, null, null, null, null, typeof(string)); }
			}

			protected override void SetSelected()
			{
				_comboSelectData.SelectedValues.Add("Value1");
				_comboSelectData.SelectedValues.Add("Value3");
			}
		}
	}
}