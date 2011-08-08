namespace FluentWebControls.Extensions
{
	public static class CheckBoxListDataExtensions
	{
		public static CheckBoxListData WithTabIndex(this CheckBoxListData checkBoxListData, string tabIndex)
		{
			checkBoxListData.TabIndex = tabIndex;
			return checkBoxListData;
		}

		public static CheckBoxListData WithClass(this CheckBoxListData checkBoxListData, string cssClass)
		{
			checkBoxListData.CssClass.Add(cssClass);
			return checkBoxListData;
		}
	}
}