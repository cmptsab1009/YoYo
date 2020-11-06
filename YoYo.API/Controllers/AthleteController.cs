using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YoYo.Core.Interfaces;
using YoYo.Core.RequestModel;
using YoYo.Core.ResponseModel;

namespace YoYo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AthleteController : ControllerBase
    {
        private readonly ILogger<AthleteController> _logger;

        private readonly IAthlete _athlete;

        /// <summary>
        /// AthleteController constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="athlete"></param>
        public AthleteController(ILogger<AthleteController> logger, IAthlete athlete)
        {
            _logger = logger;
            _athlete = athlete;
        }

        /// <summary>
        /// Used to get all athlete's
        /// </summary>
        /// <returns>Return list of all athlete's</returns>
        [HttpGet]
        public List<AthletesResponse> Get()
        {
            // this will be called when we will have DB methods
            return _athlete.GetAllAthlete();
        }

        /// <summary>
        /// To add/update athlete test score
        /// </summary>
        /// <param name="athletesRequest">Request model of athlete's</param>
        /// <returns>Return true for insert/update success</returns>
        [HttpPut]
        public bool Put([FromBody] AthletesRequest athletesRequest)
        {
            return _athlete.UpdateAthlete(athletesRequest.UserId, (int)athletesRequest.Status, athletesRequest.StoppedTime);
        }
    }
}
