using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
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
       // private IConfiguration _configuration { get; }

        private readonly IAthlete _athlete;
        public AthleteController(ILogger<AthleteController> logger,  IAthlete athlete)//IConfiguration configuration,
        {
            _logger = logger;
           // _configuration = configuration;
            _athlete = athlete;
        }

        [HttpGet]
        public List<AthletesResponse> Get()
        {
            // this will be called when we will have DB methods
            return _athlete.GetAllAthlete();
        }

        [HttpPut]
        public bool Put([FromBody] AthletesRequest athletesRequest)
        {
            return _athlete.UpdateAthlete(athletesRequest.UserId, (int)athletesRequest.Status, athletesRequest.StoppedTime);
        }
    }
}
