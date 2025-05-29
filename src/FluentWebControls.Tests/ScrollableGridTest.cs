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

using MvbaCore.Interfaces;
using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class ScrollableGridTest
	{
		[TestFixture]
		public class When_given_a_PagedList
		{
			private const string ActionName = "ActionName";
			private const string ControllerName = "ControllerName";

			[Test]
			public void Should_throw_an_exception_for_null_pagedList()
			{
				Assert.Throws<NullReferenceException>(() => ScrollableGrid.For((IPagedList<TestData.Item>)null, null, ControllerName, ActionName));
			}
		}
	}
}