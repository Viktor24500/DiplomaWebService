namespace DiplomaWebService.Models.ViewModel
{
    public class BaseViewModel
    {
        public BaseViewModel(char usernameFirstLetter, string username)
        {
            UsernameFirstLetter = usernameFirstLetter;
            Username = username;
        }
        public char UsernameFirstLetter { get; set; }
        public string Username { get; set; }
    }
}
