// AUTHOR - BEN PALLADINO - ONSHORE OUTSOURCING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.LogicObjects;

namespace BusinessLogicLayer
{
    public class LeaderboardLogic
    {
        //CREATE LIST OF USERS

        //SORT USERS IN LIST BY MOST WINS
        public List<UsersBLO> GetUserWL (List<UsersBLO> userWLRatio)
        {
            foreach (UsersBLO User in userWLRatio)
            {
                GetUserWL(User);
            }

            return (userWLRatio);
        }

        public UsersBLO GetUserWL(UsersBLO User)
        {
                if (User.Wins != 0 && User.Losses != 0)
                {
                    decimal wins = User.Wins;
                    decimal losses = User.Losses;
                    decimal ratio = wins / losses;
                    User.Ratio = ratio;
                }
                else
                {
                    User.Ratio = 0;
                }
            return (User);
        }
    }
}
