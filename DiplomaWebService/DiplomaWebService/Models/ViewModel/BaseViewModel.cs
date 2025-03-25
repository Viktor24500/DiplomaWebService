namespace DiplomaWebService.Models.ViewModel
{
	public class BaseViewModel
	{
		public BaseViewModel(char usernameFirstLetter, string username, int roleId)
		{
			UsernameFirstLetter = usernameFirstLetter;
			Username = username;
			RoleId = roleId;
		}
		public char UsernameFirstLetter { get; set; }
		public string Username { get; set; }

		public int RoleId { get; set; }
	}
}
