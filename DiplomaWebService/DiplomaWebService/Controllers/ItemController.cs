using DiplomaWebService.Common.Enum;
using DiplomaWebService.Common.Results;
using DiplomaWebService.Constants;
using DiplomaWebService.Models;
using DiplomaWebService.Models.Items;
using DiplomaWebService.Parametrs.Item;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaWebService.Controllers
{
	public class ItemController : Controller
	{
		private ILogger<ItemController> _logger;
		private string? _connectionString;

		public ItemController(ILogger<ItemController> logger, IConfiguration configuration)
		{
			_logger = logger;
			_connectionString = configuration.GetConnectionString(Constant.MainConnectionString);
		}

		[HttpGet]
		[Route("/items")]
		public async Task<IActionResult> GetAllItems()
		{
			Result<List<Item>> result = new Result<List<Item>>();
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
			string url = _connectionString + "items";
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.GetAsync(url);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<List<Item>>();
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
				result.ErrorMessage = "can't get all items";
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}

			Result<ItemViewModel> resItemViewModel = await GetItemViewModel(result.Data);
			if (resItemViewModel.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resItemViewModel.ErrorMessage);
				string errorName = Enum.GetName(typeof(ErrorCodes), resItemViewModel.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(errorName, resItemViewModel.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			return View("/Views/Dictionaries/Items/Item.cshtml", resItemViewModel.Data);
		}

		[HttpPost]
		[Route("/items")]
		public async Task<IActionResult> CreateItem(string name, int? inventoryNumber, int sectorId)
		{
			Result<Item> result = new Result<Item>();
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
			string url = _connectionString + "items";
			using (HttpClient client = new HttpClient())
			{
				ItemCreateParameters itemCreateParam = new ItemCreateParameters(name, sectorId, inventoryNumber);
				JsonContent content = JsonContent.Create(itemCreateParam);

				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.PostAsync(url, content);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<Item>();
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
					result.ErrorMessage = "";
					string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
					ErrorViewModel errorModel = new ErrorViewModel(errorName, result.ErrorMessage);
					return View("/Views/Shared/Error.cshtml", errorModel);
				}

				return RedirectToAction("GetAllItems");
			}
		}

		[HttpPut]
		[Route("/items/{id}")]
		public async Task<IActionResult> UpdateItem(int id, string name, int inventoryNumber, int sectorId)
		{
			Result<Item> result = new Result<Item>();
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
			string url = _connectionString + $"/items/{id}";
			using (HttpClient client = new HttpClient())
			{
				ItemUpdateParameters itemUpdateParam = new ItemUpdateParameters(id, name, sectorId, inventoryNumber);
				JsonContent content = JsonContent.Create(itemUpdateParam);

				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.PutAsync(url, content);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<Item>();
				}
				else
				{
					result.ErrorCode = (int)responseMessage.StatusCode;
					result.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
				}
				return RedirectToAction("GetAllItems");
			}
		}

		[HttpGet]
		[Route("/item")]
		public IActionResult GetCreateItem()
		{
			return View("/Views/Forms/ItemForm/AddItem.cshtml");
		}

		private async Task<Result<ItemViewModel>> GetItemViewModel(List<Item> items)
		{
			Result<ItemViewModel> result = new Result<ItemViewModel>();
			result.Data = new ItemViewModel();

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
			result.Data.Items = items;
			result.Data.Sectors = resultSector.Data;
			return result;
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
	}
}
