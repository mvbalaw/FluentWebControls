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
using System.Linq.Expressions;

using FluentWebControls.Interfaces;

namespace FluentWebControls.Mapping
{
	public class FreeTextMap<TDomain> : IFreeTextMap
	{
		private readonly string _value;

		public FreeTextMap(TDomain item, string id, Func<TDomain, string> getValue) : this(id, getValue(item))
		{
		}

		public FreeTextMap(string id, string value)
		{
			Id = id;
			_value = value;
		}

		public string Id { get; }
		public IPropertyMetaData Validation { get; set; }

		public string Value => _value;

		object IModelMap.GetValueForModel()
		{
			return Value;
		}

		public FreeTextMap<TDomain> WithValidation(Expression<Func<TDomain, object>> getProperty)
		{
			Validation = Configuration.ValidationMetaDataFactory.GetFor(getProperty);
			return this;
		}

		public FreeTextMap<TDomain> WithValidation(Action<IFieldValidationBuilder> updateValidation)
		{
			var propertyMetaDataWrapper = new PropertyMetaDataWrapper(Validation);
			updateValidation(propertyMetaDataWrapper);
			Validation = propertyMetaDataWrapper;
			return this;
		}
	}
}