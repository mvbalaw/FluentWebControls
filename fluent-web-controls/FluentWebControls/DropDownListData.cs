using System;
using System.Collections.Generic;
using System.Text;

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public class DropDownListData : ValidatableWebControlBase
	{
		private readonly IEnumerable<KeyValuePair<string, string>> _items;
		private KeyValuePair<string, string>? _formFieldToSetBeforeSubmitting;

		public DropDownListData(IEnumerable<KeyValuePair<string, string>> items)
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

		public LabelData Label { get; set; }
		public string SelectedValue { get; set; }
		public bool SubmitOnChange { get; set; }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (Label != null)
			{
				Label.ForId = IdWithPrefix;
				sb.Append(Label);
			}
			sb.Append("<select");
			sb.Append(IdWithPrefix.CreateQuotedAttribute("name"));
			sb.Append(IdWithPrefix.CreateQuotedAttribute("id"));
			sb.AppendFormat(BuildJqueryValidation(CssClass).CreateQuotedAttribute("class"));
			if (SubmitOnChange)
			{
				var v = _formFieldToSetBeforeSubmitting != null ? "setFormFieldAndSubmit(\"" + _formFieldToSetBeforeSubmitting.Value.Key + "\",\"" + _formFieldToSetBeforeSubmitting.Value.Value + "\", this);" : "this.form.submit();";
				sb.Append(v.CreateQuotedAttribute("onchange"));
			}
			sb.Append('>');
			if (Default != null)
			{
				WriteOption(sb, Default.Value, String.IsNullOrEmpty(SelectedValue));
			}
			foreach (var item in _items)
			{
				WriteOption(sb, item, item.Value == SelectedValue);
			}
			sb.Append("</select>");
			if (PropertyMetaData != null && PropertyMetaData.IsRequired)
			{
				sb.Append("<em>*</em>");
			}
			return sb.ToString();
		}

		private static void WriteOption(StringBuilder sb, KeyValuePair<string, string> item, bool selected)
		{
			sb.Append("<option");
			sb.Append(item.Value.CreateQuotedAttribute("value"));
			if (selected)
			{
				sb.Append("selected".CreateQuotedAttribute("selected"));
			}
			sb.Append('>');
			sb.Append(item.Key.EscapeForHtml());
			sb.Append("</option>");
		}
	}
}