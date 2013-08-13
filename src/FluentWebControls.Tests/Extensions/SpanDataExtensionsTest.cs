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

using FluentWebControls.Extensions;

using NUnit.Framework;

namespace FluentWebControls.Tests.Extensions
{
	public class SpanDataExtensionsTest
	{
		public abstract class SpanDataExtensionsTestBase
		{
			protected SpanData SpanData;

			[SetUp]
			public void BeforeEachTest()
			{
				SpanData = new SpanData("value");
			}
		}

		[TestFixture]
		public class When_asked_to_assign_CssClass : SpanDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_SpanData_With_CssClass_initialized()
			{
				const string cssClass = "Link";
				var span = SpanData.WithCssClass(cssClass);
				Assert.AreSame(SpanData, span);
				span.ToString().ParseHtmlTag()["class"].ShouldBeEqualTo(cssClass);
				SpanData.ToString().Contains(cssClass).ShouldBeTrue();
			}
		}
	}
}