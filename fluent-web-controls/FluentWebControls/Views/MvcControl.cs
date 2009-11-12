using System;
using System.Web.Mvc;

namespace FluentWebControls.Views
{
	public class MvcControl<TModel> : ViewUserControl<TModel>
		where TModel : class
	{
		protected override void OnInit(EventArgs e)
		{
			Page_Load(this, e);
			base.OnInit(e);
		}

		public virtual void Page_Load(object source, EventArgs e)
		{
		}
	}
}