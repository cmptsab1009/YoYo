using System;
using System.Collections.Generic;
using System.Text;

namespace YoYo.Data
{
   public class fitnessrating
    {
        public int AccumulatedShuttleDistance { get; set; }
        public int SpeedLevel { get; set; }
        public int ShuttleNo { get; set; }
        public int Speed { get; set; }
        public decimal LevelTime { get; set; }
        public TimeSpan CommulativeTime { get; set; }
        public TimeSpan StartTime { get; set; }
        public decimal ApproxVo2Max { get; set; }

    }
}
