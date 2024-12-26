using DiplomaWebService.Common.Enum;
using DiplomaWebService.Common.Results;
using DiplomaWebService.Constants;
using DiplomaWebService.Models;
using DiplomaWebService.Parametrs.Sector;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaWebService.Controllers
{
	public class SectorController : Controller
	{
		private ILogger<SectorController> _logger;
		private string? _connectionString;

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
				_logger.LogError(result.ErrorMessage);
				result.ErrorCode = (int)ErrorCodes.BadRequest;
				result.ErrorMessage = "ican't get all sectors";
				string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
				ErrorViewModel errorModel = new ErrorViewModel(errorName, result.ErrorMessage);
				return View("/Views/Shared/Error.cshtml", errorModel);
			}
			ViewData["/sectors"] = result.Data;
			return View("/Views/Dictionaries/Sectors/Sector.cshtml", result.Data);
		}

		[HttpPost]
		[Route("/sector")]
		public async Task<IActionResult> CreateSector(string name, string shortSectorName)
		{
			SectorCreateParameters sectorCreateParam = new SectorCreateParameters(name, shortSectorName);
			Result<Sector> result = new Result<Sector>();
			string url = _connectionString + "sector";
			using (HttpClient client = new HttpClient())
			{
				JsonContent content = JsonContent.Create(sectorCreateParam);

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
					_logger.LogError(result.ErrorMessage);
					result.ErrorCode = (int)ErrorCodes.BadRequest;
					result.ErrorMessage = "";
					string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
					ErrorViewModel errorModel = new ErrorViewModel(errorName, result.ErrorMessage);
					return View("/Views/Shared/Error.cshtml", errorModel);
				}

				return RedirectToAction("GetAllSectors");
			}
		}

		[HttpPut]
		[Route("/sector/{id}")]
		public async Task<IActionResult> UpdateSector(int id, string name, string shortSectorName)
		{
			Result<Sector> result = new Result<Sector>();
			string url = _connectionString + $"/sector/{id}";
			using (HttpClient client = new HttpClient())
			{
				SectorUpdateParameters sectorUpdateParam = new SectorUpdateParameters(id, name, shortSectorName);
				JsonContent content = JsonContent.Create(sectorUpdateParam);

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
					_logger.LogError(result.ErrorMessage);
					result.ErrorCode = (int)ErrorCodes.BadRequest;
					result.ErrorMessage = "";
					string errorName = Enum.GetName(typeof(ErrorCodes), result.ErrorCode);
					ErrorViewModel errorModel = new ErrorViewModel(errorName, result.ErrorMessage);
					return View("/Views/Shared/Error.cshtml", errorModel);
				}

				return RedirectToAction("GetAllSectors");
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
