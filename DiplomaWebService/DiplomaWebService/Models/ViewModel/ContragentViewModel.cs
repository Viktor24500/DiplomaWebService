namespace DiplomaWebService.Models.ViewModel
{
	public class ContragentViewModel : BaseViewModel
	{
		public ContragentViewModel(char usernameFirstLetter, string username, int roleId, List<Contragent> contragents) :
			base(usernameFirstLetter, username, roleId)
		{
			Contragents = contragents;
		}
		public List<Contragent> Contragents { get; set; }
	}
}
