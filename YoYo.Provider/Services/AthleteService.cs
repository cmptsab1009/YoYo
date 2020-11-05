using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoYo.Core.Constants;
using YoYo.Core.Exceptions;
using YoYo.Core.Interfaces;
using YoYo.Core.ResponseModel;
using YoYo.Data;
using YoYo.Data.Context;
using YoYo.Provider.Mapper;

namespace YoYo.Provider.Services
{
    public class AthleteService : IAthlete
    {
        private readonly ILogger _logger;
        private readonly AppDbContext _appDbContext;
        public AthleteService(ILogger<AthleteService> logger)
        {
            _logger = logger;
            _appDbContext = new AppDbContext();
        }
        public List<AthletesResponse> GetAllAthlete()
        {
            var list = (AppDbContext.athletes?.ToList());
            if (list == null || list?.Count == 0)
                throw new NotFoundException(Constant.RecordsNotFound);

            return list.Select(x => AutoMap.Mapping<athlete, AthletesResponse>(x)).ToList();
        }
    }
}
