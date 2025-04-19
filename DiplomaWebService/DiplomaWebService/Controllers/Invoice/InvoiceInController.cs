using DiplomaWebService.Common.Enum;
using DiplomaWebService.Common.Results;
using DiplomaWebService.Constants;
using DiplomaWebService.Models;
using DiplomaWebService.Models.Invoice.In;
using DiplomaWebService.Models.Items;
using DiplomaWebService.Models.Types;
using DiplomaWebService.Models.ViewModel.Invoice.In;
using DiplomaWebService.Parametrs.Invoice.In;
using DiplomaWebService.Request.Invoice;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaWebService.Controllers.Invoice
{
	public class InvoiceInController : Controller
	{
		private ILogger<InvoiceInController> _logger;
		private string? _connectionString;
		private string _username = " ";
		private char _usernameFirstLetter = ' ';
		private int _roleId = (int)Roles.Viewer;

		public InvoiceInController(ILogger<InvoiceInController> logger, IConfiguration configuration)
		{
			_logger = logger;
			_connectionString = configuration.GetConnectionString(Constant.MainConnectionString);
		}

		[HttpGet]
		[Route("/invoicesIn")]
		public async Task<IActionResult> GetAllInvoicesIn()
		{
			Result<List<InvoiceIn>> result = new Result<List<InvoiceIn>>();
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
			string url = _connectionString + "invoicesIn";
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.GetAsync(url);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<List<InvoiceIn>>();
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
				//result.ErrorMessage = "can't get all invoices";
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			Result<InvoiceInViewModelInvoiceList> invoiceInModelInvoiceList = await GetInvoiceInModelListInvoice(result.Data, username.Data, roleId.Data, username.Data[0]);
			if (invoiceInModelInvoiceList.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(result.ErrorMessage);
				result.ErrorCode = (int)ErrorCodes.BadRequest;
				//result.ErrorMessage = "can't get all invoices";
				string errorName = Enum.GetName(typeof(ErrorCodes), invoiceInModelInvoiceList.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, invoiceInModelInvoiceList.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			return View("/Views/Invoices/InvoiceIn.cshtml", invoiceInModelInvoiceList.Data);
		}

		[HttpGet]
		[Route("/invoiceIn/{id}")]
		public async Task<IActionResult> GetInvoiceInById(int id)
		{
			Result<InvoiceIn> result = new Result<InvoiceIn>();
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
			string url = _connectionString + $"invoiceIn/{id}";
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.GetAsync(url);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<InvoiceIn>();
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
				//result.ErrorMessage = "can't get all invoices";
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			Result<InvoiceInViewModelInvoice> invoiceInModel = await GetInvoiceInModelInvoice(result.Data, username.Data, roleId.Data, username.Data[0]);
			if (invoiceInModel.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(result.ErrorMessage);
				result.ErrorCode = (int)ErrorCodes.BadRequest;
				//result.ErrorMessage = "can't get all invoices";
				string errorName = Enum.GetName(typeof(ErrorCodes), invoiceInModel.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, invoiceInModel.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			return View("/Views/Invoices/InvoiceDetails/InvoiceInDetails.cshtml", invoiceInModel.Data);
		}

		[HttpPost]
		[Route("/invoiceIn")]
		public async Task<IActionResult> CreateInvoiceIn([FromBody] InvoiceInCreateRequest invoiceInCreateRequest)
		{
			int invoiceTypeId = 1;
			Result<InvoiceIn> result = new Result<InvoiceIn>();
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
			string url = _connectionString + "invoicesIn";
			using (HttpClient client = new HttpClient())
			{
				InvoiceInCreateParameters invoiceCreateParam = new InvoiceInCreateParameters(invoiceInCreateRequest.InvoiceDate, invoiceInCreateRequest.Number,
					invoiceInCreateRequest.DestinationId, invoiceInCreateRequest.SenderId, invoiceTypeId,
					invoiceInCreateRequest.SectorId, invoiceInCreateRequest.DocumentTypeId, invoiceInCreateRequest.Positions);
				JsonContent content = JsonContent.Create(invoiceCreateParam);

				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.PostAsync(url, content);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<InvoiceIn>();
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
				//result.ErrorMessage = "can't get all invoices";
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			return RedirectToAction("GetAllInvoicesIn");
		}

		[HttpGet]
		[Route("/searchInvoicesIn/{number}")]
		public async Task<IActionResult> SearchInvoiceIn(string number)
		{
			Result<List<InvoiceIn>> result = new Result<List<InvoiceIn>>();
			Result<string> resToken = GetTokenFromCookies();
			if (resToken.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resToken.ErrorMessage);
				result.ErrorCode = resToken.ErrorCode;
				result.ErrorMessage = resToken.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, result.ErrorMessage);
				return PartialView("/Views/Shared/Error.cshtml", errorModel);
			}
			Result<string> username = GetUsernameFromSession();
			if (username.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(username.ErrorMessage);
				result.ErrorCode = username.ErrorCode;
				result.ErrorMessage = username.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), username.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, username.ErrorMessage);
				return PartialView("/Views/Shared/Error.cshtml", errorModel);
			}
			Result<int> roleId = GetRoleIdFromSession();
			if (roleId.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(roleId.ErrorMessage);
				result.ErrorCode = roleId.ErrorCode;
				result.ErrorMessage = roleId.ErrorMessage;
				string errorName = Enum.GetName(typeof(ErrorCodes), roleId.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, roleId.ErrorMessage);
				return PartialView("/Views/Shared/Error.cshtml", errorModel);
			}
			string url = _connectionString + $"searchInvoicesIn/{number}";
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.GetAsync(url);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<List<InvoiceIn>>();
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
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, result.ErrorMessage);
				return PartialView("/Views/Shared/Error.cshtml", errorModel);
			}
			return PartialView("/Views/Invoices/_InvoiceInList.cshtml", result.Data);
		}

		[HttpGet]
		[Route("/invoiceIn")]
		public async Task<IActionResult> GetCreateInvoiceIn()
		{
			Result<List<InvoiceIn>> result = new Result<List<InvoiceIn>>();
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
			string url = _connectionString + "invoicesIn";
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.GetAsync(url);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<List<InvoiceIn>>();
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
				//result.ErrorMessage = "can't get all invoices";
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			Result<InvoiceInViewModelInvoiceList> invoiceInModelInvoiceList = await GetInvoiceInModelListInvoice(result.Data, username.Data, roleId.Data, username.Data[0]);
			if (invoiceInModelInvoiceList.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(result.ErrorMessage);
				result.ErrorCode = (int)ErrorCodes.BadRequest;
				//result.ErrorMessage = "can't get all invoices";
				string errorName = Enum.GetName(typeof(ErrorCodes), invoiceInModelInvoiceList.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, _roleId, errorName, invoiceInModelInvoiceList.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			return View("/Views/Forms/InvoiceForm/InvoiceIn/AddInvoiceIn.cshtml", invoiceInModelInvoiceList.Data);
		}
		private async Task<Result<InvoiceInViewModelInvoiceList>> GetInvoiceInModelListInvoice(List<InvoiceIn> invoices, string username, int roleId, char usernameFirstLetter)
		{
			Result<InvoiceInViewModelInvoiceList> invoiceModel = new Result<InvoiceInViewModelInvoiceList>();
			Result<string> resToken = GetTokenFromCookies();
			if (resToken.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resToken.ErrorMessage);
				invoiceModel.ErrorCode = resToken.ErrorCode;
				invoiceModel.ErrorMessage = resToken.ErrorMessage;
				return invoiceModel;
			}
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

			//get items
			string itemUrl = _connectionString + "items";
			Result<List<Item>> resultItem = new Result<List<Item>>();
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.GetAsync(itemUrl);
				if (responseMessage.IsSuccessStatusCode)
				{
					resultItem.Data = await responseMessage.Content.ReadFromJsonAsync<List<Item>>();
				}
				else
				{
					resultItem.ErrorCode = (int)responseMessage.StatusCode;
					resultItem.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
				}
			}
			if (resultItem.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resultItem.ErrorMessage);
				invoiceModel.ErrorCode = (int)ErrorCodes.BadRequest;
				invoiceModel.ErrorMessage = "can't get all items";
				return invoiceModel;
			}

			//get units
			string unitUrl = _connectionString + "units";
			Result<List<Unit>> resultUnit = new Result<List<Unit>>();
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.GetAsync(unitUrl);
				if (responseMessage.IsSuccessStatusCode)
				{
					resultUnit.Data = await responseMessage.Content.ReadFromJsonAsync<List<Unit>>();
				}
				else
				{
					resultUnit.ErrorCode = (int)responseMessage.StatusCode;
					resultUnit.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
				}
			}
			if (resultUnit.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resultUnit.ErrorMessage);
				invoiceModel.ErrorCode = (int)ErrorCodes.BadRequest;
				invoiceModel.ErrorMessage = "can't get all units";
				return invoiceModel;
			}

			//get categories
			string categoryUrl = _connectionString + "categories";
			Result<List<Category>> resultCategory = new Result<List<Category>>();
			using (HttpClient client = new HttpClient())
			{
				HttpResponseMessage responseMessage = await client.GetAsync(categoryUrl);
				if (responseMessage.IsSuccessStatusCode)
				{
					resultCategory.Data = await responseMessage.Content.ReadFromJsonAsync<List<Category>>();
				}
				else
				{
					resultCategory.ErrorCode = (int)responseMessage.StatusCode;
					resultCategory.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
				}
			}
			if (resultCategory.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resultCategory.ErrorMessage);
				invoiceModel.ErrorCode = (int)ErrorCodes.BadRequest;
				invoiceModel.ErrorMessage = "can't get all categories";
				return invoiceModel;
			}
			invoiceModel.Data = new InvoiceInViewModelInvoiceList(usernameFirstLetter, username, roleId, invoices, resultSector.Data, resultDocumentType.Data,
				resultContragent.Data, resultItem.Data, resultUnit.Data, resultCategory.Data);
			return invoiceModel;

		}

		private async Task<Result<InvoiceInViewModelInvoice>> GetInvoiceInModelInvoice(InvoiceIn invoice, string username, int roleId, char usernameFirstLetter)
		{
			Result<InvoiceInViewModelInvoice> invoiceModel = new Result<InvoiceInViewModelInvoice>();
			Result<string> resToken = GetTokenFromCookies();
			if (resToken.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resToken.ErrorMessage);
				invoiceModel.ErrorCode = resToken.ErrorCode;
				invoiceModel.ErrorMessage = resToken.ErrorMessage;
				return invoiceModel;
			}
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

			//get items
			string itemUrl = _connectionString + "items";
			Result<List<Item>> resultItem = new Result<List<Item>>();
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.GetAsync(itemUrl);
				if (responseMessage.IsSuccessStatusCode)
				{
					resultItem.Data = await responseMessage.Content.ReadFromJsonAsync<List<Item>>();
				}
				else
				{
					resultItem.ErrorCode = (int)responseMessage.StatusCode;
					resultItem.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
				}
			}
			if (resultItem.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resultItem.ErrorMessage);
				invoiceModel.ErrorCode = (int)ErrorCodes.BadRequest;
				invoiceModel.ErrorMessage = "can't get all items";
				return invoiceModel;
			}

			//get units
			string unitUrl = _connectionString + "units";
			Result<List<Unit>> resultUnit = new Result<List<Unit>>();
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", resToken.Data);
				HttpResponseMessage responseMessage = await client.GetAsync(unitUrl);
				if (responseMessage.IsSuccessStatusCode)
				{
					resultUnit.Data = await responseMessage.Content.ReadFromJsonAsync<List<Unit>>();
				}
				else
				{
					resultUnit.ErrorCode = (int)responseMessage.StatusCode;
					resultUnit.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
				}
			}
			if (resultUnit.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resultUnit.ErrorMessage);
				invoiceModel.ErrorCode = (int)ErrorCodes.BadRequest;
				invoiceModel.ErrorMessage = "can't get all units";
				return invoiceModel;
			}

			//get categories
			string categoryUrl = _connectionString + "categories";
			Result<List<Category>> resultCategory = new Result<List<Category>>();
			using (HttpClient client = new HttpClient())
			{
				HttpResponseMessage responseMessage = await client.GetAsync(categoryUrl);
				if (responseMessage.IsSuccessStatusCode)
				{
					resultCategory.Data = await responseMessage.Content.ReadFromJsonAsync<List<Category>>();
				}
				else
				{
					resultCategory.ErrorCode = (int)responseMessage.StatusCode;
					resultCategory.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
				}
			}
			if (resultCategory.ErrorCode != (int)ErrorCodes.Success)
			{
				_logger.LogError(resultCategory.ErrorMessage);
				invoiceModel.ErrorCode = (int)ErrorCodes.BadRequest;
				invoiceModel.ErrorMessage = "can't get all categories";
				return invoiceModel;
			}
			invoiceModel.Data = new InvoiceInViewModelInvoice(usernameFirstLetter, username, roleId, invoice, resultSector.Data, resultDocumentType.Data,
				resultContragent.Data, resultItem.Data, resultUnit.Data, resultCategory.Data);
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
	}
}
