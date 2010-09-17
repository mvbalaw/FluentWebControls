using System;
using System.Linq.Expressions;

using FluentWebControls.Interfaces;

namespace FluentWebControls.Mapping
{
	public class FreeTextMap<TDomain> : IFreeTextMap
	{
		private readonly Func<TDomain, string> _getValue;
		private readonly TDomain _item;
		private string _value;

		public FreeTextMap(TDomain item, string id, Func<TDomain, string> getValue)
		{
			Id = id;
			_item = item;
			_getValue = getValue;
		}

		public string Id { get; private set; }
		public IPropertyMetaData Validation { get; set; }

		public string Value
		{
			get
			{
				if (_value == null)
				{
					_value = _getValue(_item);
				}
				return _value;
			}
		}

		public FreeTextMap<TDomain> WithValidation(Expression<Func<TDomain, object>> getProperty)
		{
			Validation = Configuration.ValidationMetaDataFactory.GetFor(getProperty);
			return this;
		}
	}
}