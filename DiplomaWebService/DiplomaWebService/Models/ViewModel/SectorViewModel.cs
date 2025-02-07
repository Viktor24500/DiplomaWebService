namespace DiplomaWebService.Models.ViewModel
{
    public class SectorViewModel : BaseViewModel
    {
        public SectorViewModel(char usernameFirstLetter, string username, List<Sector> sectors) : base(usernameFirstLetter, username)
        {
            Sectors = sectors;
        }
        public List<Sector> Sectors { get; set; }
    }
}
