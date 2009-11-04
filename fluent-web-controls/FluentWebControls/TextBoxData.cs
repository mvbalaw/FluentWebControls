using System;
using System.Text;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;
using FluentWebControls.Validation;

namespace FluentWebControls
{
	public class TextBoxData : ValidatableWebControlBase
	{
		private readonly string _value;
		private int? _maxValue;
		private int? _minValue;

		public TextBoxData(string value, IPropertyMetaData propertyMetaData)
			: base(propertyMetaData)
		{
			_value = value;
			CssClass = "textbox";

			if (propertyMetaData != null)
			{
				if (propertyMetaData.MaxLength > 0)
				{
					Width = (propertyMetaData.ValidationType == FieldValidationType.Digits ? 11 : 4) * propertyMetaData.MaxLength.Value + "px";
				}
				if (propertyMetaData.ReturnType == typeof(DateTime) || propertyMetaData.ReturnType == typeof(DateTime?))
				{
					CssClass = "datebox";
				}
			}
		}

		public string CssClass { private get; set; }
		public LabelData Label { get; set; }

		internal int MaxValue
		{
			set { _maxValue = value > 0 ? value : (int?)null; }
		}

		internal int MinValue
		{
			set { _minValue = value >= 0 ? value : (int?)null; }
		}

		public JQueryFieldValidationType ValidationType { get; set; }
		public string Width { private get; set; }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (Label != null)
			{
				Label.ForId = IdWithPrefix;
				sb.Append(Label);
			}
			sb.Append("<input");
			sb.Append("text".CreateQuotedAttribute("type"));
			if (!IdWithPrefix.IsNullOrEmpty())
			{
				sb.Append(IdWithPrefix.CreateQuotedAttribute("id"));
				sb.Append(IdWithPrefix.CreateQuotedAttribute("name"));
			}
			if (Width != null)
			{
				var value = "width:" + Width;
				sb.Append(value.CreateQuotedAttribute("style"));
			}
			sb.Append(BuildJqueryValidation(CssClass).CreateQuotedAttribute("class"));

			if (_propertyMetaData != null)
			{
				if (_propertyMetaData.MinLength > 0)
				{
					sb.Append(_propertyMetaData.MinLength.CreateQuotedAttribute(JQueryFieldValidationType.MinLength.Text));
				}

				if (_propertyMetaData.MaxLength > 0)
				{
					sb.Append(_propertyMetaData.MaxLength.CreateQuotedAttribute(JQueryFieldValidationType.MaxLength.Text));
				}

				if (_propertyMetaData.MinValue > 0 || _minValue > 0)
				{
					var v = _propertyMetaData.MinValue ?? _minValue;
					sb.Append(v.CreateQuotedAttribute(JQueryFieldValidationType.MinValue.Text));
				}

				if (_propertyMetaData.MaxValue > 0 || _maxValue > 0)
				{
					var v = _propertyMetaData.MaxValue ?? _maxValue;
					sb.Append(v.CreateQuotedAttribute(JQueryFieldValidationType.MaxValue.Text));
				}
			}

			sb.Append(_value.CreateQuotedAttribute("value"));
			sb.Append("/>");
			if (_propertyMetaData != null)
			{
				if (_propertyMetaData.IsRequired)
				{
					sb.Append("<em>*</em>");
				}
			}
			return sb.ToString();
		}
	}
}