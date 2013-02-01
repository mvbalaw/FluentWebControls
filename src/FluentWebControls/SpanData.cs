using System.Text;

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public interface ISpanData : IWebControl
	{
		string CssClass { get; }
		string IdWithPrefix { get; }
		string Value { get; }
	}

	public class SpanData : WebControlBase, ISpanData
	{
		public SpanData(string value)
		{
			Value = value;
		}

		internal string CssClass { private get; set; }
		internal string Value { private get; set; }
		internal LabelData Label { private get; set; }

		string ISpanData.IdWithPrefix
		{
			get { return IdWithPrefix; }
		}
		string ISpanData.Value
		{
			get { return Value; }
		}
		string ISpanData.CssClass
		{
			get { return CssClass; }
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			if (Label != null)
			{
				Label.ForId = IdWithPrefix;
				sb.Append(Label);
			}
			sb.Append("<span");
			if (IdWithPrefix != null)
			{
				sb.Append(IdWithPrefix.CreateQuotedAttribute("id"));
			}
			if (CssClass != null)
			{
				sb.Append(CssClass.CreateQuotedAttribute("class"));
			}
			sb.Append('>');
			sb.Append(Value.EscapeForHtml());
			sb.Append("</span>");
			return sb.ToString();
		}
	}
}