using System;
using System.Text;

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public class CheckBoxData : WebControlBase
	{
		public CheckBoxData(bool isChecked)
		{
			Checked = isChecked;
			LabelAlignAttribute = AlignAttribute.Right;
		}

		public bool Checked { get; set; }
		public LabelData Label { get; set; }
		public AlignAttribute LabelAlignAttribute { get; set; }

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
				LabelData blankLabel = new LabelData
					{
						Text = "&nbsp;"
					};
				stringBuilder.Insert(0, blankLabel);
				Label.Style = "";
				if (!String.IsNullOrEmpty(Label.Text) && Label.Text.EndsWith(":"))
				{
					Label.Text = Label.Text.Replace(":", "");
				}
				stringBuilder.Append(Label);
			}
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<input");
			sb.Append("checkbox".CreateQuotedAttribute("type"));
			if (!IdWithPrefix.IsNullOrEmpty())
			{
				sb.Append(IdWithPrefix.CreateQuotedAttribute("id"));
				sb.Append(IdWithPrefix.CreateQuotedAttribute("name"));
			}
			if (Checked)
			{
				sb.Append("checked".CreateQuotedAttribute("checked"));
			}
			if (Value != null)
			{
				sb.Append(Value.CreateQuotedAttribute("value"));
			}
			sb.Append("/>");
			AppendLabel(sb);
			return sb.ToString();
		}

		public string Value { get; set; }
	}
}