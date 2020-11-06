using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YoYo.Core.Interfaces;
using YoYo.Core.ResponseModel;

namespace YoYo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FitnessController : ControllerBase
    {
        private readonly ILogger<FitnessController> _logger;
        private readonly IFitnessAction _fitnessAction;
        public FitnessController(ILogger<FitnessController> logger, IFitnessAction fitnessAction)
        {
            _logger = logger;
            _fitnessAction = fitnessAction;
        }

        [HttpGet]
        public List<FitnessratingResponse> Get()
        {
            // this will be called when we will have DB methods
            return _fitnessAction.GetAllFitnessratingData();
        }
    }
}
