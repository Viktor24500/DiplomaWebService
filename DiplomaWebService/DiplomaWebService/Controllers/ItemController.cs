using DiplomaWebService.Common.Enum;
using DiplomaWebService.Common.Results;
using DiplomaWebService.Constants;
using DiplomaWebService.Models;
using DiplomaWebService.Parametrs.Item;
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
                _logger.LogError(result.ErrorMessage);
                result.ErrorCode = (int)ErrorCodes.BadRequest;
                result.ErrorMessage = "can't get all items";
                ErrorViewModel errorModel = new ErrorViewModel(result.ErrorCode, result.ErrorMessage);
                return View("/Views/Error.cshtml", errorModel);
            }

            return View("/Views/Dictionaries/Items/Item.cshtml", result.Data);
        }

        [HttpPost]
        [Route("/items")]
        public async Task<IActionResult> CreateItem(string name, int inventoryNumber, int sectorId)
        {
            Result<Item> result = new Result<Item>();
            string url = _connectionString + "items";
            using (HttpClient client = new HttpClient())
            {
                ItemCreateParameters itemCreateParam = new ItemCreateParameters(name, sectorId, inventoryNumber);
                JsonContent content = JsonContent.Create(itemCreateParam);

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
                    _logger.LogError(result.ErrorMessage);
                    result.ErrorCode = (int)ErrorCodes.BadRequest;
                    result.ErrorMessage = "";
                    ErrorViewModel errorModel = new ErrorViewModel(result.ErrorCode, result.ErrorMessage);
                    return View("/Views/Error.cshtml", errorModel);
                }

                return View("/Views/Dictionaries/Items/Item.cshtml", result.Data);
            }
        }

        [HttpPut]
        [Route("/items")]
        public async Task<IActionResult> UpdateItem(int id, string name, int inventoryNumber, int sectorId)
        {
            Result<Item> result = new Result<Item>();
            string url = _connectionString + $"/items/{id}";
            using (HttpClient client = new HttpClient())
            {
                ItemUpdateParameters itemUpdateParam = new ItemUpdateParameters(id, name, sectorId, inventoryNumber);
                JsonContent content = JsonContent.Create(itemUpdateParam);

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
                    _logger.LogError(result.ErrorMessage);
                    result.ErrorCode = (int)ErrorCodes.BadRequest;
                    result.ErrorMessage = "";
                    ErrorViewModel errorModel = new ErrorViewModel(result.ErrorCode, result.ErrorMessage);
                    return View("/Views/Error.cshtml", errorModel);
                }

                return View("/Views/Dictionaries/Items/Item.cshtml", result.Data);
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
