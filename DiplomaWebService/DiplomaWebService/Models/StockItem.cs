using DiplomaWebService.Models.Items;

namespace DiplomaWebService.Models
{
	public class StockItem
	{
		public StockItem(int stockItemId, decimal allAmount, Item item,
			Unit unit, Category category, decimal price, string? serialNumber, int? productionYear, int contragentId, string contragentName)
		{
			StockItemId = stockItemId;
			AllAmount = allAmount;
			Item = item;
			Unit = unit;
			Category = category;
			Price = price;
			SerialNumber = serialNumber;
			ProductionYear = productionYear;
			ContragentId = contragentId;
			ContragentName = contragentName;
		}

		public int StockItemId { get; set; }
		public decimal AllAmount { get; set; }
		public string? SerialNumber { get; set; }
		public int? ProductionYear { get; set; }
		public Item Item { get; set; }
		public Unit Unit { get; set; }
		public Category Category { get; set; }
		public decimal Price { get; set; }

		public int ContragentId { get; set; }

		public string ContragentName { get; set; }
	}
}