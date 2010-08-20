﻿using System.Collections.Generic;
using System.Text;

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public class RadioButtonListData : ValidatableWebControlBase
	{
		private readonly IEnumerable<KeyValuePair<string, string>> _items;

		public RadioButtonListData(IEnumerable<KeyValuePair<string, string>> items)
		{
			_items = items;
		}

		public string CssClass { get; set; }
		public LabelData Label { get; set; }
		public string SelectedValue { get; set; }
		public string TabIndex { get; set; }

		public override string ToString()
		{
			var sb = new StringBuilder();
			if (Label != null)
			{
				sb.Append(Label);
			}
			if (PropertyMetaData != null && PropertyMetaData.IsRequired)
			{
				sb.Append("<em>*</em>");
			}
			string id = ((IWebControl)this).Id;
			string idPrefix = ((IWebControl)this).IdPrefix;
			foreach (var item in _items)
			{
				bool isChecked = SelectedValue == item.Value;
				var checkbox = new RadioButtonData(isChecked)
					.WithValue(item.Value)
					.WithId(id)
					.WithIdPrefix(idPrefix);

				sb.Append("<div>");
				sb.Append(checkbox.ToString());
				sb.Append(item.Key);
				sb.Append("</div>");
			}
			return sb.ToString();
		}
	}
}