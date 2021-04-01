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
	public interface ITextAreaData
	{
		int Cols { get; }
		string CssClass { get; }
		string IdWithPrefix { get; }
		LabelData Label { get; }
		int Rows { get; }
		string TabIndex { get; }
		ValidatableWebControlBase.JQueryFieldValidationType ValidationType { get; }
		string Value { get; }
		string Width { get; }
	}

	public class TextAreaData : ValidatableWebControlBase, ITextAreaData
	{
		private readonly string _value;

		public TextAreaData(string value)
		{
			_value = value;
			CssClass = "textbox";
		}

		internal int Cols { private get; set; }
		internal string CssClass { private get; set; }
		internal LabelData Label { private get; set; }
		internal int Rows { private get; set; }
		internal string TabIndex { private get; set; }
		internal JQueryFieldValidationType ValidationType { private get; set; }
		internal string Width { private get; set; }

		int ITextAreaData.Cols => Cols;

		string ITextAreaData.CssClass => CssClass;

		LabelData ITextAreaData.Label => Label;

		int ITextAreaData.Rows => Rows;

		JQueryFieldValidationType ITextAreaData.ValidationType => ValidationType;

		string ITextAreaData.Width => Width;

		string ITextAreaData.Value => _value;

		string ITextAreaData.IdWithPrefix => IdWithPrefix;

		string ITextAreaData.TabIndex => TabIndex;

		public override string ToString()
		{
			var sb = new StringBuilder();
			if (Label != null)
			{
				Label.ForId = IdWithPrefix;
				sb.Append(Label);
			}
			sb.Append("<textarea");
			if (!IdWithPrefix.IsNullOrEmpty())
			{
				sb.Append(IdWithPrefix.CreateQuotedAttribute("id"));
				sb.Append(NameWithPrefix.CreateQuotedAttribute("name"));
			}
			if (Width != null)
			{
				var value = "width:" + Width;
				sb.Append(value.CreateQuotedAttribute("style"));
			}
			sb.Append(BuildJqueryValidation(CssClass).CreateQuotedAttribute("class"));
			sb.Append(Data);
			if (PropertyMetaData != null)
			{
				if (PropertyMetaData.MinLength > 0)
				{
					sb.Append(PropertyMetaData.MinLength.CreateQuotedAttribute(JQueryFieldValidationType.MinLength.Text));
				}

				if (PropertyMetaData.MaxLength > 0)
				{
					sb.Append(PropertyMetaData.MaxLength.CreateQuotedAttribute(JQueryFieldValidationType.MaxLength.Text));
				}
			}

			if (Rows > 0)
			{
				sb.Append(Rows.CreateQuotedAttribute("Rows"));
			}
			if (Cols > 0)
			{
				sb.Append(Cols.CreateQuotedAttribute("Cols"));
			}

			if (!TabIndex.IsNullOrEmpty())
			{
				sb.Append(TabIndex.CreateQuotedAttribute("tabindex"));
			}

			sb.Append($">{_value.EscapeForHtml()}</textarea>");
			if (PropertyMetaData != null)
			{
				if (PropertyMetaData.IsRequired)
				{
					sb.Append("<em>*</em>");
				}
			}
			return sb.ToString();
		}
	}
}