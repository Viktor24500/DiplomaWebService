using DiplomaWebService.Common.Enum;
using DiplomaWebService.Common.Results;
using DiplomaWebService.Constants;
using DiplomaWebService.Models;
using DiplomaWebService.Parametrs.Contagents;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaWebService.Controllers
{
	public class ContragentController : Controller
	{
		private ILogger<ContragentController> _logger;
		private string? _connectionString;

		public ContragentController(ILogger<ContragentController> logger, IConfiguration configuration)
		{
			_logger = logger;
			_connectionString = configuration.GetConnectionString(Constant.MainConnectionString);
		}

		[HttpGet]
		[Route("/contragents")]
		public async Task<IActionResult> GetAllContragents()
		{
			Result<List<Contragent>> result = new Result<List<Contragent>>();
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
			string url = _connectionString + "contragents";
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.GetAsync(url);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<List<Contragent>>();
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
				//result.ErrorMessage = "Can't get all contragents";
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			BaseViewModel model = CreateBaseViewModel(username.Data, username.Data[0]);
			ViewData["LayoutModel"] = model;
			return View("/Views/Dictionaries/Contragents/Contragent.cshtml", result.Data);
		}

		[HttpPost]
		[Route("/contragents")]
		public async Task<IActionResult> CreateContragent(string name, int? parentId, bool isActive)
		{
			Result<Contragent> result = new Result<Contragent>();
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
			ContragentCreateParameters contragentCreateParam = new ContragentCreateParameters(parentId, name, isActive);
			string url = _connectionString + "contragents";
			using (HttpClient client = new HttpClient())
			{
				JsonContent content = JsonContent.Create(contragentCreateParam);

				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.PostAsync(url, content);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<Contragent>();
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

			return RedirectToAction("GetAllContragents");
		}

		[HttpPut]
		[Route("/contragents/{id}")]
		public async Task<IActionResult> UpdateContragent(int id, string name, int? parentId, bool isActive)
		{
			Result<Contragent> result = new Result<Contragent>();
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
			string url = _connectionString + $"/contragents/{id}";
			using (HttpClient client = new HttpClient())
			{
				ContragentUpdateParameters contragentUpdateParam = new ContragentUpdateParameters(id, parentId, name, isActive);
				JsonContent content = JsonContent.Create(contragentUpdateParam);

				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.PutAsync(url, content);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<Contragent>();
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
					ErrorViewModel errorModel = new ErrorViewModel(errorName, result.ErrorMessage);
					return View("/Views/Shared/Error.cshtml", errorModel);
				}

				return RedirectToAction("GetAllContragents");
			}
		}
		[HttpGet]
		[Route("/contragent")]
		public IActionResult GetCreateContragent()
		{
			return View("/Views/Forms/ContragentForm/AddContragent.cshtml");
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
		private BaseViewModel CreateBaseViewModel(string username, char usernameFirstLetter)
		{
			BaseViewModel model = new BaseViewModel(usernameFirstLetter, username);
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
