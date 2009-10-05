using System;
using System.Collections.Generic;
using System.Text;
using FluentWebControls.Interfaces;

namespace FluentWebControls
{
	public class DropDownListData : ValidatableWebControlBase
	{
		private readonly IEnumerable<KeyValuePair<string, string>> _items;
		private KeyValuePair<string, string>? _formFieldToSetBeforeSubmitting;

		public DropDownListData(IEnumerable<KeyValuePair<string, string>> items, IPropertyMetaData propertyMetaData)
			: base(propertyMetaData)
		{
			_items = items;
			CssClass = "ddlDetail";
		}

		public string CssClass { get; set; }
		public KeyValuePair<string, string>? Default { get; set; }

		public KeyValuePair<string, string> FormFieldToSetBeforeSubmitOnChange
		{
			set
			{
				SubmitOnChange = true;
				_formFieldToSetBeforeSubmitting = value;
			}
		}

		public string Id { get; set; }
		public LabelData Label { get; set; }
		public string SelectedValue { get; set; }
		public bool SubmitOnChange { get; set; }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (Label != null)
			{
				Label.ForId = Id;
				sb.Append(Label);
			}
			sb.Append("<select");
			sb.Append(CreateQuotedAttribute("name", Id));
			sb.Append(CreateQuotedAttribute("id", Id));
			sb.AppendFormat(CreateQuotedAttribute("class", BuildJqueryValidation(CssClass)));
			if (SubmitOnChange)
			{
				sb.Append(CreateQuotedAttribute("onchange", _formFieldToSetBeforeSubmitting != null ? "setFormFieldAndSubmit(\"" + _formFieldToSetBeforeSubmitting.Value.Key + "\",\"" + _formFieldToSetBeforeSubmitting.Value.Value + "\", this);" : "this.form.submit();"));
			}
			sb.Append('>');
			if (Default != null)
			{
				WriteOption(sb, Default.Value, String.IsNullOrEmpty(SelectedValue));
			}
			foreach (KeyValuePair<string, string> item in _items)
			{
				WriteOption(sb, item, item.Value == SelectedValue);
			}
			sb.Append("</select>");
			if (_propertyMetaData != null && _propertyMetaData.IsRequired)
			{
				sb.Append("<em>*</em>");
			}
			return sb.ToString();
		}

		private void WriteOption(StringBuilder sb, KeyValuePair<string, string> item, bool selected)
		{
			sb.Append("<option");
			sb.Append(CreateQuotedAttribute("value", item.Value));
			if (selected)
			{
				sb.Append(CreateQuotedAttribute("selected", "selected"));
			}
			sb.Append('>');
			sb.Append(EscapeForHtml(item.Key));
			sb.Append("</option>");
		}
	}
}