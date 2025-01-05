namespace DiplomaWebService.Parametrs.StockItem.Reassessment
{
    public class ReassessmentWithCoeffParameters
    {
        public ReassessmentWithCoeffParameters(int stockItemId, decimal coeff, string documentNumber,
            DateTime documentDate, DateTime operationDate)
        {
            StockItemId = stockItemId;
            Coeff = coeff;
            DocumentNumber = documentNumber;
            DocumentDate = documentDate;
            OperationDate = operationDate;
        }

        public int StockItemId { get; set; }
        public decimal Coeff { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public DateTime OperationDate { get; set; }
    }
}
