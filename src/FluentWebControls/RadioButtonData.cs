//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using System;
using System.Text;

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public interface IRadioButtonData
	{
		bool Checked { get; }
		string CssClass { get; }
		LabelData Label { get; }
		AlignAttribute LabelAlignAttribute { get; }
		string TabIndex { get; }
		string Value { get; }
	}

	public class RadioButtonData : WebControlBase, IRadioButtonData
	{
		public RadioButtonData(bool isChecked)
		{
			Checked = isChecked;
			LabelAlignAttribute = AlignAttribute.Right;
		}

		internal bool Checked { private get; set; }
		internal string CssClass { private get; set; }
		internal LabelData Label { private get; set; }
		internal AlignAttribute LabelAlignAttribute { private get; set; }
		internal string TabIndex { private get; set; }
		internal string Value { private get; set; }

		LabelData IRadioButtonData.Label => Label;

		AlignAttribute IRadioButtonData.LabelAlignAttribute => LabelAlignAttribute;

		bool IRadioButtonData.Checked => Checked;

		string IRadioButtonData.Value => Value;

		string IRadioButtonData.TabIndex => TabIndex;

		string IRadioButtonData.CssClass => CssClass;

		private void AppendLabel(StringBuilder stringBuilder)
		{
			if (Label == null)
			{
				return;
			}
			Label.ForId = IdWithPrefix;
			if (LabelAlignAttribute == AlignAttribute.Left)
			{
				stringBuilder.Insert(0, Label);
			}
			else
			{
				var blankLabel = new LabelData
				                 {
					                 Text = "&nbsp;"
				                 };
				stringBuilder.Insert(0, blankLabel);
				Label.Style = "";
				if (!string.IsNullOrEmpty(Label.Text) && Label.Text.EndsWith(":"))
				{
					Label.Text = Label.Text.Replace(":", "");
				}
				stringBuilder.Append(Label);
			}
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append("<input");
			sb.Append("radio".CreateQuotedAttribute("type"));
			if (!IdWithPrefix.IsNullOrEmpty())
			{
				sb.Append(IdWithPrefix.CreateQuotedAttribute("id"));
				sb.Append(NameWithPrefix.CreateQuotedAttribute("name"));
			}
			sb.Append(Data);
			if (Checked)
			{
				sb.Append("checked".CreateQuotedAttribute("checked"));
			}
			if (Value == null)
			{
				sb.Append("true".CreateQuotedAttribute("value"));
			}
			else
			{
				sb.Append(Value.CreateQuotedAttribute("value"));
			}
			if (!TabIndex.IsNullOrEmpty())
			{
				sb.Append(TabIndex.CreateQuotedAttribute("tabindex"));
			}
			if (!CssClass.IsNullOrEmpty())
			{
				sb.Append(CssClass.CreateQuotedAttribute("class"));
			}
			sb.Append("/>");
			AppendLabel(sb);
			return sb.ToString();
		}
	}
}