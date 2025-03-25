namespace DiplomaWebService.Parametrs.Invoice.In
{
	public class InvoicePositionsInCreateParameters
	{
		//    public InvoicePositionsInCreateParameters(int itemId, decimal amount,
		//decimal price, int unitId, int categoryId)
		//    {
		//        ItemId = itemId;
		//        Amount = amount;
		//        Price = price;
		//        UnitId = unitId;
		//        CategoryId = categoryId;
		//    }
		public int ItemId { get; set; }
		public decimal Amount { get; set; }
		public decimal Price { get; set; }
		//public int UnitId { get; set; }
		public int CategoryId { get; set; }
		public string? SerialNumber { get; set; }
		public int? ProductionYear { get; set; }
	}
}
