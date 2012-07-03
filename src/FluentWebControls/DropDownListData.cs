using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentWebControls.Extensions;
using MvbaCore;

namespace FluentWebControls
{
	public interface IDropDownListData : IWebControl
	{
		string CssClass { get; }
		KeyValuePair<string, string>? Default { get; }
		KeyValuePair<string, string>? FormFieldToSetBeforeSubmitOnChange { get; }
		string IdWithPrefix { get; }
		LabelData Label { get; }
		IEnumerable<KeyValuePair<string, string>> ListItems { get; }
		List<KeyValuePair<string, string>> Attributes { get; }
		bool ReadOnly { get; }
		string SelectedValue { get; }
		string TabIndex { get; }
		bool SubmitOnChange { get; }
	}

	public class DropDownListData : ValidatableWebControlBase, IDropDownListData
	{
		private KeyValuePair<string, string>? _formFieldToSetBeforeSubmitting;
		private IEnumerable<KeyValuePair<string, string>> _items;
		private string _selectedValue;

		public DropDownListData(IEnumerable<KeyValuePair<string, string>> items)
		{
			_items = items;
			Attributes = new List<KeyValuePair<string, string>>();
			CssClass = "ddlDetail";
		}

		internal KeyValuePair<string, string>? Default { private get; set; }

		internal KeyValuePair<string, string> FormFieldToSetBeforeSubmitOnChange
		{
			set
			{
				SubmitOnChange = true;
				_formFieldToSetBeforeSubmitting = value;
			}
		}

		internal LabelData Label { private get; set; }

		internal List<KeyValuePair<string, string>> Attributes { get; private set; }

		internal bool ReadOnly { private get; set; }
		internal string TabIndex { private get; set; }

		internal string SelectedValue
		{
			private get
			{
				if (_selectedValue != null)
				{
					return _selectedValue;
				}
				if (Default != null)
				{
					return Default.Value.Value;
				}

				if (_items.Any())
				{
					_selectedValue = _items.First().Value;
					return _selectedValue;
				}
				return null;
			}
			set { _selectedValue = value; }
		}

		public MethodCallData SlaveDataSource { get; set; }
		public string SlaveId { get; set; }
		internal bool SubmitOnChange { private get; set; }

		#region IDropDownListData Members

		bool IDropDownListData.SubmitOnChange
		{
			get { return SubmitOnChange; }
		}

		string IDropDownListData.IdWithPrefix
		{
			get { return IdWithPrefix; }
		}

		string IDropDownListData.TabIndex
		{
			get { return TabIndex; }
		}

		IEnumerable<KeyValuePair<string, string>> IDropDownListData.ListItems
		{
			get { return _items; }
		}

		List<KeyValuePair<string, string>> IDropDownListData.Attributes
		{
			get { return Attributes; }
		}

		public string CssClass { get; set; }

		KeyValuePair<string, string>? IDropDownListData.Default
		{
			get { return Default; }
		}

		KeyValuePair<string, string>? IDropDownListData.FormFieldToSetBeforeSubmitOnChange
		{
			get { return _formFieldToSetBeforeSubmitting; }
		}

		LabelData IDropDownListData.Label
		{
			get { return Label; }
		}

		string IDropDownListData.SelectedValue
		{
			get { return SelectedValue; }
		}

		bool IDropDownListData.ReadOnly
		{
			get { return ReadOnly; }
		}

		#endregion

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
			string idWithPrefix = IdWithPrefix;
			if (ReadOnly)
			{
				idWithPrefix += "_readonly";
			}
			sb.Append(NameWithPrefix.CreateQuotedAttribute("name"));
			sb.Append(idWithPrefix.CreateQuotedAttribute("id"));
			sb.AppendFormat(BuildJqueryValidation(CssClass).CreateQuotedAttribute("class"));
			if (SubmitOnChange)
			{
				string v = _formFieldToSetBeforeSubmitting != null
				           	? "setFormFieldAndSubmit(\"" + _formFieldToSetBeforeSubmitting.Value.Key + "\",\"" +
				           	  _formFieldToSetBeforeSubmitting.Value.Value + "\", this);"
				           	: "this.form.submit();";
				sb.Append(v.CreateQuotedAttribute("onchange"));
			}
			else if (SlaveId != null)
			{
				string secondaryDdlScript = String.Format("UpdateSecondDropDown(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\");",
				                                          IdWithPrefix, SlaveId.ToCamelCase(),
				                                          Reflection.GetControllerName(SlaveDataSource.ClassName),
				                                          SlaveDataSource.MethodName, SlaveDataSource.ParameterValues.First().Key);
				sb.Append(secondaryDdlScript.CreateQuotedAttribute("onchange"));
			}
			if (ReadOnly)
			{
				sb.Append("disabled".CreateQuotedAttribute("disabled"));
			}
			if (!TabIndex.IsNullOrEmpty())
			{
				sb.Append(TabIndex.CreateQuotedAttribute("tabindex"));
			}
			if (Attributes.Any())
			{
				foreach (var keyValuePair in Attributes)
				{
					sb.Append(keyValuePair.Key.CreateQuotedAttribute(keyValuePair.Value));
				}
			}
			sb.Append('>');
			if (Default != null)
			{
				WriteOption(sb, Default.Value, SelectedValue == null || SelectedValue == Default.Value.Value);
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
			if (ReadOnly)
			{
				var hidden = new HiddenData()
					.WithValue(SelectedValue)
					.WithId(((IWebControl) this).Id)
					.WithIdPrefix(((IWebControl) this).IdPrefix)
					.WithNamePrefix(((IWebControl) this).NamePrefix);

				sb.Append(hidden.ToString());
			}
			if (SlaveId != null)
			{
				var script = new StringBuilder();
				script.Append(
					@"
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