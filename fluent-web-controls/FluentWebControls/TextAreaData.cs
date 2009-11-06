using System;
using System.Text;

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public class TextAreaData : ValidatableWebControlBase
	{
		private readonly string _value;

		public TextAreaData(string value)
		{
			_value = value;
			CssClass = "textbox";
		}

		public int Cols { private get; set; }
		public string CssClass { private get; set; }
		public LabelData Label { get; set; }
		public int Rows { private get; set; }
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
			sb.Append("<textarea");
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
			}

			if (Rows > 0)
			{
				sb.Append(Rows.CreateQuotedAttribute("Rows"));
			}
			if (Cols > 0)
			{
				sb.Append(Cols.CreateQuotedAttribute("Cols"));
			}

			sb.Append(String.Format(">{0}</textarea>", _value.EscapeForHtml()));
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