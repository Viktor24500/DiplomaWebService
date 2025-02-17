namespace DiplomaWebService.Parametrs.Item
{
	public class ItemCreateParameters
	{
		public ItemCreateParameters(string itemName, int sectorId, string? nomenclatureNumber, decimal? weight,
							decimal? requiredQuantity, int unitId)
		{
			ItemName = itemName;
			SectorId = sectorId;
			NomenclatureNumber = nomenclatureNumber;
			Weight = weight;
			RequiredQuantity = requiredQuantity;
			UnitId = unitId;
		}
		public string ItemName { get; set; }

		public string? NomenclatureNumber { get; set; }

		public int SectorId { get; set; }

		public decimal? Weight { get; set; }

		public decimal? RequiredQuantity { get; set; }

		public int UnitId { get; set; }
	}
}
