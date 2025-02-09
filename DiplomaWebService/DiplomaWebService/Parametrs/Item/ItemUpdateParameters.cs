namespace DiplomaWebService.Parametrs.Item
{
    public class ItemUpdateParameters
    {
        public ItemUpdateParameters(int itemId, string itemName, int sectorId, string? inventoryNumber)
        {
            ItemId = itemId;
            ItemName = itemName;
            SectorId = sectorId;
            InventoryNumber = inventoryNumber;
        }
        public int ItemId { get; set; }
        public string ItemName { get; set; }

        public string? InventoryNumber { get; set; }
        public int SectorId { get; set; }
    }
}
