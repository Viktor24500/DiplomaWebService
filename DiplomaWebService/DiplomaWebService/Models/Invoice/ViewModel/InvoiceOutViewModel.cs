using DiplomaWebService.Models.Invoice.Out;
using DiplomaWebService.Models.Types;

namespace DiplomaWebService.Models.Invoice.ViewModel
{
    public class InvoiceOutViewModel
    {
        public List<InvoiceOut> InvoicesOut { get; set; }
        public List<Sector> Sectors { get; set; }
        public List<DocumentType> DocumentTypes { get; set; }
        public List<InvoiceType> InvoiceTypes { get; set; }

        public List<Contragent> Contragents { get; set; }
        public List<StockItem> StockItems { get; set; }
    }
}
