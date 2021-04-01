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

using MvbaCore;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class HiddenDataTest
	{
		public abstract class HiddenDataTestBase
		{
			protected abstract string ForId { get; }
			protected abstract string HtmlText { get; }
			protected abstract string Text { get; }

			private HiddenData GetHiddenData()
			{
				return new HiddenData().WithValue(Text).WithId(ForId);
			}

			[Test]
			public void Should_return_HTML_code_representing_a_hidden_field_with_its_value_embedded_in_it()
			{
				GetHiddenData().ToString().ShouldBeEqualTo(HtmlText);
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_hidden_field : HiddenDataTestBase
		{
			private readonly int _value = 10;

			protected override string ForId
			{
				get
				{
					Expression<Func<int>> expr = () => _value;
					return Reflection.GetPropertyName(expr).ToCamelCase();
				}
			}

			protected override string HtmlText => "<input type='hidden' id='_value' name='_value' value='10'/>";

			protected override string Text => _value.ToString();
		}
	}
}