using System;
using System.Text;

using FluentWebControls.Extensions;
using FluentWebControls.Validation;

namespace FluentWebControls
{
	public class TextBoxData : ValidatableWebControlBase
	{
		private const string DefaultCssClass = "textbox";
		private readonly string _value;
		private int? _maxValue;
		private int? _minValue;

		public TextBoxData(string value)
		{
			_value = value;
			CssClass = DefaultCssClass;
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
			if (PropertyMetaData != null)
			{
				if (PropertyMetaData.MaxLength > 0 && Width.IsNullOrEmpty())
				{
					Width = (PropertyMetaData.ValidationType == FieldValidationType.Digits ? 11 : 4) * PropertyMetaData.MaxLength.Value + "px";
				}
				if (PropertyMetaData.ReturnType == typeof(DateTime) ||
				    PropertyMetaData.ReturnType == typeof(DateTime?))
				{
					if (CssClass.IsNullOrEmpty() ||
					    CssClass == DefaultCssClass)
					{
						CssClass = "datebox";
					}
				}
			}

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

			if (PropertyMetaData != null)
			{
				if (PropertyMetaData.MinLength > 0)
				{
					sb.Append(PropertyMetaData.MinLength.CreateQuotedAttribute(JQueryFieldValidationType.MinLength.Text));
				}

				if (PropertyMetaData.MaxLength > 0)
				{
					sb.Append(PropertyMetaData.MaxLength.CreateQuotedAttribute(JQueryFieldValidationType.MaxLength.Text));
				}

				if (PropertyMetaData.MinValue > 0 || _minValue > 0)
				{
					var v = PropertyMetaData.MinValue ?? _minValue;
					sb.Append(v.CreateQuotedAttribute(JQueryFieldValidationType.MinValue.Text));
				}

				if (PropertyMetaData.MaxValue > 0 || _maxValue > 0)
				{
					var v = PropertyMetaData.MaxValue ?? _maxValue;
					sb.Append(v.CreateQuotedAttribute(JQueryFieldValidationType.MaxValue.Text));
				}
			}

			sb.Append(_value.CreateQuotedAttribute("value"));
			sb.Append("/>");
			if (PropertyMetaData != null)
			{
				if (PropertyMetaData.IsRequired)
				{
					sb.Append("<em>*</em>");
				}
			}
			return sb.ToString();
		}
	}
}