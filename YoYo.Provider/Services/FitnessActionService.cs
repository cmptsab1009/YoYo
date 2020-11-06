using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoYo.Core.Constants;
using YoYo.Core.Exceptions;
using YoYo.Core.Interfaces;
using YoYo.Core.ResponseModel;
using YoYo.Data;
using YoYo.Data.Context;
using YoYo.Provider.Mapper;

namespace YoYo.Provider.Services
{
    public class FitnessActionService : IFitnessAction
    {
        private readonly ILogger _logger;
        private readonly AppDbContext _appDbContext;
        public FitnessActionService(ILogger<FitnessActionService> logger)
        {
            _logger = logger;
            _appDbContext = new AppDbContext();
        }
        public List<FitnessratingResponse> GetAllFitnessratingData()
        {
            var list = AppDbContext.fitnessratings?.ToList();
            if (list == null || list?.Count == 0)
                throw new NotFoundException(Constant.RecordsNotFound);

            // set all athletes status as running i.e. 1
            AppDbContext.athletes.ForEach(x => x.Status = 1);



            return list.Select(x => AutoMap.Mapping<fitnessrating, FitnessratingResponse>(x)).ToList();
        }

    }
}
