using DiplomaWebService.Common.Enum;
using DiplomaWebService.Common.Results;
using DiplomaWebService.Constants;
using DiplomaWebService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaWebService.Controllers
{
    public class SectorController : Controller
    {
        private readonly ILogger<SectorController> _logger;
        private readonly string? _connectionString;

        public SectorController(ILogger<SectorController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString(Constant.MainConnectionString);
        }

        [HttpGet]
        [Route("/sectors")]
        public async Task<IActionResult> GetAllSectors()
        {
            Result<List<Sector>> result = new Result<List<Sector>>();
            string url = _connectionString + "sectors";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url);
                if (responseMessage.IsSuccessStatusCode)
                {
                    result.Data = await responseMessage.Content.ReadFromJsonAsync<List<Sector>>();
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
                result.ErrorMessage = "invalid username or password";
                _logger.LogError(result.ErrorMessage);
            }

            return View(result.Data);
        }

        [HttpPost]
        [Route("/sectors")]
        public async Task<IActionResult> CreateSector(string name)
        {
            Result<Sector> result = new Result<Sector>();
            string url = _connectionString + "sectors";
            using (HttpClient client = new HttpClient())
            {
                var requestBody = new { Name = name };
                JsonContent content = JsonContent.Create(requestBody);

                HttpResponseMessage responseMessage = await client.PostAsync(url, content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    result.Data = await responseMessage.Content.ReadFromJsonAsync<Sector>();
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

        [HttpPut]
        [Route("/sectors")]
        public async Task<IActionResult> UpdateSector(int id, string name)
        {
            Result<Sector> result = new Result<Sector>();
            string url = _connectionString + "sectors";
            using (HttpClient client = new HttpClient())
            {
                var requestBody = new { Id = id, Name = name };
                JsonContent content = JsonContent.Create(requestBody);

                HttpResponseMessage responseMessage = await client.PutAsync(url, content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    result.Data = await responseMessage.Content.ReadFromJsonAsync<Sector>();
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
        [Route("/sector")]
        public IActionResult GetCreateSector()
        {
            return View("/Views/Forms/SectorForm/AddSector.cshtml");
        }
    }
}
