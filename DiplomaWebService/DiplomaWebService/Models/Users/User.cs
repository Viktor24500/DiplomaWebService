namespace DiplomaWebService.Models.Users
{
	public class User
	{
		public User(int id, string userName, string email, string firstName, string lastName, string? fatherName, bool isActive, string roleName, string statusDescription)
		{
			Id = id;
			UserName = userName;
			FirstName = firstName;
			LastName = lastName;
			FatherName = fatherName;
			IsActive = isActive;
			RoleName = roleName;
			Email = email;
			StatusDescription = statusDescription;
		}
		public int Id { get; set; }
		public string UserName { get; set; }

		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string? FatherName { get; set; }

		public string RoleName { get; set; }
		public bool IsActive { get; set; }
		public string StatusDescription { get; set; }
	}
}