namespace DiplomaWebService.Models
{
    public class ErrorViewModel
    {
        public ErrorViewModel(string erorrCodeName, string errorMsg)
        {
            ErrorCodeName = erorrCodeName;
            ErrorMsg = errorMsg;
        }
        public string ErrorCodeName { get; set; }

        public string ErrorMsg { get; set; }
    }
}
