namespace DiplomaWebService.Models.ViewModel
{
	public class UnitViewModel : BaseViewModel
	{
		public UnitViewModel(char usernameFirstLetter, string username, int roleId, List<Unit> units) : base(usernameFirstLetter, username, roleId)
		{
			Units = units;
		}
		public List<Unit> Units { get; set; }
	}
}
