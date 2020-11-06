using System;
using System.Collections.Generic;
using System.Text;
using YoYo.Core.ResponseModel;

namespace YoYo.Core.Interfaces
{
    public interface IFitnessAction
    {
        List<FitnessratingResponse> GetAllFitnessratingData();
        
    }
}
