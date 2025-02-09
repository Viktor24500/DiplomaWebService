namespace DiplomaWebService.Parametrs.Item
{
    public class ItemCreateParameters
    {
        public ItemCreateParameters(string itemName, int sectorId, string? inventoryNumber)
        {
            ItemName = itemName;
            SectorId = sectorId;
            InventoryNumber = inventoryNumber;
        }
        public string ItemName { get; set; }

        public string? InventoryNumber { get; set; }
        public int SectorId { get; set; }
    }
}
