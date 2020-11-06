
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace YoYo.Data.Context
{
    public class AppDbContext
    {
        public AppDbContext()
        {
            athletes = new List<athlete>(){
             new athlete(){ Name = "Athlete1", UserId = 1, Status = 0 },
             new athlete(){ Name = "Athlete2", UserId = 2, Status = 0 },
             new athlete(){ Name = "Athlete3", UserId = 3, Status = 0 },
             new athlete(){ Name = "Athlete4", UserId = 4, Status = 0 },
             new athlete(){ Name = "Athlete5", UserId = 5, Status = 0 }
            };
            var jsonString = System.IO.File.ReadAllText("Files/fitnessrating_beeptest.json");
            fitnessratings = JsonSerializer.Deserialize<List<fitnessrating>>(jsonString); 
        }
        public static List<athlete> athletes { get; set; }
        public static List<fitnessrating> fitnessratings { get; set; }
    }
}
