using System.Collections.Generic;
using YoYo.Core.ResponseModel;

namespace YoYo.Core.Interfaces
{
    public interface IFitnessAction
    {
        List<FitnessratingResponse> GetAllFitnessratingData();
        
    }
}
