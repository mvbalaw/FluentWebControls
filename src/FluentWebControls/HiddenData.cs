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
	public interface IHiddenData : IWebControl
	{
		string IdWithPrefix { get; }
		string Value { get; }
	}

	public class HiddenData : WebControlBase, IHiddenData
	{
		internal string Value { private get; set; }

		string IHiddenData.IdWithPrefix => IdWithPrefix;

		string IHiddenData.Value => Value;

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append("<input");
			sb.Append("hidden".CreateQuotedAttribute("type"));
			sb.Append(IdWithPrefix.CreateQuotedAttribute("id"));
			sb.Append(NameWithPrefix.CreateQuotedAttribute("name"));
			sb.Append(Value.CreateQuotedAttribute("value"));
			sb.Append(Data);
			sb.Append("/>");
			return sb.ToString();
		}
	}
}