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
	public class AlignAttributeTest
	{
		[TestFixture]
		public class When_you_call_ToString_on_AlignAttribute
		{
			[Test]
			public void Should_return_the_text_with_align_set_to_the_Default_Text_selected()
			{
				AlignAttribute.Left.ToString().ShouldBeEqualTo(" align='left'");
			}
		}
	}
}