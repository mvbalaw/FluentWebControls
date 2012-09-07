using System;
using FluentWebControls.Extensions;
using Microsoft.Build.Framework.XamlTypes;

namespace FluentWebControls
{
	public abstract class WebControlBase : IWebControl
	{
		protected string IdWithPrefix
		{
			get
			{
				string prefix = ((IWebControl) this).IdPrefix ?? "";
				if (!String.IsNullOrEmpty(((IWebControl) this).IdPrefix))
				{
					prefix += Constants.WebCompatibleSeparator;
				}
				string id = ((IWebControl) this).Id;
				if (prefix.IsNullOrEmpty())
				{
					id = id.ToCamelCase();
				}
				id = prefix + id;
				return id;
			}
		}

		protected string NameWithPrefix
		{
			get
			{
				string prefix = ((IWebControl) this).NamePrefix ?? "";
				if (!String.IsNullOrEmpty(((IWebControl) this).NamePrefix))
				{
					prefix += ".";
				}
				string id = ((IWebControl) this).Id;
				if (prefix.IsNullOrEmpty())
				{
					id = id.ToCamelCase();
				}
				id = prefix + id;
				return id;
			}
		}

		protected string Data
		{
			get
			{
				var data = ((IWebControl) this).Data;
				return data != null  && !data.Name.IsNullOrEmpty() ? data.Value.CreateQuotedAttribute(String.Format("data-{0}", data.Name)) : "";
			}
		}

		string IWebControl.Id { get; set; }
		string IWebControl.IdPrefix { get; set; }
		string IWebControl.NamePrefix { get; set; }
		NameValuePair IWebControl.Data { get; set; }
	}
}