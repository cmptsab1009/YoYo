using System;
using System.Collections.Generic;
using System.Text;
using YoYo.Core.Enum;

namespace YoYo.Core.RequestModel
{
   public class AthletesRequest
    {
        public int UserId { get; set; }
        public AthleteStatus Status { get; set; }
        public DateTime? StoppedTime { get; set; }
    }
}
