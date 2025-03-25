using DiplomaWebService.Models.Invoice.In;
using DiplomaWebService.Models.Items;
using DiplomaWebService.Models.Types;

namespace DiplomaWebService.Models.ViewModel.Invoice.In
{
	public class InvoiceInViewModelInvoice : BaseViewModel
	{
		public InvoiceInViewModelInvoice(char usernameFirstLetter, string username, int roleId, InvoiceIn invoiceIn, List<Sector> sectors,
			List<DocumentType> documentTypes, List<Contragent> contragents,
			List<Item> items, List<Unit> units, List<Category> categories) : base(usernameFirstLetter, username, roleId)
		{
			InvoiceIn = invoiceIn;
			Sectors = sectors;
			DocumentTypes = documentTypes;
			Contragents = contragents;
			Items = items;
			Units = units;
			Categories = categories;
		}

		public InvoiceIn InvoiceIn { get; set; }
		public List<Sector> Sectors { get; set; }
		public List<DocumentType> DocumentTypes { get; set; }

		public List<Contragent> Contragents { get; set; }

		public List<Item> Items { get; set; }

		public List<Unit> Units { get; set; }

		public List<Category> Categories { get; set; }
	}
}
