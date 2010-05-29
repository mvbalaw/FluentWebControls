using System;
using System.Linq.Expressions;

using FluentAssert;

using FluentWebControls.Extensions;

using MvbaCore;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class SpanDataTest
	{
		public abstract class SpanDataTestBase
		{
			protected abstract string ForId { get; }
			protected abstract string HtmlText { get; }
			protected abstract string Text { get; }

			private SpanData GetSpanData()
			{
				return new SpanData(Text).WithId(ForId);
			}

			[Test]
			public void Should_return_HTML_code_representing_a_span_with_its_value_embedded_in_it()
			{
				GetSpanData().ToString().ShouldBeEqualTo(HtmlText);
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_span_field : SpanDataTestBase
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
				get { return "<span id='_value'>10</span>"; }
			}
			protected override string Text
			{
				get { return _value.ToString(); }
			}
		}
	}
}