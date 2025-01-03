using DiplomaWebService.Common.Enum;
using DiplomaWebService.Common.Results;
using DiplomaWebService.Constants;
using DiplomaWebService.Models;
using DiplomaWebService.Parametrs.StockItem.Reassessment;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaWebService.Controllers
{
    public class StockItemController : Controller
    {
        private ILogger<StockItemController> _logger;
        private string? _connectionString;

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
            string url = _connectionString + "stockItems";
            using (HttpClient client = new HttpClient())
            {
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
                result.ErrorMessage = "ican't get all sectors";
                string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
                ErrorViewModel errorModel = new ErrorViewModel(errorName, result.ErrorMessage);
                return View("/Views/Shared/Error.cshtml", errorModel);
            }
            ViewData["/stockItems"] = result.Data;
            return View("/Views/StockItem.cshtml", result.Data);
        }

        [HttpPost]
        [Route("/stockItemsReassessWithoutCoeff")]
        public async Task<ActionResult> ReassessWithoutCoeff(decimal oldPrice, decimal newPrice)
        {
            ReassessmentWithoutCoeffParameters param = new ReassessmentWithoutCoeffParameters(oldPrice, newPrice);
            Result<List<StockItem>> result = new Result<List<StockItem>>();
            string url = _connectionString + "stockItemsReassessWithoutCoeff";
            using (HttpClient client = new HttpClient())
            {
                JsonContent content = JsonContent.Create(param);

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
                    result.ErrorMessage = "";
                    string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
                    ErrorViewModel errorModel = new ErrorViewModel(errorName, result.ErrorMessage);
                    return View("/Views/Shared/Error.cshtml", errorModel);
                }

                return RedirectToAction("GetAllStockItems");
            }
        }

        [HttpPost]
        [Route("/stockItemsReassessWithCoeff")]
        public async Task<ActionResult> ReassessWithCoeff(decimal oldPrice, decimal coeff)
        {
            ReassessmentWithCoeffParameters param = new ReassessmentWithCoeffParameters(oldPrice, coeff);
            Result<List<StockItem>> result = new Result<List<StockItem>>();
            string url = _connectionString + "stockItemsReassessWithCoeff";
            using (HttpClient client = new HttpClient())
            {
                JsonContent content = JsonContent.Create(param);

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
                    result.ErrorMessage = "";
                    string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
                    ErrorViewModel errorModel = new ErrorViewModel(errorName, result.ErrorMessage);
                    return View("/Views/Shared/Error.cshtml", errorModel);
                }

                return RedirectToAction("GetAllStockItems");
            }
        }
    }
}
