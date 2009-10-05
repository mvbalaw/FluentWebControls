using System;
using System.Text;

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
		public string Id { private get; set; }
		public LabelData Label { get; set; }
		public AlignAttribute LabelAlignAttribute { get; set; }

		private void AppendLabel(StringBuilder stringBuilder)
		{
			if (Label == null)
			{
				return;
			}
			Label.ForId = Id;
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
			sb.Append(CreateQuotedAttribute("type", "checkbox"));
			if (Id != null)
			{
				sb.Append(CreateQuotedAttribute("id", Id));
				sb.Append(CreateQuotedAttribute("name", Id));
			}
			if (Checked)
			{
				sb.Append(CreateQuotedAttribute("checked", "checked"));
			}
			sb.Append("/>");
			AppendLabel(sb);
			return sb.ToString();
		}
	}
}