namespace DiplomaWebService.Models.ViewModel
{
	public class SectorViewModel : BaseViewModel
	{
		public SectorViewModel(char usernameFirstLetter, string username, int roleId, List<Sector> sectors) : base(usernameFirstLetter, username, roleId)
		{
			Sectors = sectors;
		}
		public List<Sector> Sectors { get; set; }
	}
}
