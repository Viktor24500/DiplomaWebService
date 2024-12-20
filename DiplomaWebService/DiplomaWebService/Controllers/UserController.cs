using DiplomaWebService.Common.Enum;
using DiplomaWebService.Common.Results;
using DiplomaWebService.Constants;
using DiplomaWebService.Models;
using DiplomaWebService.Parametrs.Login;
using DiplomaWebService.Parametrs.User;
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
				_logger.LogError(result.ErrorMessage);
				result.ErrorCode = (int)ErrorCodes.BadRequest;
				result.ErrorMessage = "";
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(errorName, result.ErrorMessage);
				//return View("/Views/Shared/Error.cshtml", errorModel);
				return result;
			}

			return result;
		}

		[HttpGet]
		[Route("/user")]
		public IActionResult GetCreateUser()
		{
			return View("/Views/Forms/UserForm/AddUserForm.cshtml");
		}

		[Route("/user")]
		[HttpPost]
		public async Task<IActionResult> CreateUser(string username, string userPassword, string email,
					   string firstName, string lastName, string? fatherName, int roleId)
		{
			bool isActive = true;
			Result<User> result = new Result<User>();
			string url = _connectionString + "user";
			using (HttpClient client = new HttpClient())
			{
				UserCreateParameters userCreateParam = new UserCreateParameters(
					username, userPassword, email, firstName,
					lastName, fatherName, isActive, roleId);
				JsonContent content = JsonContent.Create(userCreateParam);

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
				result.ErrorMessage = "";
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}

			return View("/Views/User.cshtml", result.Data);
		}

		[HttpGet]
		[Route("/users")]
		public async Task<IActionResult> GetAllUsers()
		{
			Result<List<User>> result = new Result<List<User>>();
			string url = _connectionString + "users";
			using (HttpClient client = new HttpClient())
			{
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
				result.ErrorMessage = "invalid username or password";
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}

			return View("/Views/User.cshtml", result.Data);
		}
	}
}
