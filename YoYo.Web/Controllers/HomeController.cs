using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using YoYo.Core.Enum;
using YoYo.Core.RequestModel;
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

        public IActionResult Index()
        {
            var athleteList = new List<AthletesResponse>();
            string apiResponse = APIHelper.GetHttpContent(_appSettings.Value.apiBaseUrl + "Athlete", new CookieContainer(), HttpMethod.Get);
            athleteList = JsonConvert.DeserializeObject<List<AthletesResponse>>(apiResponse);
            return View(athleteList);
        }
       
        public IActionResult StartTest()
        {
            var fitnessRatingBeepList = new List<FitnessratingResponse>();
            string apiResponse = APIHelper.GetHttpContent(_appSettings.Value.apiBaseUrl + "Fitness", new CookieContainer(), HttpMethod.Get);
            fitnessRatingBeepList = JsonConvert.DeserializeObject<List<FitnessratingResponse>>(apiResponse);
            return new JsonResult(fitnessRatingBeepList);
        }

        public IActionResult WarnAthletes(int id)
        {
            var athlete = new AthletesRequest() { UserId = id, Status = AthleteStatus.Warned, StoppedTime = null };
            var content = JsonConvert.SerializeObject(athlete);
            bool result = JsonConvert.DeserializeObject<bool>(APIHelper.GetHttpContent(_appSettings.Value.apiBaseUrl + "Athlete", new CookieContainer(), HttpMethod.Put, content));
            return new JsonResult(result);
        }

        public IActionResult StopAthletes(int id, string stopTime)
        {
            var currentDateTime = DateTime.Parse(stopTime.Trim());
            var athlete = new AthletesRequest() { UserId = id, Status = AthleteStatus.Stoped, StoppedTime = currentDateTime };
            var content = JsonConvert.SerializeObject(athlete);
            bool result = JsonConvert.DeserializeObject<bool>(APIHelper.GetHttpContent(_appSettings.Value.apiBaseUrl + "Athlete", new CookieContainer(), HttpMethod.Put, content));
            return new JsonResult(result);
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
