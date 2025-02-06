using DiplomaWebService.Common.Enum;
using DiplomaWebService.Common.Results;
using DiplomaWebService.Constants;
using DiplomaWebService.Models;
using DiplomaWebService.Models.Invoice.Out;
using DiplomaWebService.Models.Invoice.ViewModel;
using DiplomaWebService.Models.Types;
using DiplomaWebService.Parametrs.Invoice.Out;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaWebService.Controllers.Invoice
{
	public class InvoiceOutController : Controller
	{
		private ILogger<InvoiceOutController> _logger;
		private string? _connectionString;

		public InvoiceOutController(ILogger<InvoiceOutController> logger, IConfiguration configuration)
		{
			_logger = logger;
			_connectionString = configuration.GetConnectionString(Constant.MainConnectionString);
		}

		[HttpGet]
		[Route("/invoicesOut")]
		public async Task<IActionResult> GetAllInvoicesOut()
		{
			Result<List<InvoiceOut>> result = new Result<List<InvoiceOut>>();
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
			string url = _connectionString + "invoicesOut";
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.GetAsync(url);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<List<InvoiceOut>>();
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
				result.ErrorMessage = "can't get all invoices";
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			Result<InvoiceOutViewModel> invoiceOutModel = await GetInvoiceOutModel(result.Data);
			BaseViewModel model = CreateBaseViewModel(username.Data, username.Data[0]);
			ViewData["LayoutModel"] = model;
			return View("/Views/Invoices/InvoiceOut.cshtml", invoiceOutModel.Data);
		}

		[HttpPost]
		[Route("/invoiceOut")]
		public async Task<IActionResult> CreateInvoiceOut(DateTime invoiceDate, string number, int destinationId, int senderId,
			int sectorId, int documentTypeId, List<InvoicePositionsOutCreateParameters> invoicePositions)
		{
			int invoiceTypeId = 2;
			Result<InvoiceOut> result = new Result<InvoiceOut>();
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
			string url = _connectionString + "invoicesOut";
			using (HttpClient client = new HttpClient())
			{
				InvoiceOutCreateParameters invoiceCreateParam = new InvoiceOutCreateParameters(invoiceDate, number, destinationId,
					senderId, invoiceTypeId, sectorId, documentTypeId, invoicePositions);
				JsonContent content = JsonContent.Create(invoiceCreateParam);

				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.PostAsync(url, content);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<InvoiceOut>();
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
				result.ErrorMessage = "can't get all invoices";
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			return RedirectToAction("GetAllInvoicesOut");
		}
		[HttpGet]
		[Route("/invoiceOut")]
		public IActionResult GetCreateInvoiceOut()
		{
			return View("/Views/Forms/InvoiceForm/AddInvoiceOut.cshtml");
		}
		private async Task<Result<InvoiceOutViewModel>> GetInvoiceOutModel(List<InvoiceOut> invoices)
		{
			Result<InvoiceOutViewModel> invoiceModel = new Result<InvoiceOutViewModel>();
			Result<string> resToken = GetTokenFromCookies();
			if (resToken.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resToken.ErrorMessage);
				invoiceModel.ErrorCode = resToken.ErrorCode;
				invoiceModel.ErrorMessage = resToken.ErrorMessage;
				return invoiceModel;
			}
			invoiceModel.Data = new InvoiceOutViewModel();
			invoiceModel.Data.InvoicesOut = invoices;

			//get sectors
			string sectorUrl = _connectionString + "sectors";
			Result<List<Sector>> resultSector = new Result<List<Sector>>();
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
				_logger.LogError(resultSector.ErrorMessage);
				invoiceModel.ErrorCode = (int)ErrorCodes.BadRequest;
				invoiceModel.ErrorMessage = "can't get all sectors";
				return invoiceModel;
			}
			invoiceModel.Data.Sectors = resultSector.Data;

			//get document types
			string documentTypeUrl = _connectionString + "documentTypes";
			Result<List<DocumentType>> resultDocumentType = new Result<List<DocumentType>>();
			using (HttpClient client = new HttpClient())
			{
				HttpResponseMessage responseMessage = await client.GetAsync(documentTypeUrl);
				if (responseMessage.IsSuccessStatusCode)
				{
					resultDocumentType.Data = await responseMessage.Content.ReadFromJsonAsync<List<DocumentType>>();
				}
				else
				{
					resultDocumentType.ErrorCode = (int)responseMessage.StatusCode;
					resultDocumentType.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
				}
			}
			if (resultDocumentType.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resultDocumentType.ErrorMessage);
				invoiceModel.ErrorCode = (int)ErrorCodes.BadRequest;
				invoiceModel.ErrorMessage = "can't get all document types";
				return invoiceModel;
			}
			invoiceModel.Data.DocumentTypes = resultDocumentType.Data;

			//get invoice types
			string invoiceTypeUrl = _connectionString + "invoiceTypes";
			Result<List<InvoiceType>> resultInvoiceType = new Result<List<InvoiceType>>();
			using (HttpClient client = new HttpClient())
			{
				HttpResponseMessage responseMessage = await client.GetAsync(invoiceTypeUrl);
				if (responseMessage.IsSuccessStatusCode)
				{
					resultInvoiceType.Data = await responseMessage.Content.ReadFromJsonAsync<List<InvoiceType>>();
				}
				else
				{
					resultInvoiceType.ErrorCode = (int)responseMessage.StatusCode;
					resultInvoiceType.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
				}
			}
			if (resultInvoiceType.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resultDocumentType.ErrorMessage);
				invoiceModel.ErrorCode = (int)ErrorCodes.BadRequest;
				invoiceModel.ErrorMessage = "can't get all invoice types";
				return invoiceModel;
			}
			invoiceModel.Data.InvoiceTypes = resultInvoiceType.Data;

			//contragents
			string contragnetUrl = _connectionString + "contragents";
			Result<List<Contragent>> resultContragent = new Result<List<Contragent>>();
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.GetAsync(contragnetUrl);
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
				_logger.LogError(resultContragent.ErrorMessage);
				invoiceModel.ErrorCode = (int)ErrorCodes.BadRequest;
				invoiceModel.ErrorMessage = "can't get all contragents";
				return invoiceModel;
			}
			invoiceModel.Data.Contragents = resultContragent.Data;

			//get stockItem
			string url = _connectionString + "stockItems";
			Result<List<StockItem>> resultStockItem = new Result<List<StockItem>>();
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.GetAsync(url);
				if (responseMessage.IsSuccessStatusCode)
				{
					resultStockItem.Data = await responseMessage.Content.ReadFromJsonAsync<List<StockItem>>();
				}
				else
				{
					resultStockItem.ErrorCode = (int)responseMessage.StatusCode;
					resultStockItem.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
				}
			}
			if (resultStockItem.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resultStockItem.ErrorMessage);
				invoiceModel.ErrorCode = (int)ErrorCodes.BadRequest;
				invoiceModel.ErrorMessage = "can't get all  Stock Items";
				return invoiceModel;
			}
			invoiceModel.Data.StockItems = resultStockItem.Data;

			return invoiceModel;

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
