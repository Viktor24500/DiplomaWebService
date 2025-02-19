namespace DiplomaWebService.Parametrs.Invoice.In
{
	public class InvoicePositionsInUpdateParameters
	{
		public InvoicePositionsInUpdateParameters(int id, int invoiceId, int itemId, decimal amount,
	decimal price, int unitId, int categoryId, string? serialNumber, DateTime? productionYear)
		{
			Id = id;
			InvoiceId = invoiceId;
			ItemId = itemId;
			Amount = amount;
			Price = price;
			UnitId = unitId;
			CategoryId = categoryId;
			SerialNumber = serialNumber;
			ProductionYear = productionYear;
		}

		public int Id { get; set; }
		public int InvoiceId { get; set; }
		public int ItemId { get; set; }
		public decimal Amount { get; set; }
		public decimal Price { get; set; }
		public int UnitId { get; set; }
		public int CategoryId { get; set; }
		public string? SerialNumber { get; set; }
		public DateTime? ProductionYear { get; set; }
	}
}
