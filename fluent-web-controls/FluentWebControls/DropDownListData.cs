using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentWebControls.Extensions;
using FluentWebControls.Tools;

namespace FluentWebControls
{
	public class DropDownListData : ValidatableWebControlBase
	{
		private KeyValuePair<string, string>? _formFieldToSetBeforeSubmitting;
		private IEnumerable<KeyValuePair<string, string>> _items;

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
		public MethodCallData SlaveDataSource { get; set; }
		public string SlaveId { get; set; }
		public bool SubmitOnChange { get; set; }

		public void Remove(string value)
		{
			_items = _items.Where(x => x.Value != value);
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
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
				string v = _formFieldToSetBeforeSubmitting != null ? "setFormFieldAndSubmit(\"" + _formFieldToSetBeforeSubmitting.Value.Key + "\",\"" + _formFieldToSetBeforeSubmitting.Value.Value + "\", this);" : "this.form.submit();";
				sb.Append(v.CreateQuotedAttribute("onchange"));
			}
			else if (SlaveId != null)
			{
				string secondaryDdlScript = String.Format("UpdateSecondDropDown(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\");", IdWithPrefix, SlaveId.ToCamelCase(), NameUtility.GetControllerName(SlaveDataSource.ClassName), SlaveDataSource.MethodName, SlaveDataSource.ParameterValues.First().Key);
				sb.Append(secondaryDdlScript.CreateQuotedAttribute("onchange"));
			}
			sb.Append('>');
			if (Default != null)
			{
				WriteOption(sb, Default.Value, String.IsNullOrEmpty(SelectedValue));
				_items = _items.Where(x => x.Value != Default.Value.Value);
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
			if (SlaveId != null)
			{
				var script = new StringBuilder();
				script.Append(@"
<script language=""JavaScript"">
	function UpdateSecondDropDown(parentId, childId, controller, action, variable)
	{
		$(""select[id$='""+childId+""']"").html(""""); 
		var parentValue = $(""select[id$='""+parentId+""'] > option[selected]"").attr(""value"");
		if (parentValue != """") 
		{ 
			$.getJSON('/'+controller+'/'+action+'?'+variable+'='+ parentValue, function(valuesB) 
				{ 
					$.each(valuesB,function()
						{
							$(""select[id$='""+childId+""']"").append($(""<option></option>"").val(this['ID']).html(this['Name']));
						}
					);
				}
			);
		}
	}
</script>");
				sb.Append(script);
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