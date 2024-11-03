using DiplomaWebService.Common.Enum;
using DiplomaWebService.Common.Results;
using DiplomaWebService.Constants;
using DiplomaWebService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaWebService.Controllers
{
    public class ItemController : Controller
    {
        private readonly ILogger<ItemController> _logger;
        private readonly string? _connectionString;

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
            string url = _connectionString + "items";
            using (HttpClient client = new HttpClient())
            {
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
                result.ErrorCode = (int)ErrorCodes.BadRequest;
                result.ErrorMessage = "";
                _logger.LogError(result.ErrorMessage);
            }

            return View(result.Data);
        }

        [HttpPost]
        [Route("/items")]
        public async Task<IActionResult> CreateItem(string name, string inventoryNumber, int sectorId)
        {
            Result<Item> result = new Result<Item>();
            string url = _connectionString + "items";
            using (HttpClient client = new HttpClient())
            {
                var requestBody = new
                {
                    Name = name,
                    inventoryNumber = inventoryNumber,
                    sectorId = sectorId
                };
                JsonContent content = JsonContent.Create(requestBody);

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
                    result.ErrorCode = (int)ErrorCodes.BadRequest;
                    result.ErrorMessage = "";
                    _logger.LogError(result.ErrorMessage);
                }

                return View(result.Data);
            }
        }

        [HttpPut]
        [Route("/items")]
        public async Task<IActionResult> UpdateItem(int id, string name, string inventoryNumber, int sectorId)
        {
            Result<Item> result = new Result<Item>();
            string url = _connectionString + "items";
            using (HttpClient client = new HttpClient())
            {
                var requestBody = new
                {
                    Id = id,
                    Name = name,
                    inventoryNumber = inventoryNumber,
                    sectorId = sectorId
                };
                JsonContent content = JsonContent.Create(requestBody);

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
                if (result.ErrorCode != (int)ErrorCodes.Success)
                {
                    result.ErrorCode = (int)ErrorCodes.BadRequest;
                    result.ErrorMessage = "";
                    _logger.LogError(result.ErrorMessage);
                }

                return View(result.Data);
            }
        }

        [HttpGet]
        [Route("/item")]
        public IActionResult GetCreateItem()
        {
            return View("/Views/Forms/ItemForm/AddItem.cshtml");
        }
    }
}
