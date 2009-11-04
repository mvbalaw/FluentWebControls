using System;

namespace FluentWebControls
{
	public abstract class WebControlBase : IWebControl
	{
		protected string IdWithPrefix
		{
			get
			{
				var id = ((IWebControl)this).IdPrefix ?? "";
				if (!String.IsNullOrEmpty(((IWebControl)this).IdPrefix))
				{
					id += ".";
				}
				id += ((IWebControl)this).Id;
				return id;
			}
		}
		string IWebControl.Id { get; set; }
		string IWebControl.IdPrefix { get; set; }
	}
}