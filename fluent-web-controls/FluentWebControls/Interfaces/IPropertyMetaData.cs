using System;
using System.Collections.Generic;
using System.Reflection;

namespace FluentWebControls.Interfaces
{
	public interface IPropertyMetaData
	{
		IList<string> DependsOnProperty { get; }
		bool IsRequired { get; }
		int? MaxLength { get; }
		int? MaxValue { get; }
		int? MinLength { get; }
		int? MinValue { get; }
		string Name { get; }
		PropertyInfo PropertyInfo { get; }
		Type ReturnType { get; }
		string ValidationType { get; }
		void Combine(IPropertyMetaData parentMetaData);
	}
}