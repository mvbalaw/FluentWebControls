namespace FluentWebControls.Tests
{
	public class TestData
	{
		public class Item
		{
			public Item(int itemId, string itemName)
			{
				ItemId = itemId;
				ItemName = itemName;
			}

			public int ItemId { get; set; }
			public string ItemName { get; set; }
		}
	}
}