using System;
using System.Collections.Generic;
using System.Text;
using YoYo.Core.Enum;

namespace YoYo.Core.ResponseModel
{
    public class AthletesResponse
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public AthleteStatus Status { get; set; }
    }
}
