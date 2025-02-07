namespace DiplomaWebService.Models.ViewModel
{
	public class StockItemViewModel : BaseViewModel
	{
		public StockItemViewModel(char usernameFirstLetter, string username, List<StockItem> stockItems) : base(usernameFirstLetter, username)
		{
			StockItems = stockItems;
		}

		public List<StockItem> StockItems { get; set; }
	}
}
