using DiplomaWebService.Models.Invoice.In;
using DiplomaWebService.Models.Items;
using DiplomaWebService.Models.Types;

namespace DiplomaWebService.Models.Invoice.ViewModel
{
    public class InvoiceInViewModel
    {
        public List<InvoiceIn> InvoicesIn { get; set; }
        public List<Sector> Sectors { get; set; }
        public List<DocumentType> DocumentTypes { get; set; }
        public List<InvoiceType> InvoiceTypes { get; set; }

        public List<Contragent> Contragents { get; set; }

        public List<Item> Items { get; set; }

        public List<Unit> Units { get; set; }

        public List<Category> Categories { get; set; }
    }
}
