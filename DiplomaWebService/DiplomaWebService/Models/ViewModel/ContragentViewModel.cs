namespace DiplomaWebService.Models.ViewModel
{
    public class ContragentViewModel : BaseViewModel
    {
        public ContragentViewModel(char usernameFirstLetter, string username, List<Contragent> contragents) : base(usernameFirstLetter, username)
        {
            Contragents = contragents;
        }
        public List<Contragent> Contragents { get; set; }
    }
}
