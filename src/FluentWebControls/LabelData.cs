using System;
using System.Text;

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public class LabelData
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
		public string Value { get; set; }
		public string Width { private get; set; }

		public override string ToString()
		{
			var sb = new StringBuilder();
			if (Width != null)
			{
				sb.Append("<span");
				string v = "DISPLAY: inline-block; width:" + Width;
				sb.Append(v.CreateQuotedAttribute("style"));
				sb.Append('>');
			}
			sb.AppendFormat("<label");
			if (!ForId.IsNullOrEmpty())
			{
				sb.AppendFormat(" for='{0}'", ForId);
			}
			if (!String.IsNullOrEmpty(Style))
			{
				sb.Append(Style.CreateQuotedAttribute("style"));
			}
			sb.Append('>');
			sb.Append(Text);
			sb.Append("</label>");
			if (Value != null)
			{
				sb.AppendFormat("<div style='float:left'>{0}</div>", Value);
			}
			if (Width != null)
			{
				sb.Append("</span>");
			}
			return sb.ToString();
		}
	}
}