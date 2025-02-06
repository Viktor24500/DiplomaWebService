using DiplomaWebService.Models.Items;

namespace DiplomaWebService.Models.Invoice.Out
{
    public class InvoicePositionOut
    {
        public InvoicePositionOut(int id, int invoiceId, Item item, decimal amount,
            decimal price, Unit unit, Category category)
        {
            Id = id;
            InvoiceId = invoiceId;
            Item = item;
            Amount = amount;
            Price = price;
            Unit = unit;
            Category = category;
        }

        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public Item Item { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public Unit Unit { get; set; }
        public Category Category { get; set; }
    }
}
