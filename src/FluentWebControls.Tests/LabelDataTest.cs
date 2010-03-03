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

			protected override string HtmlText
			{
				get { return "<label for='value' style='float:left;text-align:right'></label>"; }
			}
			protected override string Text
			{
				get { return value.ToString(); }
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_label_for_a_string : LabelDataTestBase
		{
// ReSharper disable ConvertToConstant.Local
// ReSharper disable InconsistentNaming
			private string value = "value";
// ReSharper restore InconsistentNaming
// ReSharper restore ConvertToConstant.Local

			protected override string ForId
			{
				get
				{
					Expression<Func<string>> expr = () => value;
					return Reflection.GetPropertyName(expr).ToCamelCase();
				}
			}

			protected override string HtmlText
			{
				get { return "<label for='value' style='float:left;text-align:right'>value</label>"; }
			}
			protected override string Text
			{
				get { return value; }
			}
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

			protected override string HtmlText
			{
				get { return "<label for='_value' style='float:left;text-align:right'>10</label>"; }
			}
			protected override string Text
			{
				get { return _value.ToString(); }
			}
		}
	}
}