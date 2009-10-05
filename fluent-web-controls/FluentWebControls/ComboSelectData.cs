using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentWebControls.Interfaces;

namespace FluentWebControls
{
	public class ComboSelectData : ValidatableWebControlBase
	{
		private readonly IEnumerable<KeyValuePair<string, string>> _items;

		public ComboSelectData(IEnumerable<KeyValuePair<string, string>> items, IPropertyMetaData propertyMetaData)
			: base(propertyMetaData)
		{
			_items = items;
			CssClass = "comboselect";
			SelectedValues = new List<string>();
			Size = 6;
		}

		public string CssClass { get; set; }
		public string Id { get; set; }
		public LabelData Label { get; set; }
		public List<string> SelectedValues { get; private set; }
		public int Size { get; set; }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (Label != null)
			{
				sb.Append(Label);
			}
			sb.Append("<select");
			sb.Append(CreateQuotedAttribute("name", Id));
			sb.Append(CreateQuotedAttribute("id", Id));
			sb.AppendFormat(CreateQuotedAttribute("class", BuildJqueryValidation(CssClass)));
			sb.Append(CreateQuotedAttribute("multiple", "multiple"));
			sb.Append(CreateQuotedAttribute("size", Size));
			sb.Append('>');
			foreach (KeyValuePair<string, string> item in _items)
			{
				KeyValuePair<string, string> itemValue = item;
				WriteOption(sb, item, SelectedValues.Any(value => itemValue.Value.Equals(value)));
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