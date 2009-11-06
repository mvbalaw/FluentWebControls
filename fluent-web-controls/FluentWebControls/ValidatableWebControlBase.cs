using System;
using System.Collections.Generic;
using System.Linq;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;

namespace FluentWebControls
{
	public abstract class ValidatableWebControlBase : WebControlBase
	{
		public IPropertyMetaData PropertyMetaData { get; set; }

		protected string BuildJqueryValidation(string cssClass)
		{
			var attributes = new List<string>();

			if (PropertyMetaData != null && PropertyMetaData.IsRequired)
			{
				// required must be first if specified
				attributes.Add(JQueryFieldValidationType.Required.Text);
			}

			if (cssClass != null)
			{
				attributes.Add(cssClass);
			}

			if (PropertyMetaData != null)
			{
				if (new[] { typeof(int), typeof(int?) }.Any(x => x == PropertyMetaData.ReturnType))
				{
					attributes.Add(JQueryFieldValidationType.Digits.Text);
				}
				if (PropertyMetaData.ValidationType != null)
				{
					attributes.Add(PropertyMetaData.ValidationType);
				}
				if (new[] { typeof(DateTime), typeof(DateTime?) }.Any(x => x == PropertyMetaData.ReturnType))
				{
					attributes.Add(JQueryFieldValidationType.Date.Text);
				}

				if (new[] { typeof(decimal), typeof(decimal?) }.Any(x => x == PropertyMetaData.ReturnType))
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