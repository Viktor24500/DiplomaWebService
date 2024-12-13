namespace DiplomaWebService.Models
{
	public class ErrorViewModel
	{
		public ErrorViewModel(int erorrCode, string errorMsg)
		{
			ErrorCode = erorrCode;
			ErrorMsg = errorMsg;
		}
		public int ErrorCode { get; set; }

		public string ErrorMsg { get; set; }
	}
}
