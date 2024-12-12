using DiplomaWebService.Common.Enum;
using DiplomaWebService.Common.Results;
using DiplomaWebService.Constants;
using DiplomaWebService.Models;
using DiplomaWebService.Parametrs.Units;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaWebService.Controllers
{
	public class UnitController : Controller
	{
		private readonly ILogger<UnitController> _logger;
		private readonly string? _connectionString;

		public UnitController(ILogger<UnitController> logger, IConfiguration configuration)
		{
			_logger = logger;
			_connectionString = configuration.GetConnectionString(Constant.MainConnectionString);
		}

		[HttpGet]
		[Route("/units")]
		public async Task<IActionResult> GetAllUnits()
		{
			Result<List<Unit>> result = new Result<List<Unit>>();
			string url = _connectionString + "units";
			using (HttpClient client = new HttpClient())
			{
				HttpResponseMessage responseMessage = await client.GetAsync(url);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<List<Unit>>();
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

			return View("/Views/Dictionaries/Units/Unit.cshtml", result.Data);
		}

		[HttpPost]
		[Route("/unit")]
		public async Task<IActionResult> CreateUnit(string name)
		{
			Result<Unit> result = new Result<Unit>();
			string url = _connectionString + "units";
			using (HttpClient client = new HttpClient())
			{
				UnitCreateParameters unitCreateParam = new UnitCreateParameters(name);
				JsonContent content = JsonContent.Create(unitCreateParam);

				HttpResponseMessage responseMessage = await client.PostAsync(url, content);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<Unit>();
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

				return View("/Views/Dictionaries/Units/Unit.cshtml", result.Data);
			}
		}

		[HttpPut]
		[Route("/units")]
		public async Task<IActionResult> UpdateUnit(int id, string name)
		{
			Result<Unit> result = new Result<Unit>();
			string url = _connectionString + $"units/{id}";
			using (HttpClient client = new HttpClient())
			{
				UnitUpdateParameters unitUpdateParam = new UnitUpdateParameters(id, name);
				JsonContent content = JsonContent.Create(unitUpdateParam);

				HttpResponseMessage responseMessage = await client.PutAsync(url, content);
				if (responseMessage.IsSuccessStatusCode)
				{
					result.Data = await responseMessage.Content.ReadFromJsonAsync<Unit>();
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

				return View("/Views/Dictionaries/Units/Unit.cshtml", result.Data);
			}
		}

		[HttpGet]
		[Route("/unit")]
		public IActionResult GetCreateSector()
		{
			return View("/Views/Forms/UnitForm/AddUnit.cshtml");
		}
	}
}
