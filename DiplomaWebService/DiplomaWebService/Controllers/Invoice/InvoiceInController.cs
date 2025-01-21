using DiplomaWebService.Common.Enum;
using DiplomaWebService.Common.Results;
using DiplomaWebService.Constants;
using DiplomaWebService.Models;
using DiplomaWebService.Models.Invoice.In;
using DiplomaWebService.Models.Invoice.ViewModel;
using DiplomaWebService.Models.Items;
using DiplomaWebService.Models.Types;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaWebService.Controllers.Invoice
{
    public class InvoiceInController : Controller
    {
        private ILogger<InvoiceInController> _logger;
        private string? _connectionString;

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
            string url = _connectionString + "invoicesIn";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "asasasaas");
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
                result.ErrorMessage = "can't get all invoices";
                string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
                ErrorViewModel errorModel = new ErrorViewModel(errorName, result.ErrorMessage);
                return View("/Views/Shared/Error.cshtml", errorModel);
            }
            Result<InvoiceInViewModel> invoiceInModel = await GetInvoiceInModel(result.Data);
            return View("/Views/Invoices/InvoiceIn.cshtml", invoiceInModel.Data);
        }

        //[HttpPost]
        //[Route("/invoice")]
        //public async Task<IActionResult> CreateInvoiceIn()
        //{
        //    Result<InvoiceIn> result = new Result<InvoiceIn>();
        //    string url = _connectionString + "invoicesIn";
        //    using (HttpClient client = new HttpClient())
        //    {
        //        InvoicePositionsInCreateParameters invoicePosCreateParam = new InvoicePositionsInCreateParameters();
        //        InvoiceInCreateParameters invoiceCreateParam = new InvoiceInCreateParameters();
        //        JsonContent content = JsonContent.Create(invoiceCreateParam);

        //        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "asasasaas");
        //        HttpResponseMessage responseMessage = await client.PostAsync(url, content);
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            result.Data = await responseMessage.Content.ReadFromJsonAsync<InvoiceIn>();
        //        }
        //        else
        //        {
        //            result.ErrorCode = (int)responseMessage.StatusCode;
        //            result.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
        //        }
        //    }
        //    if (result.ErrorCode != (int)ErrorCodes.Success)
        //    {
        //        _logger.LogError(result.ErrorMessage);
        //        result.ErrorCode = (int)ErrorCodes.BadRequest;
        //        result.ErrorMessage = "can't get all invoices";
        //        string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
        //        ErrorViewModel errorModel = new ErrorViewModel(errorName, result.ErrorMessage);
        //        return View("/Views/Shared/Error.cshtml", errorModel);
        //    }
        //    return RedirectToAction("GetAllInvoicesIn");
        //}
        [HttpGet]
        [Route("/invoiceIn")]
        public IActionResult GetCreateInvoiceIn()
        {
            return View("/Views/Forms/InvoiceForm/AddInvoiceIn.cshtml");
        }
        private async Task<Result<InvoiceInViewModel>> GetInvoiceInModel(List<InvoiceIn> invoices)
        {
            Result<InvoiceInViewModel> invoiceModel = new Result<InvoiceInViewModel>();
            invoiceModel.Data = new InvoiceInViewModel();
            invoiceModel.Data.InvoicesIn = invoices;
            //get sectors
            string sectorUrl = _connectionString + "sectors";
            Result<List<Sector>> resultSector = new Result<List<Sector>>();
            using (HttpClient client = new HttpClient())
            {
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
            string invoiceTypeUrl = _connectionString + "documentTypes";
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

            //get items
            string itemUrl = _connectionString + "items";
            Result<List<Item>> resultItem = new Result<List<Item>>();
            using (HttpClient client = new HttpClient())
            {
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
            invoiceModel.Data.Items = resultItem.Data;

            //get units
            string unitUrl = _connectionString + "units";
            Result<List<Unit>> resultUnit = new Result<List<Unit>>();
            using (HttpClient client = new HttpClient())
            {
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
            invoiceModel.Data.Units = resultUnit.Data;

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
            invoiceModel.Data.Categories = resultCategory.Data;

            return invoiceModel;

        }
    }
}
