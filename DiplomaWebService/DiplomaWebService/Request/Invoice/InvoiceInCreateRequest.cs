using DiplomaWebService.Parametrs.Invoice.In;

namespace DiplomaWebService.Request.Invoice
{
	public class InvoiceInCreateRequest
	{
		public DateTime InvoiceDate { get; set; }
		public string Number { get; set; }
		public int DestinationId { get; set; }
		public int SenderId { get; set; }
		public int SectorId { get; set; }
		public int DocumentTypeId { get; set; }

		public List<InvoicePositionsInCreateParameters> Positions { get; set; }
	}
}
