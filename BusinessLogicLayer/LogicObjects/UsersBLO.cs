// AUTHOR - BEN PALLADINO - ONSHORE OUTSOURCING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.LogicObjects
{
    public class UsersBLO
    {
        //USERS
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string BattleNet { get; set; }
        public int Ranking { get; set; }

        //ROLES
        public int RoleID { get; set; }
        public string RoleTitle { get; set; }

        //TEAMS
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public int TeamRanking { get; set; }

        //HEROES
        public int HeroID { get; set; }
        public string HeroName { get; set; }
        public string HeroType { get; set; }

        //STATS
        public int StatsID { get; set; }
        public int HoursPlayed { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public decimal Ratio { get; set; }
    }
}
