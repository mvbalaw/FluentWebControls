//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using FluentAssert;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class LabelTest
	{
		[TestFixture]
		public class When_asked_to_create_a_label_for_a_property_of_type_int
		{
			[Test]
			public void Should_return_html_code_representing_a_label_with_its_value_embedded_in_it()
			{
// ReSharper disable ConvertToConstant
// ReSharper disable ConvertToConstant.Local
				var value = 10;
// ReSharper restore ConvertToConstant.Local
// ReSharper restore ConvertToConstant
				var labelData = Label.For(() => value);
				labelData.ToString().ShouldBeEqualTo("<label for='value' style='float:left;text-align:right'></label>");
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_label_for_a_property_of_type_nullable_int
		{
			[Test]
			public void Should_return_html_code_representing_a_label_with_its_value_embedded_in_it()
			{
				int? value = 10;
				var labelData = Label.For(() => value);
				labelData.ToString().ShouldBeEqualTo("<label for='value' style='float:left;text-align:right'></label>");
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_label_for_a_property_of_type_string
		{
			[Test]
			public void Should_return_html_code_representing_a_label_with_its_value_embedded_in_it()
			{
// ReSharper disable ConvertToConstant.Local
				var value = "Bar";
// ReSharper restore ConvertToConstant.Local
				var labelData = Label.For(() => value);
				labelData.ToString().ShouldBeEqualTo("<label for='value' style='float:left;text-align:right'></label>");
			}
		}
	}
}