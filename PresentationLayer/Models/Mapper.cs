using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer.DataObjects;

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
            userToReturn.HeroID = userToMap.HeroID;
            userToReturn.RoleID = userToMap.RoleID;
            userToReturn.StatsID = userToMap.StatsID;
            userToReturn.TeamID = userToMap.TeamID;

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
            userToReturn.HeroID = userToMap.HeroID;
            userToReturn.RoleID = userToMap.RoleID;
            userToReturn.StatsID = userToMap.StatsID;
            userToReturn.TeamID = userToMap.TeamID;

            return userToReturn;
        }

        public List<User> Map(List<UsersDAO> userListToMap)
        {
            List<User> userListToReturn = new List<User>();
            
            foreach (UsersDAO userToMap in userListToMap)
            {
                User userToReturn = new User();
                userToReturn.UserID = userToMap.UserID;
                userToReturn.Username = userToMap.Username;
                userToReturn.Password = userToMap.Password;
                userToReturn.Email = userToMap.Email;
                userToReturn.BattleNet = userToMap.BattleNet;
                userToReturn.HeroID = userToMap.HeroID;
                userToReturn.RoleID = userToMap.RoleID;
                userToReturn.StatsID = userToMap.StatsID;
                userToReturn.TeamID = userToMap.TeamID;
            }
            return userListToReturn;
        }
    }
}