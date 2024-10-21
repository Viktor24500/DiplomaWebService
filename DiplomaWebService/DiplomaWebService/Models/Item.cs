namespace DiplomaWebService.Models
{
	public class Item
	{
		public Item(int itemId, string itemName, Sector sector, int? inventoryNumber)
		{
			ItemId = itemId;
			ItemName = itemName;
			Sector = sector;
			InventoryNumber = inventoryNumber;
		}
		public int ItemId { get; set; }
		public string ItemName { get; set; }

		public int? InventoryNumber { get; set; }
		public Sector Sector { get; set; }
	}
}
