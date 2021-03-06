﻿//  * **************************************************************************
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

using MvbaCore;

using NUnit.Framework;

namespace FluentWebControls.Tests.Extensions
{
	public class RadioButtonDataExtensionsTest
	{
		public abstract class RadioButtonDataExtensionsTestBase
		{
			protected bool IsChecked;
			protected RadioButtonData RadioButtonData;

			[SetUp]
			public void BeforeEachTest()
			{
				IsChecked = true;
				RadioButtonData = new RadioButtonData(true);
			}
		}

		[TestFixture]
		public class When_asked_to_add_TabIndex : RadioButtonDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_RadioButtonData_With_TabIndex_initialized()
			{
				const string tabIndex = "1";

				var checkBoxData = RadioButtonData.WithTabIndex(tabIndex);
				Assert.AreSame(RadioButtonData, checkBoxData);
				checkBoxData.ToString().ParseHtmlTag()["tabindex"].ShouldBeEqualTo(tabIndex);
				checkBoxData.ToString().Contains(tabIndex).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Id : RadioButtonDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_RadioButtonData_With_Id_initialized()
			{
				var checkBoxData = RadioButtonData.WithId("isChecked");
				Assert.AreSame(RadioButtonData, checkBoxData);
				var propertyName = Reflection.GetPropertyName(() => IsChecked).ToCamelCase();
				RadioButtonData.ToString().ParseHtmlTag()["id"].ShouldBeEqualTo(propertyName.ToCamelCase());
// ReSharper disable once AssignNullToNotNullAttribute
				checkBoxData.ToString().Contains(propertyName).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_IsChecked : RadioButtonDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_RadioButtonData_With_IsChecked_initialized()
			{
				var checkBoxData = RadioButtonData.IsChecked(IsChecked);
				Assert.AreSame(RadioButtonData, checkBoxData);
				const string checkedAttribute = "checked";
				RadioButtonData.ToString().ParseHtmlTag()[checkedAttribute].ShouldBeEqualTo("checked");
				checkBoxData.ToString().Contains(checkedAttribute).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Label : RadioButtonDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_RadioButtonData_With_Label_initialized()
			{
				var label = new LabelData("Id");
				var blankLabel = new LabelData
				                 {
					                 Text = "&nbsp;"
				                 };

				var checkBoxData = RadioButtonData.WithLabel(label);
				Assert.AreSame(RadioButtonData, checkBoxData);
				checkBoxData.ToString().Contains(label.ToString()).ShouldBeTrue();
				checkBoxData.ToString().Contains(blankLabel.ToString()).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Label_on_left : RadioButtonDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_RadioButtonData_With_Label_initialized()
			{
				var label = new LabelData("Id");
				var blankLabel = new LabelData
				                 {
					                 Text = "&nbsp;"
				                 };

				var checkBoxData = RadioButtonData.WithLabelAlignedLeft(label);
				Assert.AreSame(RadioButtonData, checkBoxData);
				checkBoxData.ToString().Contains(label.ToString()).ShouldBeTrue();
				checkBoxData.ToString().Contains(blankLabel.ToString()).ShouldBeFalse();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_value : RadioButtonDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_RadioButtonData_With_Value_initialized()
			{
				Test.Given(RadioButtonData)
					.When(value_set_to_false)
					.Should(put_the_value_in_the_generated_html)
					.Verify();
			}

			[Test]
			public void Should_return_a_RadioButtonData_With_Value_initialized_to_true_by_default()
			{
				Test.Given(RadioButtonData)
					.When(value_not_set)
					.Should(set_the_value_to_true_in_the_generated_html)
					.Verify();
			}

			private void put_the_value_in_the_generated_html(RadioButtonData checkBoxData)
			{
				RadioButtonData.ToString().ParseHtmlTag()["value"].ShouldBeEqualTo("false");
			}

			private void set_the_value_to_true_in_the_generated_html(RadioButtonData obj)
			{
				RadioButtonData.ToString().ParseHtmlTag()["value"].ShouldBeEqualTo("true");
			}

			private static void value_not_set(RadioButtonData checkBoxData)
			{
				checkBoxData.WithValue(null);
			}

			private static void value_set_to_false(RadioButtonData checkBoxData)
			{
				checkBoxData.WithValue("false");
			}
		}
	}
}