using DiplomaWebService.Models.Types;

namespace DiplomaWebService.Models.Invoice
{
    public class Invoice
    {
        public Invoice(int id, DateTime invoiceDate, string number,
                    int destinationId, string destinationName, int senderId, string senderName,
                    InvoiceType invoiceType, Sector sector, DocumentType documentType,
                    List<InvoicePosition> invoicePositions)
        {
            Id = id;
            InvoiceDate = invoiceDate;
            Number = number;
            DestinationId = destinationId;
            DestinationName = destinationName;
            SenderId = senderId;
            SenderName = senderName;
            InvoiceType = invoiceType;
            Sector = sector;
            DocumentType = documentType;
            InvoicePositions = invoicePositions;
        }

        public int Id { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        public string Number { get; set; }
        public int DestinationId { get; set; }
        public string DestinationName { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public Sector Sector { get; set; }
        public DocumentType DocumentType { get; set; }

        public List<InvoicePosition> InvoicePositions { get; set; }
    }
}
