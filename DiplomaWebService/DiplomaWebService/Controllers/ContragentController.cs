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
        private readonly ILogger<ContragentController> _logger;
        private readonly string? _connectionString;

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
            string url = _connectionString + "contragents";
            using (HttpClient client = new HttpClient())
            {
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
                result.ErrorMessage = "Can't get all contragents";
                ErrorViewModel errorModel = new ErrorViewModel(result.ErrorCode, result.ErrorMessage);
                return View("/Views/Error.cshtml", errorModel);
            }

            return View("/Views/Dictionaries/Contragents/Contragent.cshtml", result.Data);
        }

        [HttpPost]
        [Route("/contragent")]
        public async Task<IActionResult> CreateContragent(string name, int? parentId, bool isActive)
        {
            Result<Contragent> result = new Result<Contragent>();
            ContragentCreateParameters contragentCreateParam = new ContragentCreateParameters(parentId, name, isActive);
            string url = _connectionString + "contragents";
            using (HttpClient client = new HttpClient())
            {
                JsonContent content = JsonContent.Create(contragentCreateParam);

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
                result.ErrorMessage = "";
                ErrorViewModel errorModel = new ErrorViewModel(result.ErrorCode, result.ErrorMessage);
                return View("/Views/Error.cshtml", errorModel);
            }

            return View("/Views/Dictionaries/Contragents/Contragent.cshtml", result.Data);
        }

        [HttpPut]
        [Route("/contragent")]
        public async Task<IActionResult> UpdateContragent(int id, string name, int? parentId, bool isActive)
        {
            Result<Contragent> result = new Result<Contragent>();
            string url = _connectionString + $"/contragent/{id}";
            using (HttpClient client = new HttpClient())
            {
                ContragentUpdateParameters contragentUpdateParam = new ContragentUpdateParameters(id, parentId, name, isActive);
                JsonContent content = JsonContent.Create(contragentUpdateParam);

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
                    result.ErrorMessage = "";
                    ErrorViewModel errorModel = new ErrorViewModel(result.ErrorCode, result.ErrorMessage);
                    return View("/Views/Error.cshtml", errorModel);
                }

                return View("/Views/Dictionaries/Contragents/Contragent.cshtml", result.Data);
            }
        }
        [HttpGet]
        [Route("/contragent")]
        public IActionResult GetCreateContragent()
        {
            return View("/Views/Forms/ContragentForm/AddContragent.cshtml");
        }
    }
}
