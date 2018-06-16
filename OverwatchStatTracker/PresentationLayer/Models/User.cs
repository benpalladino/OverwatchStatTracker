using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class User
    {
        //USERS

        [Required(ErrorMessage ="This field is required.")]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string BattleNet { get; set; }
        public int Ranking { get; set; }

        //HEROES
        public int HeroID { get; set; }
        public string HeroName { get; set; }
        public string HeroType { get; set; }

        //ROLES
        public int RoleID { get; set; }
        public string RoleTitle { get; set; }

        //STATS
        public int StatsID { get; set; }
        public int HoursPlayed { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public decimal Ratio { get; set; }

        //TEAMS
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public int TeamRanking { get; set; }
    }
}