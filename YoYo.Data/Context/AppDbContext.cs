using System;
using System.Collections.Generic;
using System.Text;

namespace YoYo.Data.Context
{
    public class AppDbContext
    {
        public AppDbContext()
        {
            athletes = new List<athlete>();
            athletes.AddRange(new List<athlete>() {
             new athlete(){ Name = "Athlete1", UserId = Guid.NewGuid(), Status = 0 },
             new athlete(){ Name = "Athlete2", UserId = Guid.NewGuid(), Status = 0 },
             new athlete(){ Name = "Athlete3", UserId = Guid.NewGuid(), Status = 0 },
             new athlete(){ Name = "Athlete4", UserId = Guid.NewGuid(), Status = 0 },
             new athlete(){ Name = "Athlete5", UserId = Guid.NewGuid(), Status = 0 }
            });
        }
        public static List<athlete> athletes { get; set; }
        public static List<fitnessrating> fitnessratings { get; set; }
    }
}
