using System;
using System.Text;
using FluentWebControls.Interfaces;

namespace FluentWebControls
{
	public class TextAreaData : ValidatableWebControlBase
	{
		private readonly string _value;

		public TextAreaData(string value, IPropertyMetaData propertyMetaData)
			: base(propertyMetaData)
		{
			_value = value;
			CssClass = "textbox";
		}

		public int Cols { private get; set; }
		public string CssClass { private get; set; }
		public string Id { private get; set; }
		public LabelData Label { get; set; }
		public int Rows { private get; set; }
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
			sb.Append("<textarea");
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
			}

			if (Rows > 0)
			{
				sb.Append(CreateQuotedAttribute("Rows", Rows));
			}
			if (Cols > 0)
			{
				sb.Append(CreateQuotedAttribute("Cols", Cols));
			}

			sb.Append(String.Format(">{0}</textarea>", EscapeForHtml(_value)));
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