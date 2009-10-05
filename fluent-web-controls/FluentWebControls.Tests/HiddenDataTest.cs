using System;
using System.Linq.Expressions;

using FluentAssert;

using FluentWebControls.Extensions;
using FluentWebControls.Tools;

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
				return new HiddenData(ForId)
					{
						Text = Text
					};
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
					return NameUtility.GetPropertyName(expr).ToCamelCase();
				}
			}
			protected override string HtmlText
			{
				get { return "<input type='hidden' id='_value' name='_value' value='10'/>"; }
			}
			protected override string Text
			{
				get { return _value.ToString(); }
			}
		}
	}
}