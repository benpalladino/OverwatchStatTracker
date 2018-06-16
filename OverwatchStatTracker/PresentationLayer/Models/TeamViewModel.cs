using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentationLayer.Models
{
    public class TeamViewModel
    { 
        public Team SingleTeam { get; set; }
        public List<Team> TeamList { get; set; }

        public TeamViewModel()
        {
            SingleTeam = new Team();
            TeamList = new List<Team>();
        }
    }
}