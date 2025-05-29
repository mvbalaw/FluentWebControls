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
using System.Collections.Generic;
using System.Text;

using FluentWebControls.Extensions;
using MvbaCore.Interfaces;

namespace FluentWebControls
{
	public class ButtonData : IControllerAwareWebControl
	{
		private readonly IPathUtility _pathUtility;
		private readonly IButtonType _type;
		private readonly List<string> _urlParameters = new List<string>();
		private string _text;

		public ButtonData(IButtonType type)
			: this(type, null, null)
		{
		}

		public ButtonData(IButtonType type, IPathUtility pathUtility)
			: this(type, pathUtility, null)
		{
		}

		public ButtonData(IButtonType type, IPathUtility pathUtility, string controllerName)
		{
			_type = type;
			_pathUtility = pathUtility;
			ControllerName = controllerName;
			Text = type.Name;
			Visible = true;
		}

		public string ActionName { get; set; }
		public string ConfirmMessage { get; set; }
		public string ControllerExtension { get; set; }
		public string ControllerName { get; set; }
		public string CssClass { get; set; }
		public bool Default { get; set; }
		public string Id { get; set; }
		public string OnClickMethod { get; set; }
		public string QueryParameter { get; set; }

		public string Text
		{
			get => _text;
			set => _text = value ?? _type.Name;
		}

		public bool Visible { get; set; }
		public string Width { get; set; }

		public void AddUrlParameter(string parameter)
		{
			_urlParameters.Add(parameter);
		}

		public void AddUrlParameters(List<string> parameters)
		{
			_urlParameters.AddRange(parameters);
		}

		private string BuildUrlParameters()
		{
			if (_urlParameters.Count <= 0)
			{
				return "";
			}
			return $"/{_urlParameters.Join("/")}";
		}

		public override string ToString()
		{
			if (!Visible)
			{
				return "";
			}

			var sb = new StringBuilder();
			sb.Append("<input");
			var id = $"{Id ?? "btn" + _type.Name}";
			sb.Append(id.CreateQuotedAttribute("Id"));
			sb.Append(id.CreateQuotedAttribute("name"));
			sb.Append(Text.CreateQuotedAttribute("value"));
			sb.Append((_type.CssClass + (Default ? " default" : "") + (CssClass != null ? " " + CssClass : "")).CreateQuotedAttribute("class"));
			if (!string.IsNullOrEmpty(Width))
			{
				var value = "width:" + Width;
				sb.Append(value.CreateQuotedAttribute("style"));
			}
			sb.Append(_type.Type.CreateQuotedAttribute("type"));

			if (_type.Type.Equals("submit", StringComparison.OrdinalIgnoreCase))
			{
				var actionName = ActionName ?? _type.Name;
				var virtualDirectory = $"/{ControllerName}{ControllerExtension ?? ""}/{actionName}";
				var url = _pathUtility == null ? virtualDirectory : _pathUtility.GetUrl(virtualDirectory);
				sb.Append((string.IsNullOrEmpty(QueryParameter) ? url : $"{url}?{QueryParameter}").CreateQuotedAttribute("action"));

				if (string.IsNullOrEmpty(OnClickMethod))
				{
					OnClickMethod =
						$"javascript:return {(_type == ButtonType.Delete ? $"confirmThenChangeFormAction(\"{ConfirmMessage ?? ButtonType.Delete.ConfirmationMessage}\", this)" : "changeFormAction(this)")}";
				}
			}
			else if (_type.Type.Equals("button", StringComparison.OrdinalIgnoreCase))
			{
				var virtualDirectory =
					$"/{ControllerName}{ControllerExtension ?? ""}/{ActionName}{BuildUrlParameters()}";
				var url = _pathUtility == null ? virtualDirectory : _pathUtility.GetUrl(virtualDirectory);

				if (string.IsNullOrEmpty(OnClickMethod) && _type == ButtonType.Link)
				{
					OnClickMethod = $"javascript:location.href=\"{url}\"";
				}
			}

			if (!string.IsNullOrEmpty(OnClickMethod))
			{
				sb.Append(OnClickMethod.CreateQuotedAttribute("onClick"));
			}
			sb.Append("/>");
			return sb.ToString();
		}

		public class ButtonType : IButtonType
		{
			public static readonly ButtonType Basic = new ButtonType("Basic", "button", JQueryFormValidationType.IgnoreFormOnClick, "");
			public static readonly ButtonType Cancel = new ButtonType("Cancel", "submit", JQueryFormValidationType.IgnoreFormOnClick, "");
			public static readonly ButtonType Delete = new ButtonType("Delete", "submit", JQueryFormValidationType.IgnoreFormOnClick, "Are you sure you want to delete this");
			public static readonly ButtonType Download = new ButtonType("Download", "submit", JQueryFormValidationType.IgnoreFormOnClick, "");
			public static readonly ButtonType Go = new ButtonType("Go", "submit", JQueryFormValidationType.IgnoreFormOnClick, "");
			public static readonly ButtonType Link = new ButtonType("Link", "button", JQueryFormValidationType.IgnoreFormOnClick, "");
			public static readonly ButtonType New = new ButtonType("New", "submit", JQueryFormValidationType.IgnoreFormOnClick, "");
			public static readonly ButtonType Save = new ButtonType("Save", "submit", JQueryFormValidationType.ValidateFormOnClick, "");

			private ButtonType(string name, string type, JQueryFormValidationType validationType, string confirmationMessage)
			{
				Name = name;
				Type = type;
				CssClass = validationType.Type;
				ConfirmationMessage = confirmationMessage;
			}

			public string ConfirmationMessage { get; }
			public string CssClass { get; }
			public string Name { get; }
			public string Type { get; }

			private class JQueryFormValidationType
			{
				public static readonly JQueryFormValidationType IgnoreFormOnClick = new JQueryFormValidationType("cancel");
				public static readonly JQueryFormValidationType ValidateFormOnClick = new JQueryFormValidationType("button");

				private JQueryFormValidationType(string type)
				{
					Type = type;
				}

				public string Type { get; }
			}
		}
	}

	public interface IButtonType
	{
		string ConfirmationMessage { get; }
		string CssClass { get; }
		string Name { get; }
		string Type { get; }
	}
}