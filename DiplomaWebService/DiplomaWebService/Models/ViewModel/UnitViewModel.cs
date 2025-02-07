namespace DiplomaWebService.Models.ViewModel
{
    public class UnitViewModel : BaseViewModel
    {
        public UnitViewModel(char usernameFirstLetter, string username, List<Unit> units) : base(usernameFirstLetter, username)
        {
            Units = units;
        }
        public List<Unit> Units { get; set; }
    }
}
