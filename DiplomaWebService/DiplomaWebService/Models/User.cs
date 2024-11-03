namespace DiplomaWebService.Models
{
    public class User
    {
        public User(int id, string userName, string firstName, string lastName, string? fatherName, bool isActive, string roleName)
        {
            Id = id;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            FatherName = fatherName;
            IsActive = isActive;
            RoleName = roleName;
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? FatherName { get; set; }

        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}
