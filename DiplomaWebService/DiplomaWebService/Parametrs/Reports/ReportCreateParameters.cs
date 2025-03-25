namespace DiplomaWebService.Parametrs.Reports
{
	public class ReportCreateParameters
	{
		public ReportCreateParameters(int invoiceId)
		{
			InvoiceId = invoiceId;
		}

		public int InvoiceId { get; set; }
	}
}
