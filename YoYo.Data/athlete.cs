using System;
using System.Collections.Generic;
using System.Text;

namespace YoYo.Data
{
    public class athlete
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public TimeSpan StoppedTime { get; set; }
    }
}
