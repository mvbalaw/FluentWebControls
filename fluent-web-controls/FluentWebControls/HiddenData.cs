using System.Text;

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public class HiddenData
	{
		private readonly string _forId;

		public HiddenData(string forId)
		{
			_forId = forId;
		}

		public string Text { get; set; }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("<input");
			sb.Append("hidden".CreateQuotedAttribute("type"));
			sb.Append(_forId.CreateQuotedAttribute("id"));
			sb.Append(_forId.CreateQuotedAttribute("name"));
			sb.Append(Text.CreateQuotedAttribute("value"));
			sb.Append("/>");
			return sb.ToString();
		}
	}
}