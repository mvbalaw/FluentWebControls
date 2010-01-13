using System.Collections.Generic;

using FluentAssert;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;

using NUnit.Framework;

namespace FluentWebControls.Tests.Extensions
{
	public class ComboSelectDataExtensionsTest
	{
		public abstract class ComboSelectDataExtensionsTestBase
		{
			private readonly List<KeyValuePair<string, string>> _items = new List<KeyValuePair<string, string>>
				{
					new KeyValuePair<string, string>("Name1", "1"),
					new KeyValuePair<string, string>("Name2", "2"),
					new KeyValuePair<string, string>("Name3", "3")
				};

			protected ComboSelectData _comboSelectData;
			protected IPropertyMetaData _propertyMetaData;

			[SetUp]
			public void BeforeEachTest()
			{
				_propertyMetaData = PropertyMetaDataMocker.CreateStub("Name", false, null, null, null, null, typeof(string));
				_comboSelectData = new ComboSelectData(_items).WithValidationFrom(_propertyMetaData);
			}
		}

		private class Values
		{
			public Values(string id, int value)
			{
				Id = id;
				Value = value;
			}

			public string Id { get; private set; }
			public int Value { get; private set; }
		}

		[TestFixture]
		public class When_asked_to_assign_CssClass : ComboSelectDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_ComboSelectData_With_CssClass_initialized()
			{
				const string cssClass = "cssClass";
				var listData = _comboSelectData.CssClass(cssClass);
				Assert.AreSame(_comboSelectData, listData);
				TestWebControlsUtility.HtmlParser(listData.ToString())["class"].ShouldBeEqualTo(cssClass);
				_comboSelectData.ToString().Contains(cssClass).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Label : ComboSelectDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_ComboSelectData_With_Label_initialized()
			{
				var label = new LabelData("Id");

				var listData = _comboSelectData.WithLabel(label);
				Assert.AreSame(_comboSelectData, listData);
				listData.ToString().Contains(label.ToString()).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Set_a_set_of_selected_Values : ComboSelectDataExtensionsTestBase
		{
			private readonly IList<Values> _selectedValues = new List<Values>
				{
					new Values("Name2", 2),
					new Values("Name3", 3)
				};

			[Test]
			public void Should_return_a_ComboSelectData_with_selected_values_assigned_to_option_that_was_selected()
			{
				if (_comboSelectData != null)
				{
					var listData = _comboSelectData.WithSelectedValues(_selectedValues, values => values.Value);
					Assert.AreSame(_comboSelectData, listData);
				}
				_comboSelectData.ToString().Contains("<option value='1'>Name1</option>").ShouldBeTrue();
				_comboSelectData.ToString().Contains("<option value='2' selected='selected'>Name2</option>").ShouldBeTrue();
				_comboSelectData.ToString().Contains("<option value='3' selected='selected'>Name3</option>").ShouldBeTrue();
			}
		}
	}
}