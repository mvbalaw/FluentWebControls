using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentWebControls.Interfaces
{
	public interface IBusinessObjectPropertyMetaDataFactory
	{
		IPropertyMetaData GetFor<T>(string propertyName);
		IPropertyMetaData GetFor<TReturn>(Expression<Func<TReturn>> property);
		IPropertyMetaData GetFor<T, TReturn>(Expression<Func<T, TReturn>> property);
		IEnumerable<IPropertyMetaData> GetProperties<T>();
	}
}