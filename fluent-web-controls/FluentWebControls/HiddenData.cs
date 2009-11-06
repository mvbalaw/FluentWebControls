using System.Text;

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public class HiddenData : WebControlBase
	{
		public string Text { get; set; }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("<input");
			sb.Append("hidden".CreateQuotedAttribute("type"));
			sb.Append(IdWithPrefix.CreateQuotedAttribute("id"));
			sb.Append(IdWithPrefix.CreateQuotedAttribute("name"));
			sb.Append(Text.CreateQuotedAttribute("value"));
			sb.Append("/>");
			return sb.ToString();
		}
	}
}