//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public class ComboSelectData : ValidatableWebControlBase
	{
		private readonly IEnumerable<KeyValuePair<string, string>> _items;

		public ComboSelectData(IEnumerable<KeyValuePair<string, string>> items)
		{
			_items = items;
			CssClass = new List<string>
			           {
				           "comboselect"
			           };
			SelectedValues = new List<string>();
			Size = 6;
		}

		public List<string> CssClass { get; set; }
		public LabelData Label { get; set; }
		public List<string> SelectedValues { get; private set; }
		public int Size { get; set; }
		public string TabIndex { get; set; }

		public override string ToString()
		{
			var sb = new StringBuilder();
			if (Label != null)
			{
				sb.Append(Label);
			}
			sb.Append("<select");
			sb.Append(NameWithPrefix.CreateQuotedAttribute("name"));
			sb.Append(IdWithPrefix.CreateQuotedAttribute("id"));
			sb.AppendFormat(BuildJqueryValidation(CssClass.Join(" ")).CreateQuotedAttribute("class"));
			sb.Append("multiple".CreateQuotedAttribute("multiple"));
			sb.Append(Size.CreateQuotedAttribute("size"));
			sb.Append(Data);
			if (!TabIndex.IsNullOrEmpty())
			{
				sb.Append(TabIndex.CreateQuotedAttribute("tabindex"));
			}
			sb.Append('>');
			foreach (var item in _items)
			{
				var itemValue = item;
				WriteOption(sb, item, SelectedValues.Any(value => itemValue.Value.Equals(value)));
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