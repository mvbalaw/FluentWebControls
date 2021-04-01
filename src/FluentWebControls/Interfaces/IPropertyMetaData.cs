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

	public interface IFieldValidationBuilder
	{
		IFieldValidationBuilder MaxLength(int? value);
		IFieldValidationBuilder MaxValue(int? value);
		IFieldValidationBuilder MinLength(int? value);
		IFieldValidationBuilder MinValue(int? value);
		IFieldValidationBuilder Optional();
		IFieldValidationBuilder Required();
	}

	public class PropertyMetaDataWrapper : IPropertyMetaData, IFieldValidationBuilder
	{
		private readonly IList<string> _dependsOnProperty;
		private readonly IPropertyMetaData _propertyMetaData;
		private bool _isRequired;
		private int? _maxLength;
		private int? _maxValue;
		private int? _minLength;
		private int? _minValue;
		private string _name;
		private PropertyInfo _propertyInfo;
		private Type _returnType;
		private string _validationType;

		public PropertyMetaDataWrapper(IPropertyMetaData propertyMetaData)
		{
			_propertyMetaData = propertyMetaData;
			if (propertyMetaData != null)
			{
				_isRequired = propertyMetaData.IsRequired;
				_maxLength = propertyMetaData.MaxLength;
				_maxValue = propertyMetaData.MaxValue;
				_minLength = propertyMetaData.MinLength;
				_maxValue = propertyMetaData.MaxValue;
				_dependsOnProperty = _propertyMetaData.DependsOnProperty;
				_name = _propertyMetaData.Name;
				_propertyInfo = _propertyMetaData.PropertyInfo;
				_returnType = propertyMetaData.ReturnType;
				_validationType = propertyMetaData.ValidationType;
			}
			else
			{
				_dependsOnProperty = new List<string>();
			}
		}

		public IFieldValidationBuilder MaxLength(int? value)
		{
			_maxLength = value;
			return this;
		}

		public IFieldValidationBuilder MinLength(int? value)
		{
			_minLength = value;
			return this;
		}

		public IFieldValidationBuilder MaxValue(int? value)
		{
			_maxValue = value;
			return this;
		}

		public IFieldValidationBuilder MinValue(int? value)
		{
			_minValue = value;
			return this;
		}

		public IFieldValidationBuilder Required()
		{
			_isRequired = true;
			return this;
		}

		public IFieldValidationBuilder Optional()
		{
			_isRequired = false;
			return this;
		}

		IList<string> IPropertyMetaData.DependsOnProperty => _dependsOnProperty;

		bool IPropertyMetaData.IsRequired => _isRequired;

		int? IPropertyMetaData.MaxLength => _maxLength;

		int? IPropertyMetaData.MaxValue => _maxValue;

		int? IPropertyMetaData.MinLength => _minLength;

		int? IPropertyMetaData.MinValue => _minValue;

		string IPropertyMetaData.Name => _name;

		PropertyInfo IPropertyMetaData.PropertyInfo => _propertyInfo;

		Type IPropertyMetaData.ReturnType => _returnType;

		string IPropertyMetaData.ValidationType => _validationType;

		void IPropertyMetaData.Combine(IPropertyMetaData parentMetaData)
		{
			if (_propertyMetaData != null)
			{
				_propertyMetaData.Combine(parentMetaData);
				_isRequired |= _propertyMetaData.IsRequired;
			}
			else
			{
				_isRequired |= parentMetaData.IsRequired;
			}
		}

		public IFieldValidationBuilder Name(string name)
		{
			_name = name;
			return this;
		}

		public IFieldValidationBuilder ReturnType(Type returnType)
		{
			_returnType = returnType;
			return this;
		}

		public IFieldValidationBuilder ReturnType(PropertyInfo propertyInfo)
		{
			_propertyInfo = propertyInfo;
			return this;
		}

		public IFieldValidationBuilder ValidationType(string validationType)
		{
			_validationType = validationType;
			return this;
		}
	}
}