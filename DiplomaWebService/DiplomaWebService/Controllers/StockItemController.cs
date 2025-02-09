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
                ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, errorName, result.ErrorMessage);
                return View("/Views/Shared/Error.cshtml", errorModel);
            }
            StockItemViewModel model = CreateViewModel(username.Data, username.Data[0], result.Data);
            return View("/Views/StockItem.cshtml", model);
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
                ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, errorName, result.ErrorMessage);
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
                    ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, errorName, result.ErrorMessage);
                    return View("/Views/Shared/Error.cshtml", errorModel);
                }

                return RedirectToAction("GetAllStockItems");
            }
        }

        [HttpPost]
        [Route("/stockItemsReassessWithCoeff")]
        public async Task<ActionResult> ReassessWithCoeff(int stockItemId, decimal coeff, string documentNumber,
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
                ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, errorName, result.ErrorMessage);
                return View("/Views/Shared/Error.cshtml", errorModel);
            }
            ReassessmentWithCoeffParameters param = new ReassessmentWithCoeffParameters(stockItemId, coeff, documentNumber,
                documentDate, operationDate);

            string url = _connectionString + "stockItemsReassessWithCoeff";
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
                    ErrorViewModel errorModel = new ErrorViewModel(_usernameFirstLetter, _username, errorName, result.ErrorMessage);
                    return View("/Views/Shared/Error.cshtml", errorModel);
                }

                return RedirectToAction("GetAllStockItems");
            }
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
        private StockItemViewModel CreateViewModel(string username, char usernameFirstLetter, List<StockItem> stockItems)
        {
            StockItemViewModel model = new StockItemViewModel(usernameFirstLetter, username, stockItems);
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
