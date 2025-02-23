namespace DiplomaWebService.Models.ViewModel
{
	public class StockItemViewModel : BaseViewModel
	{
		public StockItemViewModel(char usernameFirstLetter, string username, int roleId, List<StockItem> stockItems, List<Sector> sectors) :
			base(usernameFirstLetter, username, roleId)
		{
			StockItems = stockItems;
			Sectors = sectors;
		}

		public List<StockItem> StockItems { get; set; }
		public List<Sector> Sectors { get; set; }
	}
}
