namespace DiplomaWebService.Request.Items
{
	public class ItemUpdateRequest
	{
		public string ItemName { get; set; }

		public string? NomenclatureNumber { get; set; }

		public int SectorId { get; set; }

		public decimal? Weight { get; set; }

		public decimal? RequiredQuantity { get; set; }

		public int UnitId { get; set; }

		public string? Description { get; set; }
	}
}
