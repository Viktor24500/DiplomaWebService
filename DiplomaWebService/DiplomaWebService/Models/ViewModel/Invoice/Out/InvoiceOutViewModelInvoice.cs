using DiplomaWebService.Models.Invoice.Out;
using DiplomaWebService.Models.Types;

namespace DiplomaWebService.Models.ViewModel.Invoice.Out
{
	public class InvoiceOutViewModelInvoice : BaseViewModel
	{
		public InvoiceOutViewModelInvoice(char usernameFirstLetter, string username, int roleId, InvoiceOut invoiceOut, List<Sector> sectors,
			List<DocumentType> documentTypes, List<Contragent> contragents, List<StockItem> stockItems) : base(usernameFirstLetter, username, roleId)
		{
			InvoicesOut = invoiceOut;
			Sectors = sectors;
			DocumentTypes = documentTypes;
			StockItems = stockItems;
			Contragents = contragents;
		}
		public InvoiceOut InvoicesOut { get; set; }
		public List<Sector> Sectors { get; set; }
		public List<DocumentType> DocumentTypes { get; set; }
		public List<InvoiceType> InvoiceTypes { get; set; }

		public List<Contragent> Contragents { get; set; }
		public List<StockItem> StockItems { get; set; }
	}
}