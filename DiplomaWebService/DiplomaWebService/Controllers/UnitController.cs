using DiplomaWebService.Common.Enum;
using DiplomaWebService.Common.Results;
using DiplomaWebService.Constants;
using DiplomaWebService.Models;
using DiplomaWebService.Models.ViewModel;
using DiplomaWebService.Parametrs.Units;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaWebService.Controllers
{
	public class UnitController : Controller
	{
		private ILogger<UnitController> _logger;
		private string? _connectionString;
		private string _username = " ";
		private char _usernameFirstLetter = ' ';

		public UnitController(ILogger<UnitController> logger, IConfiguration configuration)
		{
			_logger = logger;
			_connectionString = configuration.GetConnectionString(Constant.MainConnectionString);
		}

		[HttpGet]
		[Route("/units")]
		public async Task<IActionResult> GetAllUnits()
		{
			Result<List<Unit>> result = new Result<List<Unit>>();
			Result<string> resToken = GetTokenFromCookies();
			if (resToken.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resToken.ErrorMessage);
				result.ErrorCode = resToken.ErrorCode;
				result.ErrorMessage = resToken.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			Result<string> username = GetUsernameFromSession();
			if (username.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(username.ErrorMessage);
				result.ErrorCode = username.ErrorCode;
				result.ErrorMessage = username.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), username.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, errorName, username.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			string url = _connectionString + "units";
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.GetAsync(url);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<List<Unit>>();
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
				//result.ErrorMessage = "can't get all units";
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			UnitViewModel model = CreateViewModel(username.Data, username.Data[0], result.Data);
			//ViewData["LayoutModel"] = model;
			return View("/Views/Dictionaries/Units/Unit.cshtml", model);
		}

		[HttpPost]
		[Route("/units")]
		public async Task<IActionResult> CreateUnit(string name, string? description)
		{
			Result<Unit> result = new Result<Unit>();
			Result<string> resToken = GetTokenFromCookies();
			if (resToken.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resToken.ErrorMessage);
				result.ErrorCode = resToken.ErrorCode;
				result.ErrorMessage = resToken.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			string url = _connectionString + "units";
			using (HttpClient client = new HttpClient())
			{
				UnitCreateParameters unitCreateParam = new UnitCreateParameters(name, description);
				JsonContent content = JsonContent.Create(unitCreateParam);

				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.PostAsync(url, content);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<Unit>();
				}
				else
				{
					result.ErrorCode = (int)responseMessage.StatusCode;
					result.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
				}
				if (result.ErrorCode != (int)ErrorCodes.Success)
				{
					_logger.LogError(result.ErrorMessage);
					result.ErrorCode = (int)ErrorCodes.BadRequest;
					//result.ErrorMessage = "";
					string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
					ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, errorName, result.ErrorMessage);
					return View("/Views/Shared/Error.cshtml", errorModel);
				}
				return RedirectToAction("GetAllUnits");
			}
		}

		[HttpPut]
		[Route("/units")]
		public async Task<IActionResult> UpdateUnit(int id, string name, string? description)
		{
			Result<Unit> result = new Result<Unit>();
			Result<string> resToken = GetTokenFromCookies();
			if (resToken.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resToken.ErrorMessage);
				result.ErrorCode = resToken.ErrorCode;
				result.ErrorMessage = resToken.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			string url = _connectionString + $"units/{id}";
			using (HttpClient client = new HttpClient())
			{
				UnitUpdateParameters unitUpdateParam = new UnitUpdateParameters(id, name, description);
				JsonContent content = JsonContent.Create(unitUpdateParam);

				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.PutAsync(url, content);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<Unit>();
				}
				else
				{
					result.ErrorCode = (int)responseMessage.StatusCode;
					result.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
				}
				if (result.ErrorCode != (int)ErrorCodes.Success)
				{
					_logger.LogError(result.ErrorMessage);
					result.ErrorCode = (int)ErrorCodes.BadRequest;
					//result.ErrorMessage = "";
					string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
					ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, errorName, result.ErrorMessage);
					return View("/Views/Shared/Error.cshtml", errorModel);
				}

				return RedirectToAction("GetAllUnits");
			}
		}

		[HttpGet]
		[Route("/searchUnits/{name}")]
		public async Task<IActionResult> SearchUnitsByName(string name)
		{
			Result<List<Unit>> result = new Result<List<Unit>>();
			Result<string> resToken = GetTokenFromCookies();
			if (resToken.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resToken.ErrorMessage);
				result.ErrorCode = resToken.ErrorCode;
				result.ErrorMessage = resToken.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			Result<string> username = GetUsernameFromSession();
			if (username.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(username.ErrorMessage);
				result.ErrorCode = username.ErrorCode;
				result.ErrorMessage = username.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), username.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, errorName, username.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			string url = _connectionString + $"searchUnits/{name}";
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.GetAsync(url);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<List<Unit>>();
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
				//result.ErrorMessage = "Can't search units";
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			UnitViewModel model = CreateViewModel(username.Data, username.Data[0], result.Data);
			return View("/Views/Dictionaries/Units/Unit.cshtml", model);
		}

		[HttpGet]
		[Route("/unit")]
		public IActionResult GetCreateUnit()
		{
			return View("/Views/Forms/UnitForm/AddUnit.cshtml");
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
		private UnitViewModel CreateViewModel(string username, char usernameFirstLetter, List<Unit> units)
		{
			UnitViewModel model = new UnitViewModel(usernameFirstLetter, username, units);
			return model;
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
	}
}
