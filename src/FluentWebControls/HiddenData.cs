using System.Text;

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public interface IHiddenData
	{
		string IdWithPrefix { get; }
		string Text { get; }
	}

	public class HiddenData : WebControlBase, IHiddenData
	{
		internal string Value { private get; set; }
		string IHiddenData.IdWithPrefix
		{
			get { return IdWithPrefix; }
		}
		string IHiddenData.Text
		{
			get { return Value; }
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append("<input");
			sb.Append("hidden".CreateQuotedAttribute("type"));
			sb.Append(IdWithPrefix.CreateQuotedAttribute("id"));
			sb.Append(IdWithPrefix.CreateQuotedAttribute("name"));
			sb.Append(Value.CreateQuotedAttribute("value"));
			sb.Append("/>");
			return sb.ToString();
		}
	}
}