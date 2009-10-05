using System.Text;

namespace FluentWebControls
{
	public class HiddenData : WebControlBase
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
			sb.Append(CreateQuotedAttribute("type", "hidden"));
			sb.Append(CreateQuotedAttribute("id", _forId));
			sb.Append(CreateQuotedAttribute("name", _forId));
			sb.Append(CreateQuotedAttribute("value", Text));
			sb.Append("/>");
			return sb.ToString();
		}
	}
}