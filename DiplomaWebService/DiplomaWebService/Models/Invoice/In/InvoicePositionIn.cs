using DiplomaWebService.Models.Items;

namespace DiplomaWebService.Models.Invoice.In
{
	public class InvoicePositionIn
	{
		public InvoicePositionIn(int id, int invoiceId, Item item, decimal amount,
			decimal price, Unit unit, Category category, string? serialNumber, DateTime? productionYear)
		{
			Id = id;
			InvoiceId = invoiceId;
			Item = item;
			Amount = amount;
			Price = price;
			Unit = unit;
			Category = category;
			SerialNumber = serialNumber;
			ProductionYear = productionYear;
		}

		public int Id { get; set; }
		public int InvoiceId { get; set; }
		public Item Item { get; set; }
		public decimal Amount { get; set; }
		public decimal Price { get; set; }
		public Unit Unit { get; set; }
		public Category Category { get; set; }
		public string? SerialNumber { get; set; }
		public DateTime? ProductionYear { get; set; }
	}
}
