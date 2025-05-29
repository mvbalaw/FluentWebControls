//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using System.Collections.Generic;

using FluentAssert;

using FluentWebControls.Extensions;
using MvbaCore.Interfaces;
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

			protected ComboSelectData ComboSelectData;
			protected IPropertyMetaData PropertyMetaData;

			[SetUp]
			public void BeforeEachTest()
			{
				PropertyMetaData = PropertyMetaDataMocker.CreateStub("Name", false, null, null, null, null, typeof(string));
				ComboSelectData = new ComboSelectData(_items).WithValidationFrom(PropertyMetaData);
			}
		}

		private class Values
		{
			public Values(string id, int value)
			{
				Id = id;
				Value = value;
			}

			public string Id { get; }
			public int Value { get; }
		}

		[TestFixture]
		public class When_asked_to_add_TabIndex : ComboSelectDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_ComboSelectData_With_TabIndex_initialized()
			{
				const string tabIndex = "1";

				var listData = ComboSelectData.WithTabIndex(tabIndex);
				Assert.AreSame(ComboSelectData, listData);
				listData.ToString().ParseHtmlTag()["tabindex"].ShouldBeEqualTo(tabIndex);
				listData.ToString().Contains(tabIndex).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_CssClass : ComboSelectDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_ComboSelectData_With_CssClass_initialized()
			{
				const string cssClass = "cssClass";
				var listData = ComboSelectData.CssClass(cssClass);
				Assert.AreSame(ComboSelectData, listData);
				listData.ToString().ParseHtmlTag()["class"].ShouldBeEqualTo("comboselect cssClass");
				ComboSelectData.ToString().Contains("comboselect cssClass").ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Label : ComboSelectDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_ComboSelectData_With_Label_initialized()
			{
				var label = new LabelData("Id");

				var listData = ComboSelectData.WithLabel(label);
				Assert.AreSame(ComboSelectData, listData);
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
				if (ComboSelectData != null)
				{
					var listData = ComboSelectData.WithSelectedValues(_selectedValues, values => values.Value);
					Assert.AreSame(ComboSelectData, listData);
				}
// ReSharper disable once PossibleNullReferenceException
				var str = ComboSelectData.ToString();
				str.Contains("<option value='1'>Name1</option>").ShouldBeTrue();
				str.Contains("<option value='2' selected='selected'>Name2</option>").ShouldBeTrue();
				str.Contains("<option value='3' selected='selected'>Name3</option>").ShouldBeTrue();
			}
		}
	}
}