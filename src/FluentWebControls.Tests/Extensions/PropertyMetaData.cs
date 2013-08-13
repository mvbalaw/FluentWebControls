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

using FluentWebControls.Interfaces;

using Rhino.Mocks;

namespace FluentWebControls.Tests.Extensions
{
	public static class PropertyMetaDataMocker
	{
		public static IPropertyMetaData CreateStub(string name, bool isRequired, int? minLength, int? maxLength, int? minValue, int? maxValue, Type type)
		{
			var stub = MockRepository.GenerateStub<IPropertyMetaData>();
			stub.Expect(x => x.Name).Return(name).Repeat.Any();
			stub.Expect(x => x.IsRequired).Return(isRequired).Repeat.Any();
			stub.Expect(x => x.MinLength).Return(minLength).Repeat.Any();
			stub.Expect(x => x.MaxLength).Return(maxLength).Repeat.Any();
			stub.Expect(x => x.MinValue).Return(minValue).Repeat.Any();
			stub.Expect(x => x.MaxValue).Return(maxValue).Repeat.Any();
			stub.Expect(x => x.ReturnType).Return(type).Repeat.Any();
			return stub;
		}
	}
}