using System;
using System.Text;
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
					Width = (propertyMetaData.ValidationType == FieldValidationType.Digits ? 11 : 4)*propertyMetaData.MaxLength.Value + "px";
				}
				if (propertyMetaData.ReturnType == typeof (DateTime) || propertyMetaData.ReturnType == typeof (DateTime?))
				{
					CssClass = "datebox";
				}
			}
		}

		public string CssClass { private get; set; }
		public string Id { get; set; }
		public LabelData Label { get; set; }

		internal int MaxValue
		{
			set { _maxValue = value > 0 ? value : (int?) null; }
		}

		internal int MinValue
		{
			set { _minValue = value >= 0 ? value : (int?) null; }
		}

		public JQueryFieldValidationType ValidationType { get; set; }
		public string Width { private get; set; }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (Label != null)
			{
				Label.ForId = Id;
				sb.Append(Label);
			}
			sb.Append("<input");
			sb.Append(CreateQuotedAttribute("type", "text"));
			if (Id != null)
			{
				sb.Append(CreateQuotedAttribute("id", Id));
				sb.Append(CreateQuotedAttribute("name", Id));
			}
			if (Width != null)
			{
				sb.Append(CreateQuotedAttribute("style", "width:" + Width));
			}
			sb.Append(CreateQuotedAttribute("class", BuildJqueryValidation(CssClass)));

			if (_propertyMetaData != null)
			{
				if (_propertyMetaData.MinLength > 0)
				{
					sb.Append(CreateQuotedAttribute(JQueryFieldValidationType.MinLength.Text, _propertyMetaData.MinLength));
				}

				if (_propertyMetaData.MaxLength > 0)
				{
					sb.Append(CreateQuotedAttribute(JQueryFieldValidationType.MaxLength.Text, _propertyMetaData.MaxLength));
				}

				if (_propertyMetaData.MinValue > 0 || _minValue > 0)
				{
					sb.Append(CreateQuotedAttribute(JQueryFieldValidationType.MinValue.Text, _propertyMetaData.MinValue ?? _minValue));
				}

				if (_propertyMetaData.MaxValue > 0 || _maxValue > 0)
				{
					sb.Append(CreateQuotedAttribute(JQueryFieldValidationType.MaxValue.Text, _propertyMetaData.MaxValue ?? _maxValue));
				}
			}

			sb.Append(CreateQuotedAttribute("value", _value));
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