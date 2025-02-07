using DiplomaWebService.Models.Invoice.Out;
using DiplomaWebService.Models.Types;

namespace DiplomaWebService.Models.ViewModel.Invoice
{
    public class InvoiceOutViewModel : BaseViewModel
    {
        public InvoiceOutViewModel(char usernameFirstLetter, string username, List<InvoiceOut> invoicesOut, List<Sector> sectors,
            List<DocumentType> documentTypes, List<Contragent> contragents, List<StockItem> stockItems) : base(usernameFirstLetter, username)
        {
            InvoicesOut = invoicesOut;
            Sectors = sectors;
            DocumentTypes = documentTypes;
            StockItems = stockItems;
            Contragents = contragents;
        }
        public List<InvoiceOut> InvoicesOut { get; set; }
        public List<Sector> Sectors { get; set; }
        public List<DocumentType> DocumentTypes { get; set; }
        public List<InvoiceType> InvoiceTypes { get; set; }

        public List<Contragent> Contragents { get; set; }
        public List<StockItem> StockItems { get; set; }
    }
}
