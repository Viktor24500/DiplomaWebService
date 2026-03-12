namespace DiplomaWebService.Models.Users
{
	public class User
	{
		public User(int id, string userName, string email, string firstName, string lastName, string? comment, bool isActive, string roleName,
			string statusDescription, string phoneNumber, int roleId)
		{
			Id = id;
			UserName = userName;
			FirstName = firstName;
			LastName = lastName;
			Comment = comment;
			IsActive = isActive;
			RoleName = roleName;
			Email = email;
			StatusDescription = statusDescription;
			PhoneNumber = phoneNumber;
			RoleId = roleId;
		}
		public int Id { get; set; }
		public string UserName { get; set; }

		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string? Comment { get; set; }

		public int RoleId { get; set; }

		public string RoleName { get; set; }
		public bool IsActive { get; set; }
		public string StatusDescription { get; set; }

		public string PhoneNumber { get; set; }
	}
}