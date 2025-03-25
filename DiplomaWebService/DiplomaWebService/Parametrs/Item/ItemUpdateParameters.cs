namespace DiplomaWebService.Parametrs.Item
{
	public class ItemUpdateParameters
	{
		public ItemUpdateParameters(int itemId, string itemName, int sectorId, string? nomenclatureNumber, decimal? weight,
			 decimal? requiredQuantity, int unitId, string? description)
		{
			ItemId = itemId;
			ItemName = itemName;
			SectorId = sectorId;
			NomenclatureNumber = nomenclatureNumber;
			Weight = weight;
			RequiredQuantity = requiredQuantity;
			UnitId = unitId;
			Description = description;
		}
		public int ItemId { get; set; }
		public string ItemName { get; set; }

		public string? NomenclatureNumber { get; set; }

		public int SectorId { get; set; }

		public decimal? Weight { get; set; }

		public decimal? RequiredQuantity { get; set; }

		public int UnitId { get; set; }

		public string? Description { get; set; }
	}
}
