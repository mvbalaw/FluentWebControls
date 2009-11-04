using System;
using System.Text;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;

namespace FluentWebControls
{
	public class ButtonData
	{
		private readonly string _controllerName;
		private readonly IPathUtility _pathUtility;
		private readonly IButtonType _type;
		private string _text;

		public ButtonData(IButtonType type, IPathUtility pathUtility)
			: this(type, pathUtility, null)
		{
		}

		public ButtonData(IButtonType type, IPathUtility pathUtility, string controllerName)
		{
			_type = type;
			_pathUtility = pathUtility;
			_controllerName = controllerName;
			Text = type.Name;
			Visible = true;
		}

		public string ActionName { get; set; }
		public string ConfirmMessage { get; set; }
		public string CssClass { get; set; }
		public string OnClickMethod { get; set; }
		public string QueryParameter { get; set; }

		public string Text
		{
			get { return _text; }
			set { _text = value ?? _type.Name; }
		}

		public bool Visible { get; set; }
		public string Width { get; set; }

		public override string ToString()
		{
			if (!Visible)
			{
				return "";
			}

			StringBuilder sb = new StringBuilder();
			sb.Append("<input");
			sb.Append(String.Format("btn{0}", _type.Name).CreateQuotedAttribute("Id"));
			sb.Append(String.Format("btn{0}", _type.Name).CreateQuotedAttribute("name"));
			sb.Append(Text.CreateQuotedAttribute("value"));
			sb.Append(_type.CssClass.CreateQuotedAttribute("class"));
			if (!String.IsNullOrEmpty(Width))
			{
				var value = "width:" + Width;
				sb.Append(value.CreateQuotedAttribute("style"));
			}
			sb.Append(_type.Type.CreateQuotedAttribute("type"));

			if (_type.Type.Equals("submit", StringComparison.OrdinalIgnoreCase))
			{
				string actionName = ActionName ?? _type.Name;
				string url = _pathUtility.GetUrl(String.Format("/{0}.mvc/{1}", _controllerName, actionName));
				sb.Append((String.IsNullOrEmpty(QueryParameter) ? url : String.Format("{0}?{1}", url, QueryParameter)).CreateQuotedAttribute("action"));

				if (String.IsNullOrEmpty(OnClickMethod))
				{
					OnClickMethod = String.Format("javascript:return {0}",
					                              _type == ButtonType.Delete
					                              	? String.Format("confirmThenChangeFormAction(\"{0}\", this)", ConfirmMessage ?? ButtonType.Delete.ConfirmationMessage)
					                              	: "changeFormAction(this)");
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
			public static ButtonType Basic = new ButtonType("Basic", "button", JQueryFormValidationType.IgnoreFormOnClick, "");
			public static ButtonType Cancel = new ButtonType("Cancel", "submit", JQueryFormValidationType.IgnoreFormOnClick, "");
			public static ButtonType Delete = new ButtonType("Delete", "submit", JQueryFormValidationType.IgnoreFormOnClick, "Are you sure you want to delete this");
			public static ButtonType Download = new ButtonType("Download", "submit", JQueryFormValidationType.IgnoreFormOnClick, "");
			public static ButtonType Go = new ButtonType("Go", "submit", JQueryFormValidationType.IgnoreFormOnClick, "");
			public static ButtonType New = new ButtonType("New", "submit", JQueryFormValidationType.IgnoreFormOnClick, "");
			public static ButtonType Save = new ButtonType("Save", "submit", JQueryFormValidationType.ValidateFormOnClick, "");

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