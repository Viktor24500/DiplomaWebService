using DiplomaWebService.Common.Enum;
using DiplomaWebService.Common.Results;
using DiplomaWebService.Constants;
using DiplomaWebService.Models;
using DiplomaWebService.Parametrs.Login;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaWebService.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly string? _connectionString;

        public UserController(ILogger<UserController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString(Constant.MainConnectionString);
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult GetLogin()
        {
            return View("/Views/Forms/LoginForm/Login.cshtml");
        }

        [HttpPost]
        [Route("/login")]
        public async Task<Result<string>> Login(string username, string password)
        {
            Result<string> result = new Result<string>();
            string url = _connectionString + "users/login";
            using (HttpClient client = new HttpClient())
            {
                LoginParametrs loginParam = new LoginParametrs(username, password);
                JsonContent content = JsonContent.Create(loginParam);

                HttpResponseMessage responseMessage = await client.PostAsync(url, content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    result.Data = await responseMessage.Content.ReadAsStringAsync();
                }
                else
                {
                    result.ErrorCode = (int)responseMessage.StatusCode;
                    result.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
                }
            }
            if (result.ErrorCode != (int)ErrorCodes.Success)
            {
                result.ErrorCode = (int)ErrorCodes.BadRequest;
                result.ErrorMessage = "";
                _logger.LogError(result.ErrorMessage);
                return result;
            }

            return result;
        }

        [HttpGet]
        [Route("/users")]
        public IActionResult GetCreateUser()
        {
            return View("/Views/Forms/UserForm/NewUserForm.cshtml");
        }

        [Route("/users")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(string username, string userPassword, string email,
                       string firstName, string lastName, string? fatherName, bool isActive, int roleId)
        {
            Result<User> result = new Result<User>();
            string url = _connectionString + "users/users";
            using (HttpClient client = new HttpClient())
            {
                var requestBody = new
                {
                    Username = username,
                    Password = userPassword,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    FatherName = fatherName,
                    IsActive = isActive,
                    RoleId = roleId
                };
                JsonContent content = JsonContent.Create(requestBody);

                HttpResponseMessage responseMessage = await client.PostAsync(url, content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    result.Data = await responseMessage.Content.ReadFromJsonAsync<User>();
                }
                else
                {
                    result.ErrorCode = (int)responseMessage.StatusCode;
                    result.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
                }
            }
            if (result.ErrorCode != (int)ErrorCodes.Success)
            {
                result.ErrorCode = (int)ErrorCodes.BadRequest;
                result.ErrorMessage = "";
                _logger.LogError(result.ErrorMessage);
            }

            return View(result.Data);
        }
    }
}
