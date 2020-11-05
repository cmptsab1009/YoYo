using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using YoYo.Core.ResponseModel;
using YoYo.Web.Helper;
using YoYo.Web.Models;

namespace YoYo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptions<AppSettings> _appSettings;


        public HomeController(ILogger<HomeController> logger, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _appSettings = appSettings;
        }

        

        public async Task<IActionResult> Index()
        {
            var athleteList = new List<AthletesResponse>();
            string apiResponse =  APIHelper.GetHttpContent(_appSettings.Value.apiBaseUrl + "Athlete", new CookieContainer());
            athleteList = JsonConvert.DeserializeObject<List<AthletesResponse>>(apiResponse);
            return View(athleteList);
        }
        public IActionResult StartTest()
        {
           // var jsonString = System.IO.File.ReadAllText("Files/fitnessrating_beeptest.json");
            var fitnessRatingBeepList = new List<FitnessratingResponse>();// JsonSerializer.Deserialize<List<FitnessratingResponse>>(jsonString);
            return new JsonResult(fitnessRatingBeepList);
        }

        public IActionResult WarnAthletes(int athleteId)
        {
            return new JsonResult("ok");
        }

        public IActionResult StopAthletes(int athleteId)
        {
            return new JsonResult("ok");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
