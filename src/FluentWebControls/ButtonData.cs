using System;
using System.Collections.Generic;
using System.Text;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;

namespace FluentWebControls
{
	public class ButtonData : IControllerAwareWebControl
	{
		private readonly IPathUtility _pathUtility;
		private readonly IButtonType _type;
		private readonly List<string> _urlParameters = new List<string>();
		private string _text;

		public ButtonData(IButtonType type)
			: this(type, null,  null)
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
			get { return _text; }
			set { _text = value ?? _type.Name; }
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
			return String.Format("/{0}", _urlParameters.Join("/"));
		}

		public override string ToString()
		{
			if (!Visible)
			{
				return "";
			}

			var sb = new StringBuilder();
			sb.Append("<input");
			string id = String.Format("{0}", Id ?? "btn" + _type.Name);
			sb.Append(id.CreateQuotedAttribute("Id"));
			sb.Append(id.CreateQuotedAttribute("name"));
			sb.Append(Text.CreateQuotedAttribute("value"));
			sb.Append((_type.CssClass + (Default ? " default" : "") + (CssClass != null ? " "+CssClass : "")).CreateQuotedAttribute("class"));
			if (!String.IsNullOrEmpty(Width))
			{
				string value = "width:" + Width;
				sb.Append(value.CreateQuotedAttribute("style"));
			}
			sb.Append(_type.Type.CreateQuotedAttribute("type"));

			if (_type.Type.Equals("submit", StringComparison.OrdinalIgnoreCase))
			{
				string actionName = ActionName ?? _type.Name;
				string virtualDirectory = String.Format("/{0}{1}/{2}", ControllerName, ControllerExtension ?? "", actionName);
				string url = _pathUtility == null ? virtualDirectory : _pathUtility.GetUrl(virtualDirectory);
				sb.Append((String.IsNullOrEmpty(QueryParameter) ? url : String.Format("{0}?{1}", url, QueryParameter)).CreateQuotedAttribute("action"));

				if (String.IsNullOrEmpty(OnClickMethod))
				{
					OnClickMethod = String.Format("javascript:return {0}",
					                              _type == ButtonType.Delete
					                              	? String.Format("confirmThenChangeFormAction(\"{0}\", this)", ConfirmMessage ?? ButtonType.Delete.ConfirmationMessage)
					                              	: "changeFormAction(this)");
				}
			}
			else if (_type.Type.Equals("button", StringComparison.OrdinalIgnoreCase))
			{
				string virtualDirectory = String.Format("/{0}{1}/{2}{3}", ControllerName, ControllerExtension ?? "", ActionName, BuildUrlParameters());
				string url = _pathUtility == null ? virtualDirectory : _pathUtility.GetUrl(virtualDirectory);

				if (String.IsNullOrEmpty(OnClickMethod) && _type == ButtonType.Link)
				{
					OnClickMethod = String.Format("javascript:location.href=\"{0}\"", url);
				}
			}

			if (!String.IsNullOrEmpty(OnClickMethod))
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

			public string ConfirmationMessage { get; private set; }
			public string CssClass { get; private set; }
			public string Name { get; private set; }
			public string Type { get; private set; }

			private class JQueryFormValidationType
			{
				public static readonly JQueryFormValidationType IgnoreFormOnClick = new JQueryFormValidationType("cancel");
				public static readonly JQueryFormValidationType ValidateFormOnClick = new JQueryFormValidationType("button");

				private JQueryFormValidationType(string type)
				{
					Type = type;
				}

				public string Type { get; private set; }
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