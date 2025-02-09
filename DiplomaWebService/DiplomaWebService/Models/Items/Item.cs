namespace DiplomaWebService.Models.Items
{
    public class Item
    {
        public Item(int itemId, string itemName, Sector sector, string? inventoryNumber)
        {
            ItemId = itemId;
            ItemName = itemName;
            Sector = sector;
            InventoryNumber = inventoryNumber;
        }
        public int ItemId { get; set; }
        public string ItemName { get; set; }

        public string? InventoryNumber { get; set; }
        public Sector Sector { get; set; }
    }
}
