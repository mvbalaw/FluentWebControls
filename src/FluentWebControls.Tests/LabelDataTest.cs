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

using MvbaCore;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class LabelDataTest
	{
		public abstract class LabelDataTestBase
		{
			protected abstract string ForId { get; }
			protected abstract string HtmlText { get; }
			protected abstract string Text { get; }

			private LabelData GetLabelData()
			{
				return new LabelData(ForId)
				       {
					       Text = Text
				       };
			}

			[Test]
			public void Should_return_HTML_code_representing_a_label_field_with_its_value_embedded_in_it()
			{
				GetLabelData().ToString().ShouldBeEqualTo(HtmlText);
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_label_for_a_nullable_int : LabelDataTestBase
		{
// ReSharper disable RedundantDefaultFieldInitializer
			private int? value = null;
// ReSharper restore RedundantDefaultFieldInitializer

			protected override string ForId
			{
				get
				{
					Expression<Func<int?>> expr = () => value;
					return Reflection.GetPropertyName(expr).ToCamelCase();
				}
			}

			protected override string HtmlText => "<label for='value'></label>";

			protected override string Text => value.ToString();
		}

		[TestFixture]
		public class When_asked_to_create_a_label_for_a_string : LabelDataTestBase
		{
// ReSharper disable once ConvertToConstant.Local
// ReSharper disable once FieldCanBeMadeReadOnly.Local
// ReSharper disable once InconsistentNaming
			private string value = "value";

			protected override string ForId
			{
				get
				{
					Expression<Func<string>> expr = () => value;
					return Reflection.GetPropertyName(expr).ToCamelCase();
				}
			}

			protected override string HtmlText => "<label for='value'>value</label>";

			protected override string Text => value;
		}

		[TestFixture]
		public class When_asked_to_create_a_label_for_an_int : LabelDataTestBase
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

			protected override string HtmlText => "<label for='_value'>10</label>";

			protected override string Text => _value.ToString();
		}
	}
}