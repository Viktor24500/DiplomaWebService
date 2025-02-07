using DiplomaWebService.Models.Items;

namespace DiplomaWebService.Models.ViewModel
{
    public class ItemViewModel : BaseViewModel
    {
        public ItemViewModel(char usernameFirstLetter, string username, List<Item> items, List<Sector> sectors) : base(usernameFirstLetter, username)
        {
            Items = items;
            Sectors = sectors;
        }

        public List<Item> Items { get; set; }
        public List<Sector> Sectors { get; set; }
    }
}
