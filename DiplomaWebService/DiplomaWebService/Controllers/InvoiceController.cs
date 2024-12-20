using DiplomaWebService.Common.Enum;
using DiplomaWebService.Common.Results;
using DiplomaWebService.Constants;
using DiplomaWebService.Models;
using DiplomaWebService.Models.Invoice;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaWebService.Controllers
{
	public class InvoiceController : Controller
	{
		private readonly ILogger<InvoiceController> _logger;
		private readonly string? _connectionString;

		public InvoiceController(ILogger<InvoiceController> logger, IConfiguration configuration)
		{
			_logger = logger;
			_connectionString = configuration.GetConnectionString(Constant.MainConnectionString);
		}

		[HttpGet]
		[Route("/invoices")]
		public async Task<IActionResult> GetAllInvoices()
		{
			Result<List<Invoice>> result = new Result<List<Invoice>>();
			string url = _connectionString + "invoices";
			using (HttpClient client = new HttpClient())
			{
				HttpResponseMessage responseMessage = await client.GetAsync(url);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<List<Invoice>>();
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

			return View("/Views/Invoices/Invoice.cshtml", result.Data);
		}
		[HttpGet]
		[Route("/invoice")]
		public IActionResult GetCreateInvoice()
		{
			return View("/Views/Forms/InvoiceForm/AddInvoice.cshtml");
		}
	}
}
