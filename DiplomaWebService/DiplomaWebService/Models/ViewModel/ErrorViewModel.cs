using DiplomaWebService.Models.ViewModel;

namespace DiplomaWebService.Models
{
    public class ErrorViewModel : BaseViewModel
    {
        public ErrorViewModel(char usernameFirstLetter, string username, string erorrCodeName, string errorMsg) : base(usernameFirstLetter, username)
        {
            ErrorCodeName = erorrCodeName;
            ErrorMsg = errorMsg;
        }
        public string ErrorCodeName { get; set; }

        public string ErrorMsg { get; set; }
    }
}
