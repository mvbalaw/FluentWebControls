using System;

using FluentWebControls.Interfaces;

using Rhino.Mocks;

namespace FluentWebControls.Tests.Extensions
{
	public static class PropertyMetaDataMocker
	{
		public static IPropertyMetaData CreateStub(string name, bool isRequired, int? minLength, int? maxLength, int? minValue, int? maxValue, Type type)
		{
			IPropertyMetaData stub = MockRepository.GenerateStub<IPropertyMetaData>();
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