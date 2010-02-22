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
	public class DropDownListDataTest
	{
		public abstract class DropDownListDataTestBase
		{
			protected DropDownListData _dropDownListData;
			protected abstract string HtmlText { get; }
			protected abstract string Id { get; }
			protected abstract IEnumerable<KeyValuePair<string, string>> Items { get; }
			protected abstract IPropertyMetaData PropertyMetaData { get; }

			private static void AssertAreEqual(string textToCompare, DropDownListData dropDownListData)
			{
				dropDownListData.ToString().ShouldBeEqualTo(textToCompare);
			}

			protected virtual void SetDefault()
			{
			}

			protected virtual void SetSelected()
			{
			}

			protected virtual void SetSubmitOnChange()
			{
			}

			[Test]
			public void Should_return_HTML_code_representing_a_DropDownList_with_its_value_embedded_in_it()
			{
				_dropDownListData = new DropDownListData(Items)
					.WithValidationFrom(PropertyMetaData)
					.WithId(Id);

				SetDefault();
				SetSelected();
				SetSubmitOnChange();
				AssertAreEqual(HtmlText, _dropDownListData);
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_DropDownList_for_a_property : DropDownListDataTestBase
		{
			// cannot be const or Reflection.GetPropertyName won't handle it correctly
// ReSharper disable ConvertToConstant.Local
			private string _value = "value";
// ReSharper restore ConvertToConstant.Local
			protected override string HtmlText
			{
				get { return "<select name='_value' id='_value' class='required ddlDetail'><option value='Value1'>Name1</option><option value='Value2' selected='selected'>Name2</option></select><em>*</em>"; }
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
							new KeyValuePair<string, string>("Name2", "Value2")
						};
				}
			}
			protected override IPropertyMetaData PropertyMetaData
			{
				get { return PropertyMetaDataMocker.CreateStub(Id, true, null, null, null, null, typeof(string)); }
			}

			protected override void SetSelected()
			{
				_dropDownListData.SelectedValue = "Value2";
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_DropDownList_for_a_property_with_Default_value : DropDownListDataTestBase
		{
			// cannot be const or Reflection.GetPropertyName won't handle it correctly
// ReSharper disable ConvertToConstant.Local
			private string _value = "value";
// ReSharper restore ConvertToConstant.Local
			protected override string HtmlText
			{
				get { return "<select name='_value' id='_value' class='ddlDetail'><option value='' selected='selected'>All</option><option value='Value1'>Name1</option><option value='Value2'>Name2</option></select>"; }
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
							new KeyValuePair<string, string>("Name2", "Value2")
						};
				}
			}
			protected override IPropertyMetaData PropertyMetaData
			{
				get { return PropertyMetaDataMocker.CreateStub(Id, false, null, null, null, null, typeof(string)); }
			}

			protected override void SetDefault()
			{
				_dropDownListData.Default = new KeyValuePair<string, string>("All", "");
			}

			protected override void SetSelected()
			{
				_dropDownListData.SelectedValue = "";
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_DropDownList_for_a_property_with_SubmitOnChange_set_to_true : DropDownListDataTestBase
		{
// ReSharper disable ConvertToConstant.Local
			private string _value = "value";
// ReSharper restore ConvertToConstant.Local
			protected override string HtmlText
			{
				get { return "<select name='_value' id='_value' class='ddlDetail' onchange='this.form.submit();'><option value='Value1'>Name1</option><option value='Value2'>Name2</option></select>"; }
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
							new KeyValuePair<string, string>("Name2", "Value2")
						};
				}
			}
			protected override IPropertyMetaData PropertyMetaData
			{
				get { return PropertyMetaDataMocker.CreateStub(Id, false, null, null, null, null, typeof(string)); }
			}

			protected override void SetSubmitOnChange()
			{
				_dropDownListData.SubmitOnChange = true;
			}
		}
	}
}