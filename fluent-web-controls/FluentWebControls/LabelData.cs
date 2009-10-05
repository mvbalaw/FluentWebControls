using System;
using System.Text;

namespace FluentWebControls
{
	public class LabelData : WebControlBase
	{
		public LabelData()
		{
			Style = "float:left;text-align:right";
		}

		public LabelData(string forId)
			: this()
		{
			ForId = forId;
		}

		public string ForId { get; set; }
		public string Style { get; set; }
		public string Text { get; set; }
		public string Width { private get; set; }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (Width != null)
			{
				sb.Append("<span");
				sb.Append(CreateQuotedAttribute("style", "DISPLAY: inline-block; width:" + Width));
				sb.Append('>');
			}
			sb.AppendFormat("<label");
			if (ForId != null)
			{
				sb.AppendFormat(" for='{0}'", ForId);
			}
			if (!String.IsNullOrEmpty(Style))
			{
				sb.Append(CreateQuotedAttribute("style", Style));
			}
			sb.Append('>');
			sb.Append(Text);
			sb.Append("</label>");
			if (Width != null)
			{
				sb.Append("</span>");
			}
			return sb.ToString();
		}
	}
}