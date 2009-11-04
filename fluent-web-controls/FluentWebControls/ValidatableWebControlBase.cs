using System;
using System.Collections.Generic;
using System.Linq;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;

namespace FluentWebControls
{
	public abstract class ValidatableWebControlBase : WebControlBase
	{
		protected readonly IPropertyMetaData _propertyMetaData;

		protected ValidatableWebControlBase(IPropertyMetaData propertyMetaData)
		{
			_propertyMetaData = propertyMetaData;
		}

		protected string BuildJqueryValidation(string cssClass)
		{
			var attributes = new List<string>();

			if (_propertyMetaData != null && _propertyMetaData.IsRequired)
			{
				// required must be first if specified
				attributes.Add(JQueryFieldValidationType.Required.Text);
			}

			if (cssClass != null)
			{
				attributes.Add(cssClass);
			}

			if (_propertyMetaData != null)
			{
				if (new[] { typeof(int), typeof(int?) }.Any(x => x == _propertyMetaData.ReturnType))
				{
					attributes.Add(JQueryFieldValidationType.Digits.Text);
				}
				if (_propertyMetaData.ValidationType != null)
				{
					attributes.Add(_propertyMetaData.ValidationType);
				}
				if (new[] { typeof(DateTime), typeof(DateTime?) }.Any(x => x == _propertyMetaData.ReturnType))
				{
					attributes.Add(JQueryFieldValidationType.Date.Text);
				}

				if (new[] { typeof(decimal), typeof(decimal?) }.Any(x => x == _propertyMetaData.ReturnType))
				{
					attributes.Add(JQueryFieldValidationType.Number.Text);
				}
			}
			return attributes.ToArray().Join(" ");
		}

		public class JQueryFieldValidationType
		{
			public static JQueryFieldValidationType Accept = new JQueryFieldValidationType("accept");
			public static JQueryFieldValidationType CreditCard = new JQueryFieldValidationType("creditcard");
			public static JQueryFieldValidationType Date = new JQueryFieldValidationType("date");
			public static JQueryFieldValidationType Digits = new JQueryFieldValidationType("digits"); // [0-9] only
			public static JQueryFieldValidationType Email = new JQueryFieldValidationType("email");
			public static JQueryFieldValidationType EqualTo = new JQueryFieldValidationType("equalTo");
			public static JQueryFieldValidationType MaxLength = new JQueryFieldValidationType("maxlength");
			public static JQueryFieldValidationType MaxValue = new JQueryFieldValidationType("max");
			public static JQueryFieldValidationType MinLength = new JQueryFieldValidationType("minlength");
			public static JQueryFieldValidationType MinValue = new JQueryFieldValidationType("min");
			public static JQueryFieldValidationType Number = new JQueryFieldValidationType("number"); // may have decimals
			public static JQueryFieldValidationType RangeLength = new JQueryFieldValidationType("rangeLength");
			public static JQueryFieldValidationType RangeValue = new JQueryFieldValidationType("range");
			public static JQueryFieldValidationType Remote = new JQueryFieldValidationType("remote");
			public static JQueryFieldValidationType Required = new JQueryFieldValidationType("required");
			public static JQueryFieldValidationType Url = new JQueryFieldValidationType("url");

			internal JQueryFieldValidationType(string text)
			{
				Text = text;
			}

			public string Text { get; private set; }
		}
	}
}