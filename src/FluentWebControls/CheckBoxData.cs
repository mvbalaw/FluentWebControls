using System;
using System.Text;
using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public interface ICheckBoxData
	{
		bool Checked { get; }
		LabelData Label { get; }
		AlignAttribute LabelAlignAttribute { get; }
		string Value { get; }
		string TabIndex { get; }
		string CssClass { get; }
	}

	public class CheckBoxData : WebControlBase, ICheckBoxData
	{
		public CheckBoxData(bool isChecked)
		{
			Checked = isChecked;
			LabelAlignAttribute = AlignAttribute.Right;
		}

		internal bool Checked { private get; set; }
		internal LabelData Label { private get; set; }
		internal AlignAttribute LabelAlignAttribute { private get; set; }
		internal string Value { private get; set; }
		internal string TabIndex { private get; set; }
		internal string CssClass { private get; set; }

		#region ICheckBoxData Members

		LabelData ICheckBoxData.Label
		{
			get { return Label; }
		}

		AlignAttribute ICheckBoxData.LabelAlignAttribute
		{
			get { return LabelAlignAttribute; }
		}

		bool ICheckBoxData.Checked
		{
			get { return Checked; }
		}

		string ICheckBoxData.Value
		{
			get { return Value; }
		}

		string ICheckBoxData.TabIndex
		{
			get { return TabIndex; }
		}

		string ICheckBoxData.CssClass
		{
			get { return CssClass; }
		}

		#endregion

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
			sb.Append("checkbox".CreateQuotedAttribute("type"));
			if (!IdWithPrefix.IsNullOrEmpty())
			{
				sb.Append(IdWithPrefix.CreateQuotedAttribute("id"));
				sb.Append(NameWithPrefix.CreateQuotedAttribute("name"));
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