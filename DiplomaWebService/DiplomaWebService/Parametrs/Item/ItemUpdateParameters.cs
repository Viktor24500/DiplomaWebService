namespace DiplomaWebService.Parametrs.Item
{
    public class ItemUpdateParameters
    {
        public ItemUpdateParameters(int itemId, string itemName, int sectorId, int? inventoryNumber)
        {
            ItemId = itemId;
            ItemName = itemName;
            SectorId = sectorId;
            InventoryNumber = inventoryNumber;
        }
        public int ItemId { get; set; }
        public string ItemName { get; set; }

        public int? InventoryNumber { get; set; }
        public int SectorId { get; set; }
    }
}
