namespace DiplomaWebService.Parametrs.Item
{
    public class ItemCreateParameters
    {
        public ItemCreateParameters(string itemName, int sectorId, int? inventoryNumber)
        {
            ItemName = itemName;
            SectorId = sectorId;
            InventoryNumber = inventoryNumber;
        }
        public string ItemName { get; set; }

        public int? InventoryNumber { get; set; }
        public int SectorId { get; set; }
    }
}
