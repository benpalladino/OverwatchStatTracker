// AUTHOR - BEN PALLADINO - ONSHORE OUTSOURCING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer.DataObjects;
using BusinessLogicLayer.LogicObjects;

namespace PresentationLayer.Models
{
    public class Mapper
    {
        public UsersDAO Map(User userToMap)
        {
            UsersDAO userToReturn = new UsersDAO();

            userToReturn.UserID = userToMap.UserID;
            userToReturn.Username = userToMap.Username;
            userToReturn.Password = userToMap.Password;
            userToReturn.Email = userToMap.Email;
            userToReturn.BattleNet = userToMap.BattleNet;
            userToReturn.RoleID = userToMap.RoleID;
            userToReturn.RoleTitle = userToMap.RoleTitle;
            userToReturn.TeamID = userToMap.TeamID;
            userToReturn.TeamName = userToMap.TeamName;
            userToReturn.HeroID = userToMap.HeroID;
            userToReturn.HeroName = userToMap.HeroName;
            userToReturn.HeroType = userToMap.HeroType;
            userToReturn.StatsID = userToMap.StatsID;
            userToReturn.HoursPlayed = userToMap.HoursPlayed;
            userToReturn.Wins = userToMap.Wins;
            userToReturn.Losses = userToMap.Losses;

            return userToReturn;
        }

        public User Map(UsersDAO userToMap)
        {
            User userToReturn = new User();

            userToReturn.UserID = userToMap.UserID;
            userToReturn.Username = userToMap.Username;
            userToReturn.Password = userToMap.Password;
            userToReturn.Email = userToMap.Email;
            userToReturn.BattleNet = userToMap.BattleNet;
            userToReturn.RoleID = userToMap.RoleID;
            userToReturn.RoleTitle = userToMap.RoleTitle;
            userToReturn.TeamID = userToMap.TeamID;
            userToReturn.TeamName = userToMap.TeamName;
            userToReturn.HeroID = userToMap.HeroID;
            userToReturn.HeroName = userToMap.HeroName;
            userToReturn.HeroType = userToMap.HeroType;
            userToReturn.StatsID = userToMap.StatsID;
            userToReturn.HoursPlayed = userToMap.HoursPlayed;
            userToReturn.Wins = userToMap.Wins;
            userToReturn.Losses = userToMap.Losses;

            return userToReturn;
        }

        public List<User> Map(List<UsersDAO> userListToMap)
        {
            List<User> userListToReturn = new List<User>();

            foreach (UsersDAO userToMap in userListToMap)
            {
                User userToReturn = new User();
                userToReturn.UserID = userToMap.UserID;
                userToReturn.Username = userToMap.Username.ToUpper();
                userToReturn.Password = userToMap.Password;
                userToReturn.Email = userToMap.Email;
                userToReturn.BattleNet = userToMap.BattleNet;
                userToReturn.RoleID = userToMap.RoleID;
                userToReturn.RoleTitle = userToMap.RoleTitle;
                userToReturn.TeamID = userToMap.TeamID;
                userToReturn.TeamName = userToMap.TeamName;
                userToReturn.HeroID = userToMap.HeroID;
                userToReturn.HeroName = userToMap.HeroName;
                userToReturn.HeroType = userToMap.HeroType;
                userToReturn.StatsID = userToMap.StatsID;
                userToReturn.HoursPlayed = userToMap.HoursPlayed;
                userToReturn.Wins = userToMap.Wins;
                userToReturn.Losses = userToMap.Losses;

                userListToReturn.Add(userToReturn);
            }
            return userListToReturn;
        }

        //TEAM MAPPER OBJECTS

        public TeamsDAO teamMap(Team teamToMap)
        {
            TeamsDAO teamToReturn = new TeamsDAO();

            teamToReturn.TeamID = teamToMap.TeamID;
            teamToReturn.TeamName = teamToMap.TeamName.ToUpper();
            teamToReturn.TeamRanking = teamToMap.TeamRanking;

            return teamToReturn;
        }

        public Team teamMap(TeamsDAO teamToMap)
        {
            Team teamToReturn = new Team();

            teamToReturn.TeamID = teamToMap.TeamID;
            teamToReturn.TeamName = teamToMap.TeamName.ToUpper();
            teamToReturn.TeamRanking = teamToMap.TeamRanking;

            return teamToReturn;
        }

        public List<Team> teamMap(List<TeamsDAO> teamListToMap)
        {
            List<Team> teamListToReturn = new List<Team>();

            foreach (TeamsDAO teamToMap in teamListToMap)
            {
                Team mapToReturn = new Team();
                mapToReturn.TeamID = teamToMap.TeamID;
                mapToReturn.TeamName = teamToMap.TeamName.ToUpper();
                mapToReturn.TeamRanking = teamToMap.TeamRanking;

                teamListToReturn.Add(mapToReturn);
            }
            return teamListToReturn;
        }

