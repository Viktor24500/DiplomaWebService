namespace DiplomaWebService.Parametrs.User
{
    public class UserCreateParameters
    {
        public UserCreateParameters(string userName, string userPassword, string email, string firstName, string lastName, string?
            fatherName, bool isActive, int roleId)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            FatherName = fatherName;
            IsActive = isActive;
            RoleId = roleId;
            UserPassword = userPassword;
            Email = email;
        }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? FatherName { get; set; }

        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
