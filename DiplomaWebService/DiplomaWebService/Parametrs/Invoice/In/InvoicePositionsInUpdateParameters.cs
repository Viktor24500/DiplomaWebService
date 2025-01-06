namespace DiplomaWebService.Parametrs.Invoice.In
{
    public class InvoicePositionsInUpdateParameters
    {
        public InvoicePositionsInUpdateParameters(int id, int invoiceId, int itemId, decimal amount,
    decimal price, int unitId, int categoryId)
        {
            Id = id;
            InvoiceId = invoiceId;
            ItemId = itemId;
            Amount = amount;
            Price = price;
            UnitId = unitId;
            CategoryId = categoryId;
        }

        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ItemId { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public int UnitId { get; set; }
        public int CategoryId { get; set; }
    }
}
