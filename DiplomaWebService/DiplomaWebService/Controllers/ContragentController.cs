using DiplomaWebService.Common.Enum;
using DiplomaWebService.Common.Results;
using DiplomaWebService.Constants;
using DiplomaWebService.Models;
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
                result.ErrorCode = (int)ErrorCodes.BadRequest;
                result.ErrorMessage = "";
                _logger.LogError(result.ErrorMessage);
            }

            return View(result.Data);
        }

        [HttpPost]
        [Route("/contragents")]
        public async Task<IActionResult> CreateContragent(string name, int? parentId)
        {
            Result<Contragent> result = new Result<Contragent>();
            string url = _connectionString + "contragents";
            using (HttpClient client = new HttpClient())
            {
                var requestBody = new { Name = name, ParentId = parentId };
                JsonContent content = JsonContent.Create(requestBody);

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
                result.ErrorCode = (int)ErrorCodes.BadRequest;
                result.ErrorMessage = "";
                _logger.LogError(result.ErrorMessage);
                return result;
            }

            //return result;
        }

        [HttpPut]
        [Route("/sectors")]
        public async Task<IActionResult> UpdateContragent(int id, string name, int? parentId)
        {
            Result<Contragent> result = new Result<Contragent>();
            string url = _connectionString + "contragents";
            using (HttpClient client = new HttpClient())
            {
                var requestBody = new { Id = id, Name = name, ParentId = parentId };
                JsonContent content = JsonContent.Create(requestBody);

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
                    result.ErrorCode = (int)ErrorCodes.BadRequest;
                    result.ErrorMessage = "";
                    _logger.LogError(result.ErrorMessage);
                }

                //return View(result.Data);
            }
        }
        [HttpGet]
        [Route("/contragent")]
        public IActionResult GetCreateSector()
        {
            return View("/Views/Forms/ContragentForm/AddContragent.cshtml");
        }
    }
}
