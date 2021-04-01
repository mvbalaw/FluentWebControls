//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

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
		internal LabelData Label { private get; set; }
		internal string Value { private get; set; }

		string ISpanData.IdWithPrefix => IdWithPrefix;

		string ISpanData.Value => Value;

		string ISpanData.CssClass => CssClass;

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