namespace DiplomaWebService.Request.User
{
	public class UserUpdateRequest
	{
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string? Comment { get; set; }
		public bool IsActive { get; set; }
		public string PhoneNumber { get; set; }
		public int RoleId { get; set; }
	}
}
