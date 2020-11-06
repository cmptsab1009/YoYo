using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using YoYo.Core.Constants;
using YoYo.Core.Enum;
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
            var list = new List<athlete>();
            try
            {
                list = (AppDbContext.athletes?.ToList());
                if (list == null || list?.Count == 0)
                    throw new NotFoundException(Constant.RecordsNotFound);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


            return list.Select(x => AutoMap.Mapping<athlete, AthletesResponse>(x)).ToList();
        }

        public bool UpdateAthlete(int id, int status, DateTime? stopTime)
        {
            try
            {
                if (AppDbContext.athletes.Any(x => x.UserId == id))
                {
                    AppDbContext.athletes.Where(x => x.UserId == id).ToList().ForEach(y => y.Status = status);

                    if(status == (int)AthleteStatus.Stoped && stopTime != null) // athelet is stopped, so needs to update the stopped time
                        AppDbContext.athletes.Where(x => x.UserId == id).ToList().ForEach(y => y.StoppedTime = stopTime.Value.TimeOfDay );

                    return true;
                }
                else
                    throw new NotFoundException(Constant.RecordsNotFound);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
