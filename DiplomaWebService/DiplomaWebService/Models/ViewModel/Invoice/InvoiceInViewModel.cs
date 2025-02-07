using DiplomaWebService.Models.Invoice.In;
using DiplomaWebService.Models.Items;
using DiplomaWebService.Models.Types;

namespace DiplomaWebService.Models.ViewModel.Invoice
{
    public class InvoiceInViewModel : BaseViewModel
    {
        public InvoiceInViewModel(char usernameFirstLetter, string username, List<InvoiceIn> invoicesIn, List<Sector> sectors,
            List<DocumentType> documentTypes, List<Contragent> contragents,
            List<Item> items, List<Unit> units, List<Category> categories) : base(usernameFirstLetter, username)
        {
            InvoicesIn = invoicesIn;
            Sectors = sectors;
            DocumentTypes = documentTypes;
            Contragents = contragents;
            Items = items;
            Units = units;
            Categories = categories;
        }

        public List<InvoiceIn> InvoicesIn { get; set; }
        public List<Sector> Sectors { get; set; }
        public List<DocumentType> DocumentTypes { get; set; }

        public List<Contragent> Contragents { get; set; }

        public List<Item> Items { get; set; }

        public List<Unit> Units { get; set; }

        public List<Category> Categories { get; set; }
    }
}
