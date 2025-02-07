using DiplomaWebService.Common.Enum;
using DiplomaWebService.Common.Results;
using DiplomaWebService.Constants;
using DiplomaWebService.Models;
using DiplomaWebService.Models.Users;
using DiplomaWebService.Models.ViewModel;
using DiplomaWebService.Parametrs.Login;
using DiplomaWebService.Parametrs.User;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaWebService.Controllers
{
    public class UserController : Controller
    {
        private ILogger<UserController> _logger;
        private string? _connectionString;

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
        public async Task<IActionResult> Login(string username, string password)
        {
            Result<Login> result = new Result<Login>();
            string url = _connectionString + "users/login";
            using (HttpClient client = new HttpClient())
            {
                LoginParametrs loginParam = new LoginParametrs(username, password);
                JsonContent content = JsonContent.Create(loginParam);

                HttpContext.Response.Cookies.Delete("token");
                HttpResponseMessage responseMessage = await client.PostAsync(url, content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    result.Data = await responseMessage.Content.ReadFromJsonAsync<Login>();
                }
                else
                {
                    result.ErrorCode = (int)responseMessage.StatusCode;
                    result.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
                }
            }
            if (result.ErrorCode != (int)ErrorCodes.Success)
            {
                _logger.LogError(result.ErrorMessage);
                result.ErrorCode = (int)ErrorCodes.BadRequest;
                //result.ErrorMessage = "";
                string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
                ErrorViewModel errorModel = new ErrorViewModel(errorName, result.ErrorMessage);
                return View("/Views/Shared/ErrorWithoutLayout.cshtml", errorModel);
            }
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = result.Data.TokenExpiration;
            HttpContext.Session.SetString("Username", username);
            HttpContext.Response.Cookies.Append("token", result.Data.Token, cookieOptions);

            BaseViewModel model = CreateBaseViewModel(username, username[0]);
            ViewData["LayoutModel"] = model;
            return RedirectToAction("GetAllStockItems", "StockItem");
        }
        [HttpGet]
        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Response.Cookies.Delete("token");
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Route("/user")]
        public IActionResult GetCreateUser()
        {
            return View("/Views/Forms/UserForm/AddUserForm.cshtml");
        }

        [Route("/users")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(string lastName, string firstName, string? fatherName,
                                    string username, string userPassword, string email, int roleId, bool isActive)
        {
            isActive = true;
            Result<User> result = new Result<User>();
            string url = _connectionString + "users";
            Result<string> resToken = GetTokenFromCookies();
            if (resToken.ErrorCode != (int)ErrorCodes.Success)
            {
                _logger.LogError(resToken.ErrorMessage);
                result.ErrorCode = resToken.ErrorCode;
                result.ErrorMessage = resToken.ErrorMessage;
                string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
                ErrorViewModel errorModel = new ErrorViewModel(errorName, result.ErrorMessage);
                return View("/Views/Shared/Error.cshtml", errorModel);
            }
            using (HttpClient client = new HttpClient())
            {
                UserCreateParameters userCreateParam = new UserCreateParameters(
                    username, userPassword, email, firstName,
                    lastName, fatherName, isActive, roleId);
                JsonContent content = JsonContent.Create(userCreateParam);

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
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
                _logger.LogError(result.ErrorMessage);
                result.ErrorCode = (int)ErrorCodes.BadRequest;
                //result.ErrorMessage = "";
                string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
                ErrorViewModel errorModel = new ErrorViewModel(errorName, result.ErrorMessage);
                return View("/Views/Shared/Error.cshtml", errorModel);
            }

            return RedirectToAction("GetAllUsers");
        }

        [HttpGet]
        [Route("/users")]
        public async Task<IActionResult> GetAllUsers()
        {
            Result<List<User>> result = new Result<List<User>>();
            string url = _connectionString + "users";

            Result<string> resToken = GetTokenFromCookies();
            Result<string> username = GetUsernameFromSession();
            if (username.ErrorCode != (int)ErrorCodes.Success)
            {
                _logger.LogError(username.ErrorMessage);
                result.ErrorCode = username.ErrorCode;
                result.ErrorMessage = username.ErrorMessage;
                string errorName = Enum.GetName(typeof(ErrorCodes), username.ErrorCode);
                ErrorViewModel errorModel = new ErrorViewModel(errorName, username.ErrorMessage);
                return View("/Views/Shared/Error.cshtml", errorModel);
            }
            if (resToken.ErrorCode != (int)ErrorCodes.Success)
            {
                _logger.LogError(resToken.ErrorMessage);
                result.ErrorCode = resToken.ErrorCode;
                result.ErrorMessage = resToken.ErrorMessage;
                string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
                ErrorViewModel errorModel = new ErrorViewModel(errorName, result.ErrorMessage);
                return View("/Views/Shared/Error.cshtml", errorModel);
            }
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
                HttpResponseMessage responseMessage = await client.GetAsync(url);
                if (responseMessage.IsSuccessStatusCode)
                {
                    result.Data = await responseMessage.Content.ReadFromJsonAsync<List<User>>();
                }
                else
                {
                    result.ErrorCode = (int)responseMessage.StatusCode;
                    result.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
                }
            }
            if (result.ErrorCode != (int)ErrorCodes.Success)
            {
                _logger.LogError(result.ErrorMessage);
                result.ErrorCode = (int)ErrorCodes.BadRequest;
                //result.ErrorMessage = "invalid username or password";
                string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
                ErrorViewModel errorModel = new ErrorViewModel(errorName, result.ErrorMessage);
                return View("/Views/Shared/Error.cshtml", errorModel);
            }
            //get roles
            Result<List<Role>> resultRole = new Result<List<Role>>();
            string urlRole = _connectionString + "roles";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
                HttpResponseMessage responseMessage = await client.GetAsync(urlRole);
                if (responseMessage.IsSuccessStatusCode)
                {
                    resultRole.Data = await responseMessage.Content.ReadFromJsonAsync<List<Role>>();
                }
                else
                {
                    resultRole.ErrorCode = (int)responseMessage.StatusCode;
                    resultRole.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
                }
            }
            if (resultRole.ErrorCode != (int)ErrorCodes.Success)
            {
                _logger.LogError(result.ErrorMessage);
                result.ErrorCode = resultRole.ErrorCode;
                result.ErrorMessage = resultRole.ErrorMessage;
                //result.ErrorMessage = "invalid username or password";
                string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
                ErrorViewModel errorModel = new ErrorViewModel(errorName, result.ErrorMessage);
                return View("/Views/Shared/Error.cshtml", errorModel);
            }

            UserViewModel model = CreateUserViewModel(username.Data, username.Data[0], result.Data, resultRole.Data);
            ViewData["LayoutModel"] = model;
            return View("/Views/User.cshtml", model);
        }

        private Result<string> GetTokenFromCookies()
        {
            Result<string> result = new Result<string>();
            if (!HttpContext.Request.Cookies.TryGetValue("token", out string token))
            {
                result.ErrorMessage = "Authentication token is missing";
                result.ErrorCode = (int)ErrorCodes.BadRequest;
            }
            result.Data = token;
            return result;
        }

        private Result<string> GetUsernameFromSession()
        {
            Result<string> result = new Result<string>();
            string? username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                result.ErrorMessage = "Can't get username from session";
                result.ErrorCode = (int)ErrorCodes.BadRequest;
                _logger.LogError(result.ErrorMessage);
            }
            else
            {
                result.Data = username;
            }
            return result;
        }

        private UserViewModel CreateUserViewModel(string username, char usernameFirstLetter, List<User> users, List<Role> roles)
        {
            UserViewModel model = new UserViewModel(usernameFirstLetter, username, users, roles);
            return model;
        }

        private BaseViewModel CreateBaseViewModel(string username, char usernameFirstLetter)
        {
            BaseViewModel model = new BaseViewModel(usernameFirstLetter, username);
            return model;
        }
    }
}
