namespace DiplomaWebService.Models.Items
{
	public class Item
	{
		public Item(int itemId, string itemName, string? nomenclatureNumber,
			decimal? weight, decimal? requiredQuantity, string? description,
			 Sector sector, Unit unit)
		{
			ItemId = itemId;
			ItemName = itemName;
			NomenclatureNumber = nomenclatureNumber;
			Sector = sector;
			Weight = weight;
			RequiredQuantity = requiredQuantity;
			Unit = unit;
			Description = description;
		}
		public int ItemId { get; set; }
		public string ItemName { get; set; }

		public string? NomenclatureNumber { get; set; }

		public Sector Sector { get; set; }

		public decimal? Weight { get; set; }

		public decimal? RequiredQuantity { get; set; }

		public string? Description { get; set; }

		public Unit Unit { get; set; }
	}
}
