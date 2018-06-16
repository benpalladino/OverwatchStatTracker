using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class Stats
    {
        public int StatsID { get; set; }
        public int HoursPlayed { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
    }
}