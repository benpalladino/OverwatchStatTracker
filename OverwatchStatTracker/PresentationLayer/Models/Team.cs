using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class Team
    {
        //TEAMS
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public int TeamRanking { get; set; }
    }
}