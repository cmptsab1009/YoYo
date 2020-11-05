using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YoYo.Core.ResponseModel;

namespace YoYo.Core.Interfaces
{
    public interface IAthlete
    {
        List<AthletesResponse> GetAllAthlete();
    }
}
