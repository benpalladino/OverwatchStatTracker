// AUTHOR - BEN PALLADINO - ONSHORE OUTSOURCING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataObjects
{
    public class StatsDAO
    {
        //STATS
        public int StatsID { get; set; }
        public int HoursPlayed { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
    }
}
