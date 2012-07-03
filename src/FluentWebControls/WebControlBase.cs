using System;

namespace FluentWebControls
{
	public abstract class WebControlBase : IWebControl
	{
		protected string IdWithPrefix
		{
			get
			{
				string prefix = ((IWebControl)this).IdPrefix ?? "";
				if (!String.IsNullOrEmpty(((IWebControl)this).IdPrefix))
				{
					prefix += Constants.WebCompatibleSeparator;
				}
				var id = ((IWebControl)this).Id;
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
				string prefix = ((IWebControl)this).NamePrefix ?? "";
				if (!String.IsNullOrEmpty(((IWebControl)this).NamePrefix))
				{
					prefix += ".";
				}
				var id = ((IWebControl)this).Id;
				if (prefix.IsNullOrEmpty())
				{
					id = id.ToCamelCase();
				}
				id = prefix + id;
				return id;
			}
		}
		string IWebControl.Id { get; set; }
		string IWebControl.IdPrefix { get; set; }
		string IWebControl.NamePrefix { get; set; }
	}
}