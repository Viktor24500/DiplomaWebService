using DiplomaWebService.Models.Users;

namespace DiplomaWebService.Models.ViewModel
{
    public class UserViewModel : BaseViewModel
    {
        public UserViewModel(char usernameFirstLetter, string username, List<User> users, List<Role> roles) : base(usernameFirstLetter, username)
        {
            Users = users;
            Roles = roles;
        }

        public List<User> Users { get; set; }
        public List<Role> Roles { get; set; }
    }
}
