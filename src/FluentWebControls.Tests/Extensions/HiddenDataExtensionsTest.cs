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
	public class HiddenDataExtensionsTest
	{
		public abstract class HiddenDataExtensionsTestBase
		{
			protected HiddenData _hiddenData;

			[SetUp]
			public void BeforeEachTest()
			{
				_hiddenData = new HiddenData().WithId("Id");
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Text : HiddenDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_HiddenData_With_Text_initialized()
			{
				const string text = "Text";
				var hiddenData = _hiddenData.WithValue(text);
				Assert.AreSame(_hiddenData, hiddenData);
				_hiddenData.ToString().ParseHtmlTag()["value"].ShouldBeEqualTo(text);
				hiddenData.ToString().Contains(text).ShouldBeTrue();
			}
		}
	}
}