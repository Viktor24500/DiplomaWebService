using DiplomaWebService.Common.Enum;
using DiplomaWebService.Common.Results;
using DiplomaWebService.Constants;
using DiplomaWebService.Models;
using DiplomaWebService.Models.ViewModel;
using DiplomaWebService.Parametrs.StockItem.Reassessment;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaWebService.Controllers
{
	public class StockItemController : Controller
	{
		private ILogger<StockItemController> _logger;
		private string? _connectionString;
		private string _username = " ";
		private char _usernameFirstLetter = ' ';
		private int _roleId = (int)Roles.Viewer;

		public StockItemController(ILogger<StockItemController> logger, IConfiguration configuration)
		{
			_logger = logger;
			_connectionString = configuration.GetConnectionString(Constant.MainConnectionString);
		}

		[HttpGet]
		[Route("/stockItems")]
		public async Task<IActionResult> GetAllStockItems()
		{
			Result<List<StockItem>> result = new Result<List<StockItem>>();
			Result<string> resToken = GetTokenFromCookies();
			if (resToken.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resToken.ErrorMessage);
				result.ErrorCode = resToken.ErrorCode;
				result.ErrorMessage = resToken.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			Result<string> username = GetUsernameFromSession();
			if (username.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(username.ErrorMessage);
				result.ErrorCode = username.ErrorCode;
				result.ErrorMessage = username.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), username.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, username.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			Result<int> roleId = GetRoleIdFromSession();
			if (roleId.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(roleId.ErrorMessage);
				result.ErrorCode = roleId.ErrorCode;
				result.ErrorMessage = roleId.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), roleId.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, roleId.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			string url = _connectionString + "stockItems";
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.GetAsync(url);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<List<StockItem>>();
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
				//result.ErrorMessage = "can't get all stock items";
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			Result<StockItemViewModel> resStockItemViewModel = await CreateViewModel(username.Data, username.Data[0], roleId.Data, result.Data);
			if (resStockItemViewModel.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resStockItemViewModel.ErrorMessage);
				string errorName = Enum.GetName(typeof(ErrorCodes), resStockItemViewModel.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, resStockItemViewModel.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			return View("/Views/StockItems/StockItem.cshtml", resStockItemViewModel.Data);
		}

		[HttpGet]
		[Route("/searchStockItems/{search}")]
		public async Task<IActionResult> SearchStockItem(string search)
		{
			Result<List<StockItem>> result = new Result<List<StockItem>>();
			string url = _connectionString + $"searchStockItems/{search}";

			Result<string> resToken = GetTokenFromCookies();
			Result<string> username = GetUsernameFromSession();
			if (username.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(username.ErrorMessage);
				result.ErrorCode = username.ErrorCode;
				result.ErrorMessage = username.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), username.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, username.ErrorMessage);
				Response.StatusCode = result.ErrorCode;
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			if (resToken.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resToken.ErrorMessage);
				result.ErrorCode = resToken.ErrorCode;
				result.ErrorMessage = resToken.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, result.ErrorMessage);
				Response.StatusCode = result.ErrorCode;
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			Result<int> roleId = GetRoleIdFromSession();
			if (roleId.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(roleId.ErrorMessage);
				result.ErrorCode = roleId.ErrorCode;
				result.ErrorMessage = roleId.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), roleId.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, roleId.ErrorMessage);
				Response.StatusCode = result.ErrorCode;
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.GetAsync(url);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<List<StockItem>>();
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
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, result.ErrorMessage);
				Response.StatusCode = result.ErrorCode;
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			return PartialView("/Views/Views/StockItems/_StockItemList.cshtml", result.Data);
		}

		[HttpGet]
		[Route("/stockItemsByContragentId/{contragentId}")]
		public async Task<IActionResult> GetAllStockItemsByContragentId(int contragentId)
		{
			Result<List<StockItem>> result = new Result<List<StockItem>>();
			Result<string> resToken = GetTokenFromCookies();
			if (resToken.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resToken.ErrorMessage);
				result.ErrorCode = resToken.ErrorCode;
				result.ErrorMessage = resToken.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			Result<string> username = GetUsernameFromSession();
			if (username.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(username.ErrorMessage);
				result.ErrorCode = username.ErrorCode;
				result.ErrorMessage = username.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), username.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, username.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			Result<int> roleId = GetRoleIdFromSession();
			if (roleId.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(roleId.ErrorMessage);
				result.ErrorCode = roleId.ErrorCode;
				result.ErrorMessage = roleId.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), roleId.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, roleId.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			string url = _connectionString + "stockItemsByContragentId/{contragentId}";
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.GetAsync(url);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<List<StockItem>>();
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
				//result.ErrorMessage = "can't get all stock items";
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			Result<StockItemViewModel> resStockItemViewModel = await CreateViewModel(username.Data, username.Data[0], roleId.Data, result.Data);
			if (resStockItemViewModel.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resStockItemViewModel.ErrorMessage);
				string errorName = Enum.GetName(typeof(ErrorCodes), resStockItemViewModel.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, resStockItemViewModel.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			return View("/Views/StockItem.cshtml", resStockItemViewModel.Data);
		}

		[HttpPost]
		[Route("/stockItemsReassessWithoutCoeff")]
		public async Task<ActionResult> ReassessWithoutCoeff(int stockItemId, decimal newPrice, string documentNumber,
			DateTime documentDate, DateTime operationDate)
		{
			Result<StockItem> result = new Result<StockItem>();
			Result<string> resToken = GetTokenFromCookies();
			if (resToken.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resToken.ErrorMessage);
				result.ErrorCode = resToken.ErrorCode;
				result.ErrorMessage = resToken.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			ReassessmentWithoutCoeffParameters param = new ReassessmentWithoutCoeffParameters(stockItemId, newPrice, documentNumber,
				documentDate, operationDate);
			string url = _connectionString + "stockItemsReassessWithoutCoeff";
			using (HttpClient client = new HttpClient())
			{
				JsonContent content = JsonContent.Create(param);

				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.PostAsync(url, content);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<StockItem>();
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
					ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, result.ErrorMessage);
					return View("/Views/Shared/Error.cshtml", errorModel);
				}

				return RedirectToAction("GetAllStockItems");
			}
		}

		[HttpPost]
		[Route("/stockItemsReassessWithCoeff")]
		public async Task<ActionResult> ReassessWithCoeff(int sectorId, decimal coeff, string documentNumber,
			DateTime documentDate, DateTime operationDate)
		{
			Result<List<StockItem>> result = new Result<List<StockItem>>();
			Result<string> resToken = GetTokenFromCookies();
			if (resToken.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resToken.ErrorMessage);
				result.ErrorCode = resToken.ErrorCode;
				result.ErrorMessage = resToken.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			ReassessmentWithCoeffParameters param = new ReassessmentWithCoeffParameters(sectorId, coeff, documentNumber,
				documentDate, operationDate);

			string url = _connectionString + "stockItemsReassessWithCoeff";
			using (HttpClient client = new HttpClient())
			{
				JsonContent content = JsonContent.Create(param);

				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.PostAsync(url, content);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<List<StockItem>>();
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
					ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, result.ErrorMessage);
					return View("/Views/Shared/Error.cshtml", errorModel);
				}

				return RedirectToAction("GetAllStockItems");
			}
		}

		[HttpGet]
		[Route("/filterStockItems")]
		public async Task<IActionResult> FilterStockItems([FromQuery] List<int> sector, [FromQuery] List<int> contragent)
		{
			Result<List<StockItem>> result = new Result<List<StockItem>>();
			Result<string> resToken = GetTokenFromCookies();
			if (resToken.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resToken.ErrorMessage);
				result.ErrorCode = resToken.ErrorCode;
				result.ErrorMessage = resToken.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			Result<string> username = GetUsernameFromSession();
			if (username.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(username.ErrorMessage);
				result.ErrorCode = username.ErrorCode;
				result.ErrorMessage = username.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), username.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, username.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			Result<int> roleId = GetRoleIdFromSession();
			if (roleId.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(roleId.ErrorMessage);
				result.ErrorCode = roleId.ErrorCode;
				result.ErrorMessage = roleId.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), roleId.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, roleId.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}

			string url = _connectionString + "filterStockItems";
			string sectorParameters = url + "?" + GetStringWithParameters("sectors", sector);
			string contragentParameters = url + "?" + GetStringWithParameters("contragents", contragent);
			string urlWithParameters = url + "?" + sectorParameters + "&" + contragentParameters;

			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.GetAsync(urlWithParameters);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<List<StockItem>>();
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
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			Result<StockItemViewModel> resStockItemViewModel = await CreateViewModel(username.Data, username.Data[0], roleId.Data, result.Data);
			if (resStockItemViewModel.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resStockItemViewModel.ErrorMessage);
				string errorName = Enum.GetName(typeof(ErrorCodes), resStockItemViewModel.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, resStockItemViewModel.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			return View("/Views/StockItems/StockItem.cshtml", resStockItemViewModel.Data);
		}

		[HttpGet]
		[Route("/stockItemsByContragentIdAndSectorId")]
		public async Task<IActionResult> GetStockItemsByContragentIdAndSectorId([FromQuery] int contragentId, [FromQuery] int sectorId)
		{
			Result<List<StockItem>> result = new Result<List<StockItem>>();
			Result<string> resToken = GetTokenFromCookies();
			if (resToken.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resToken.ErrorMessage);
				result.ErrorCode = resToken.ErrorCode;
				result.ErrorMessage = resToken.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			Result<string> username = GetUsernameFromSession();
			if (username.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(username.ErrorMessage);
				result.ErrorCode = username.ErrorCode;
				result.ErrorMessage = username.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), username.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, username.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			Result<int> roleId = GetRoleIdFromSession();
			if (roleId.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(roleId.ErrorMessage);
				result.ErrorCode = roleId.ErrorCode;
				result.ErrorMessage = roleId.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), roleId.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, roleId.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}

			string urlWithParameters = _connectionString + "stockItemsByContragentIdAndSectorId" + "?" + $"contragentId={contragentId}" + "&" + $"sectorId={sectorId}";

			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.GetAsync(urlWithParameters);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<List<StockItem>>();
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
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			if (result.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(result.ErrorMessage);
				result.ErrorCode = (int)ErrorCodes.BadRequest;
				//result.ErrorMessage = "invalid username or password";
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, result.ErrorMessage);
				return PartialView("/Views/Shared/Error.cshtml", errorModel);
			}
			return PartialView("Views/Forms/InvoiceForm/InvoiceOut/InvoicePositionOutStockItems.cshtml", result.Data);
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
		private async Task<Result<StockItemViewModel>> CreateViewModel(string username, char usernameFirstLetter, int roleId, List<StockItem> stockItems)
		{
			Result<StockItemViewModel> result = new Result<StockItemViewModel>();
			//get list sector
			string sectorUrl = _connectionString + "sectors";
			Result<List<Sector>> resultSector = new Result<List<Sector>>();
			Result<string> resToken = GetTokenFromCookies();
			if (resToken.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resToken.ErrorMessage);
				result.ErrorCode = resToken.ErrorCode;
				result.ErrorMessage = resToken.ErrorMessage;
				return result;
			}
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.GetAsync(sectorUrl);
				if (responseMessage.IsSuccessStatusCode)
				{
					resultSector.Data = await responseMessage.Content.ReadFromJsonAsync<List<Sector>>();
				}
				else
				{
					resultSector.ErrorCode = (int)responseMessage.StatusCode;
					resultSector.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
				}
			}
			if (resultSector.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(result.ErrorMessage);
				result.ErrorCode = (int)ErrorCodes.BadRequest;
				result.ErrorMessage = "can't get all sectors";
				return result;
			}
			//get list contragent 
			string contragentUrl = _connectionString + "contragents";
			Result<List<Contragent>> resultContragent = new Result<List<Contragent>>();
			resToken = GetTokenFromCookies();
			if (resToken.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resToken.ErrorMessage);
				result.ErrorCode = resToken.ErrorCode;
				result.ErrorMessage = resToken.ErrorMessage;
				return result;
			}
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.GetAsync(contragentUrl);
				if (responseMessage.IsSuccessStatusCode)
				{
					resultContragent.Data = await responseMessage.Content.ReadFromJsonAsync<List<Contragent>>();
				}
				else
				{
					resultContragent.ErrorCode = (int)responseMessage.StatusCode;
					resultContragent.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
				}
			}
			if (resultContragent.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(result.ErrorMessage);
				result.ErrorCode = (int)ErrorCodes.BadRequest;
				result.ErrorMessage = "can't get all contragents";
				return result;
			}
			result.Data = new StockItemViewModel(usernameFirstLetter, username, roleId, stockItems, resultSector.Data, resultContragent.Data);
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
		private Result<int> GetRoleIdFromSession()
		{
			Result<int> result = new Result<int>();
			int? roleId = HttpContext.Session.GetInt32("RoleId");
			if (!roleId.HasValue)
			{
				result.ErrorMessage = "Can't get roleId from session";
				result.ErrorCode = (int)ErrorCodes.BadRequest;
				_logger.LogError(result.ErrorMessage);
			}
			else
			{
				result.Data = roleId.Value;
			}
			return result;
		}

		private string GetStringWithParameters(string listName, List<int> parameters)
		{
			string urlWithParameters = "";
			foreach (int sectorId in parameters)
			{
				urlWithParameters = urlWithParameters + $"{listName}={sectorId}&";
			}
			urlWithParameters = urlWithParameters.Trim('&');
			return urlWithParameters;
		}
	}
}
