namespace DiplomaWebService.Models.ViewModel
{
	public class StockItemViewModel : BaseViewModel
	{
		public StockItemViewModel(char usernameFirstLetter, string username, List<StockItem> stockItems, List<Sector> sectors) : base(usernameFirstLetter, username)
		{
			StockItems = stockItems;
			Sectors = sectors;
		}

		public List<StockItem> StockItems { get; set; }
		public List<Sector> Sectors { get; set; }
	}
}
