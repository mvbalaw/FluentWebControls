using System;
using System.Text;

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public interface ITextAreaData
	{
		int Cols { get; }
		string CssClass { get; }
		string IdWithPrefix { get; }
		LabelData Label { get; }
		int Rows { get; }
		ValidatableWebControlBase.JQueryFieldValidationType ValidationType { get; }
		string Value { get; }
		string Width { get; }
		string TabIndex { get; }
	}

	public class TextAreaData : ValidatableWebControlBase, ITextAreaData
	{
		private readonly string _value;

		public TextAreaData(string value)
		{
			_value = value;
			CssClass = "textbox";
		}

		internal int Cols { private get; set; }
		internal string CssClass { private get; set; }
		internal LabelData Label { private get; set; }
		internal int Rows { private get; set; }
		internal JQueryFieldValidationType ValidationType { private get; set; }
		internal string Width { private get; set; }
		internal string TabIndex { private get; set; }

		int ITextAreaData.Cols
		{
			get { return Cols; }
		}
		string ITextAreaData.CssClass
		{
			get { return CssClass; }
		}
		LabelData ITextAreaData.Label
		{
			get { return Label; }
		}
		int ITextAreaData.Rows
		{
			get { return Rows; }
		}
		JQueryFieldValidationType ITextAreaData.ValidationType
		{
			get { return ValidationType; }
		}
		string ITextAreaData.Width
		{
			get { return Width; }
		}
		string ITextAreaData.Value
		{
			get { return _value; }
		}
		string ITextAreaData.IdWithPrefix
		{
			get { return IdWithPrefix; }
		}
		string ITextAreaData.TabIndex
		{
			get { return TabIndex; }
		}
		public override string ToString()
		{
			var sb = new StringBuilder();
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
				string value = "width:" + Width;
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

			if (!TabIndex.IsNullOrEmpty())
			{
				sb.Append(TabIndex.CreateQuotedAttribute("tabindex"));
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