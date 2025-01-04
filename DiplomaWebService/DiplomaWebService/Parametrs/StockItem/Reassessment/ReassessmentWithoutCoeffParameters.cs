namespace DiplomaWebService.Parametrs.StockItem.Reassessment
{
    public class ReassessmentWithoutCoeffParameters
    {
        public ReassessmentWithoutCoeffParameters(string stockItemId, decimal newPrice, string documentNumber,
            DateTime documentDate, DateTime operationDate)
        {
            StockItemId = stockItemId;
            NewPrice = newPrice;
            DocumentNumber = documentNumber;
            DocumentDate = documentDate;
            OperationDate = operationDate;
        }

        public string StockItemId { get; set; }
        public decimal NewPrice { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public DateTime OperationDate { get; set; }
    }
}
