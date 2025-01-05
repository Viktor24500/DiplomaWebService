using DiplomaWebService.Models.Items;

namespace DiplomaWebService.Models
{
	public class StockItem
	{
		public StockItem(int stockItemId, decimal allAmount, Item item,
		Unit unit, Category category, decimal price)
		{
			StockItemId = stockItemId;
			AllAmount = allAmount;
			Item = item;
			Unit = unit;
			Category = category;
			Price = price;
		}

		public int StockItemId { get; set; }
		public decimal AllAmount { get; set; }
		public Item Item { get; set; }
		public Unit Unit { get; set; }
		public Category Category { get; set; }
		public decimal Price { get; set; }
	}
}
