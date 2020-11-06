using System;
using System.Collections.Generic;
using YoYo.Core.ResponseModel;

namespace YoYo.Core.Interfaces
{
    public interface IAthlete
    {
        List<AthletesResponse> GetAllAthlete();
        bool UpdateAthlete(int id, int status, DateTime? stopTime);
    }
}
