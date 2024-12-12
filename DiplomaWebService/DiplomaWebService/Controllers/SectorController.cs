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

			return View("/Views/Dictionaries/Sectors/Sector.cshtml", result.Data);
		}

		[HttpPost]
		[Route("/sectors")]
		public async Task<IActionResult> CreateSector(string name, string shortSecotrName)
		{
			SectorCreateParameters sectorCreateParam = new SectorCreateParameters(name, shortSecotrName);
			Result<Sector> result = new Result<Sector>();
			string url = _connectionString + "sectors";
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
					result.ErrorCode = (int)ErrorCodes.BadRequest;
					result.ErrorMessage = "";
					_logger.LogError(result.ErrorMessage);
				}

				return View("/Views/Dictionaries/Sectors/Sector.cshtml", result.Data);
			}
		}

		[HttpPut]
		[Route("/sectors")]
		public async Task<IActionResult> UpdateSector(int id, string name, string shortSectorName)
		{
			Result<Sector> result = new Result<Sector>();
			string url = _connectionString + $"/sectors/{id}";
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
					result.ErrorCode = (int)ErrorCodes.BadRequest;
					result.ErrorMessage = "";
					_logger.LogError(result.ErrorMessage);
				}

				return View("/Views/Dictionaries/Sectors/Sector.cshtml", result.Data);
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
