//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using System;
using System.Text;

using FluentWebControls.Extensions;

namespace FluentWebControls
{
	public class LabelData
	{
		public LabelData()
		{
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
				var v = "DISPLAY: inline-block; width:" + Width;
				sb.Append(v.CreateQuotedAttribute("style"));
				sb.Append('>');
			}
			sb.AppendFormat("<label");
			if (!ForId.IsNullOrEmpty())
			{
				sb.AppendFormat(" for='{0}'", ForId);
			}
			if (!string.IsNullOrEmpty(Style))
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