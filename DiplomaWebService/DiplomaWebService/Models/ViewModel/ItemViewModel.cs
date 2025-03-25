using DiplomaWebService.Models.Items;

namespace DiplomaWebService.Models.ViewModel
{
	public class ItemViewModel : BaseViewModel
	{
		public ItemViewModel(char usernameFirstLetter, string username, int roleId, List<Item> items, List<Sector> sectors,
			List<Unit> units) : base(usernameFirstLetter, username, roleId)
		{
			Items = items;
			Sectors = sectors;
			Units = units;
		}

		public List<Item> Items { get; set; }
		public List<Sector> Sectors { get; set; }
		public List<Unit> Units { get; set; }
	}
}
