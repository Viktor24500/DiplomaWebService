namespace DiplomaWebService.Parametrs.Invoice.Out
{
    public class InvoicePositionsOutCreateParameters
    {
        public InvoicePositionsOutCreateParameters(/*int itemId, */int stockItemId, decimal amount
    /*decimal price, int unitId, int categoryId*/)
        {
            //ItemId = itemId;
            Amount = amount;
            //Price = price;
            //UnitId = unitId;
            //CategoryId = categoryId;
            StockItemId = stockItemId;
        }
        public int StockItemId { get; set; }
        //public int ItemId { get; set; }
        public decimal Amount { get; set; }
        //public decimal Price { get; set; }
        //public int UnitId { get; set; }
        //public int CategoryId { get; set; }
    }
}
