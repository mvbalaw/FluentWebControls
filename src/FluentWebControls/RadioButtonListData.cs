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
			UseItemSeparator = true;
		}

		public string CssClass { get; set; }
		public LabelData Label { get; set; }
		public string SelectedValue { get; set; }
		public string TabIndex { get; set; }
		public bool UseItemSeparator { get; set; }

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
			var id = ((IWebControl)this).Id;
			var idPrefix = ((IWebControl)this).IdPrefix;
			var namePrefix = ((IWebControl)this).NamePrefix;
			foreach (var item in _items)
			{
				var isChecked = SelectedValue == item.Value;
				var checkbox = new RadioButtonData(isChecked)
					.WithValue(item.Value)
					.WithId(id)
					.WithIdPrefix(idPrefix)
					.WithNamePrefix(namePrefix);
				if (!CssClass.IsNullOrEmpty())
				{
					checkbox.WithCssClass(CssClass);
				}
				if (UseItemSeparator)
				{
					sb.Append("<div>");
				}
				sb.Append(checkbox);
				sb.Append(item.Key);
				if (UseItemSeparator)
				{
					sb.Append("</div>");
				}
			}
			return sb.ToString();
		}
	}
}