using System;
using System.Linq.Expressions;

using FluentWebControls.Interfaces;

namespace FluentWebControls.Mapping
{
	public class FreeTextMap<TDomain> : IFreeTextMap
	{
		private readonly Func<TDomain, string> _getValue;
		private readonly TDomain _item;
		private bool _haveValue;
		private string _value;

		public FreeTextMap(TDomain item, string id, Func<TDomain, string> getValue)
		{
			Id = id;
			_item = item;
			_getValue = getValue;
		}

		public FreeTextMap(string id, string value)
		{
			Id = id;
			_haveValue = true;
			_value = value;
		}

		public string Id { get; private set; }
		public IPropertyMetaData Validation { get; set; }

		public string Value
		{
			get
			{
				if (_haveValue)
				{
					return _value;
				}
				_haveValue = true;
				return _value = _getValue(_item);
			}
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

		object IModelMap.GetValueForModel()
		{
			return Value;
		}
	}
}