using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using YoYo.Core.ResponseModel;

namespace YoYo.Web.Controllers
{
    public class AthleteController : Controller
    {
        private readonly ILogger<AthleteController> _logger;

        public AthleteController(ILogger<AthleteController> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var athleteList = new List<AthletesResponse>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:44337/api/Athlete"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    athleteList = JsonConvert.DeserializeObject<List<AthletesResponse>>(apiResponse);
                }
            }
            return View(athleteList);
        }
    }
}
