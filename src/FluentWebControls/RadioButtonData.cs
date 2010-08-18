using System;
using System.Text;

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public interface IRadioButtonData
	{
		bool Checked { get; }
		LabelData Label { get; }
		AlignAttribute LabelAlignAttribute { get; }
		string Value { get; }
		string TabIndex { get; }
	}

	public class RadioButtonData : WebControlBase, IRadioButtonData
	{
		public RadioButtonData(bool isChecked)
		{
			Checked = isChecked;
			LabelAlignAttribute = AlignAttribute.Right;
		}

		internal bool Checked { private get; set; }
		internal LabelData Label { private get; set; }
		internal AlignAttribute LabelAlignAttribute { private get; set; }
		internal string Value { private get; set; }
		internal string TabIndex { private get; set; }

		LabelData IRadioButtonData.Label
		{
			get { return Label; }
		}
		AlignAttribute IRadioButtonData.LabelAlignAttribute
		{
			get { return LabelAlignAttribute; }
		}
		bool IRadioButtonData.Checked
		{
			get { return Checked; }
		}

		string IRadioButtonData.Value
		{
			get { return Value; }
		}
		string IRadioButtonData.TabIndex
		{
			get { return TabIndex; }
		}

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
				if (!String.IsNullOrEmpty(Label.Text) && Label.Text.EndsWith(":"))
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
				sb.Append(IdWithPrefix.CreateQuotedAttribute("name"));
			}
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
			sb.Append("/>");
			AppendLabel(sb);
			return sb.ToString();
		}
	}
}