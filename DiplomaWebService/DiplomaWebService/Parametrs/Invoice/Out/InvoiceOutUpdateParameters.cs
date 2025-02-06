namespace DiplomaWebService.Parametrs.Invoice.Out
{
    public class InvoiceOutUpdateParameters
    {
        public InvoiceOutUpdateParameters(int id, DateTime invoiceDate, string number,
    int destinationId, int senderId, int invoiceTypeId,
    int sectorId, int documentTypeId, List<InvoicePositionsOutUpdateParameters> invoicePositions)
        {
            Id = id;
            InvoiceDate = invoiceDate;
            Number = number;
            DestinationId = destinationId;
            SenderId = senderId;
            InvoiceTypeId = invoiceTypeId;
            SectorId = sectorId;
            DocumentTypeId = documentTypeId;
            InvoicePositions = invoicePositions;
        }

        public int Id { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        public string Number { get; set; }
        public int DestinationId { get; set; }
        public int SenderId { get; set; }
        public int InvoiceTypeId { get; set; }
        public int SectorId { get; set; }
        public int DocumentTypeId { get; set; }

        public List<InvoicePositionsOutUpdateParameters> InvoicePositions { get; set; }
    }
}