        //HERO MAPPER OBJECTS

        public HeroesDAO HeroMap(Hero heroToMap)
        {
            HeroesDAO heroToReturn = new HeroesDAO();

            heroToReturn.HeroID = heroToMap.HeroID;
            heroToReturn.HeroName = heroToMap.HeroName.ToUpper();
            heroToReturn.HeroType = heroToMap.HeroType;

            return heroToReturn;
        }

        public Hero HeroMap(HeroesDAO heroToMap)
        {
            Hero heroToReturn = new Hero();

            heroToReturn.HeroID = heroToMap.HeroID;
            heroToReturn.HeroName = heroToMap.HeroName.ToUpper();
            heroToReturn.HeroType = heroToMap.HeroType;

            return heroToReturn;
        }

        public List<Hero> HeroMap(List<HeroesDAO> heroListToMap)
        {
            List<Hero> heroListToReturn = new List<Hero>();

            foreach (HeroesDAO heroToMap in heroListToMap)
            {
                //Method inside of a Method? Looks a lot better.
                Hero mapToReturn = HeroMap(heroToMap);

                heroListToReturn.Add(mapToReturn);
            }
            return heroListToReturn;
        }

        public List<HeroesDAO> HeroMap(List<Hero> heroListToMap)
        {
            List<HeroesDAO> heroListToReturn = new List<HeroesDAO>();

            foreach (Hero heroToMap in heroListToMap)
            {
                
                HeroesDAO mapToReturn = HeroMap(heroToMap);

                heroListToReturn.Add(mapToReturn);
            }
            return heroListToReturn;
        }

        //BUSINESS LOGIC MAPPER
        public UsersBLO LogicMap(User userToMap)
        {
            UsersBLO userToReturn = new UsersBLO();

            userToReturn.UserID = userToMap.UserID;
            userToReturn.Username = userToMap.Username.ToUpper();
            userToReturn.Password = userToMap.Password;
            userToReturn.Email = userToMap.Email.ToUpper();
            userToReturn.BattleNet = userToMap.BattleNet.ToUpper();
            userToReturn.RoleID = userToMap.RoleID;
            userToReturn.RoleTitle = userToMap.RoleTitle;
            userToReturn.TeamID = userToMap.TeamID;
            userToReturn.TeamName = userToMap.TeamName;
            userToReturn.HeroID = userToMap.HeroID;
            userToReturn.HeroName = userToMap.HeroName;
            userToReturn.HeroType = userToMap.HeroType;
            userToReturn.StatsID = userToMap.StatsID;
            userToReturn.HoursPlayed = userToMap.HoursPlayed;
            userToReturn.Wins = userToMap.Wins;
            userToReturn.Losses = userToMap.Losses;
            userToReturn.Ratio = userToMap.Ratio;

            return userToReturn;
        }

        public User LogicMap(UsersBLO userToMap)
        {
            User userToReturn = new User();

            userToReturn.UserID = userToMap.UserID;
            userToReturn.Username = userToMap.Username.ToUpper();
            userToReturn.Password = userToMap.Password;
            userToReturn.Email = userToMap.Email.ToUpper();
            userToReturn.BattleNet = userToMap.BattleNet.ToUpper();
            userToReturn.RoleID = userToMap.RoleID;
            userToReturn.RoleTitle = userToMap.RoleTitle;
            userToReturn.TeamID = userToMap.TeamID;
            userToReturn.TeamName = userToMap.TeamName;
            userToReturn.HeroID = userToMap.HeroID;
            userToReturn.HeroName = userToMap.HeroName;
            userToReturn.HeroType = userToMap.HeroType;
            userToReturn.StatsID = userToMap.StatsID;
            userToReturn.HoursPlayed = userToMap.HoursPlayed;
            userToReturn.Wins = userToMap.Wins;
            userToReturn.Losses = userToMap.Losses;
            userToReturn.Ratio = userToMap.Ratio;

            return userToReturn;
        }

        public List<User> LogicMap(List<UsersBLO> userListToMap)
        {
            List<User> userListToReturn = new List<User>();

            foreach (UsersBLO userToMap in userListToMap)
            {
                User userToReturn = LogicMap(userToMap);

                userListToReturn.Add(userToReturn);
            }
            return userListToReturn;
        }

        public List<UsersBLO> LogicMap(List<User> userListToMap)
        {
            List<UsersBLO> userListToReturn = new List<UsersBLO>();

            foreach (User userToMap in userListToMap)
            {
                UsersBLO userToReturn = LogicMap(userToMap);

                userListToReturn.Add(userToReturn);
            }
            return userListToReturn;
        }

    }
}