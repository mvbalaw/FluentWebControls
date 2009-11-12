using System;

namespace FluentWebControls.Views
{
	public class PagedGridControl : GridControlBase
	{
		public static class BoundPropertyNames
		{
			public static string PagedGridControl
			{
				get { return "PagedGridControl"; }
			}
		}

		private int Total { get; set; }

		protected override bool ClientSideSortingEnabled
		{
			get { return false; }
		}

		public override void Page_Load(object sender, EventArgs e)
		{
			GridData model = ViewData.Model;
			Total = model.Total;
			base.Page_Load(sender, e);
		}

		protected int LastPage
		{
			get { return (int)Math.Ceiling((decimal)Total / PagedListParameters.PageSize); }
		}
	}
}