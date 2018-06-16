// AUTHOR - BEN PALLADINO - ONSHORE OUTSOURCING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using UtilityLogger;
using DataAccessLayer;
using DataAccessLayer.DataObjects;
using BusinessLogicLayer;

namespace DataAccessLayer
{
    public class UserDataAccess
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["OverwatchStatTracker"].ConnectionString;
        static ErrorLogger Logger = new ErrorLogger();

    //CREATE USERS
    //--ADMIN, MODERATOR, AND USER WILL BE ABLE TO CREATE A USER.
    //--VALUES REQUIRED FOR DATABASE ARE USERNAME, PASSWORD, EMAIL, AND BATTLENET ADDRESS.
        public bool AddUser(UsersDAO userToAdd)
        {
            //TRY TO EXECUTE THE CODE HERE, IF IT COMPLETES THEN THE BOOL SUCCESS = TRUE
            bool success = false;
            //THIS TRY ATTEMPTS TO OPEN A CONNECTION. IF IT SUCCEEDS, IT CLOSES THE CONNECTION. 
            try
            {
                //OPENS CONNECTION TO THE SQL DATABASE
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    //OPENS ACCESS TO SQL STORED PROCEDURE
                    using (SqlCommand command = new SqlCommand("sp_UsersTable_AddUser", connection))
                    {
                        //DECLARES COMMAND TYPES AND PARAMETERS FROM SQL DATABASE STORED PROCEDURES
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", userToAdd.Username);
                        command.Parameters.AddWithValue("@Password", userToAdd.Password);
                        command.Parameters.AddWithValue("@Email", userToAdd.Email);
                        command.Parameters.AddWithValue("@BattleNet", userToAdd.BattleNet);
                        command.Parameters.AddWithValue("@RoleID", userToAdd.RoleID);
                        command.Parameters.AddWithValue("@StatsID", userToAdd.StatsID);
                        command.Parameters.AddWithValue("@HeroID", userToAdd.HeroID);
                        command.Parameters.AddWithValue("@TeamID", userToAdd.TeamID);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            //IF THERE IS AN ERROR, THE CATCH WILL EXECUTE AND WRITE TO THE ERROR LOGGER
            catch (Exception error)
            {
                success = false;
                Logger.LogError(error);
            }
            return success;
        }

    //VIEW USERS
    //--ADMIN AND MODERATOR WILL BE ABLE TO VIEW ANY USERS INFORMATION.
    //--USER WILL BE ABLE TO VIEW THEIR INFORMATION.
    //--VALUES THAT CAN BE VIEWED FROM THE DATABASE ARE USERNAME, PASSWORD,
    // EMAIL, AND BATTLENET ADDRESS, ROLES, STATS, HEROES, AND TEAM.

        //THIS METHOD READS A USER FROM THE SQL USER TABLE
        public List<UsersDAO> GetAllUsers()
        {
            //CREATES A NEW LIST OF USERS FROM THE SQL DATABASE
            List<UsersDAO> userList = new List<UsersDAO>();
            
            //THIS TRY ATTEMPTS TO OPEN A CONNECTION. IF IT SUCCEEDS, IT CLOSES THE CONNECTION. 
            try
            {
                //OPENS CONNECTION TO THE SQL DATABASE
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    //OPENS ACCESS TO SQL STORED PROCEDURE
                    using (SqlCommand command = new SqlCommand("sp_UsersTable_GetAllUsers", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        //READS THE NEW LIST THAT WAS CREATED FROM ABOVE USING THE SQL DATABASE
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //GETS THE PARAMETERS AND THEIR DATATYPES
                                UsersDAO userToList = new UsersDAO();
                                userToList.UserID = reader.GetInt32(0);
                                userToList.Username = reader.GetString(1);
                                userToList.Password = reader.GetString(2);
                                userToList.Email = reader.GetString(3);
                                userToList.BattleNet = reader.GetString(4);
                                userToList.StatsID = reader.GetInt32(5);
                                userToList.HeroID = reader.GetInt32(6);
                                userToList.HeroName = reader.GetString(7);
                                userToList.RoleID = reader.GetInt32(8);
                                userToList.RoleTitle = reader.GetString(9);
                                userToList.TeamID = reader.GetInt32(10);
                                userToList.TeamName = reader.GetString(11);
                                userToList.StatsID = reader.GetInt32(12);
                                userToList.HoursPlayed = reader.GetInt32(13);
                                userToList.Wins = reader.GetInt32(14);
                                userToList.Losses = reader.GetInt32(15);
                                
                                userList.Add(userToList);
                            }
                        }
                    }
                }
            }
            //IF THERE IS AN ERROR, THE CATCH WILL EXECUTE AND WRITE TO THE ERROR LOGGER
            catch (Exception error)
            {
                Logger.LogError(error);
            }
            return userList;
        }

    //UPDATE USERS
    //--ADMIN AND MODERATOR WILL BE ABLE TO UPDATE ANY USERS INFORMATION.
    //--USER WILL BE ABLE TO UPDATE THEIR INFORMATION.
    //--VALUES THAT CAN BE UPDATED FOR DATABASE ARE USERNAME, PASSWORD, EMAIL, AND BATTLENET ADDRESS.

        //THIS METHOD UPDATES USERS FROM THE SQL USER TABLE
        public bool UpdateUsers(UsersDAO userToUpdate)
        {
            //CHECKS TO SEE IF THE METHOD WAS SUCCESSFUL OR NOT
            bool success = false;

            //THIS TRY ATTEMPTS TO OPEN A CONNECTION. IF IT SUCCEEDS, IT CLOSES THE CONNECTION. 
            try
            {
                //TRY TO EXECUTE THE CODE HERE, IF IT COMPLETES THEN THE BOOL SUCCESS = TRUE
                //OPENS CONNECTION TO THE SQL DATABASE
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    //OPENS ACCESS TO SQL STORED PROCEDURE
                    using (SqlCommand command = new SqlCommand("sp_UsersTable_UpdateUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", userToUpdate.UserID);
                        command.Parameters.AddWithValue("@Username", userToUpdate.Username);
                        command.Parameters.AddWithValue("@Email", userToUpdate.Email);
                        command.Parameters.AddWithValue("@BattleNet", userToUpdate.BattleNet);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception error)
            {
                success = false;
                Logger.LogError(error);
            }
            return success;
        }

    //DELETE USERS
    //--ADMIN AND MODERATOR WILL BE ABLE TO DELETE ANY USERS INFORMATION.
    //--USER WILL BE ABLE TO DELETE THEIR INFORMATION.
    //--VALUES THAT CAN BE DELETED FROM DATABASE ARE USERNAME, PASSWORD, EMAIL, AND BATTLENET ADDRESS
    //  WITH CASCADING DELETE TO ROLE ID, STATS ID, TEAM ID, AND HERO ID.

        //THIS METHOD READS A USER FROM THE SQL USER TABLE
        public bool DeleteUser(int UserID, int StatsID)
        {
            bool success = false;

            //THIS TRY ATTEMPTS TO OPEN A CONNECTION. IF IT SUCCEEDS, IT CLOSES THE CONNECTION. 
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_UsersTable_DeleteUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", UserID);
                        command.Parameters.AddWithValue("@StatsID", StatsID);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception error)
            {
                success = false;
                Logger.LogError(error);
            }
            return success;
        }

        //GET USER BY ID
        //THIS METHOD READS A USER BY THEIR USERID FROM THE SQL USER TABLE
        public UsersDAO GetUserByID(int UserID)
        {
            UsersDAO userToReturn = new UsersDAO();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_UsersTable_GetUserByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", UserID);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                userToReturn.Username = reader.GetString(0);
                                userToReturn.Password = reader.GetString(1);
                                userToReturn.Email = reader.GetString(2);
                                userToReturn.BattleNet = reader.GetString(3);
                                userToReturn.UserID = reader.GetInt32(4);
                                userToReturn.RoleID = reader.GetInt32(5);
                                userToReturn.RoleTitle = reader.GetString(6);
                                userToReturn.TeamID = reader.GetInt32(7);
                                userToReturn.TeamName = reader.GetString(8);
                                userToReturn.HeroID = reader.GetInt32(9);
                                userToReturn.HeroName = reader.GetString(10);
                                userToReturn.HeroType = reader.GetString(11);
                                userToReturn.StatsID = reader.GetInt32(12);
                                userToReturn.HoursPlayed = reader.GetInt32(13);
                                userToReturn.Wins = reader.GetInt32(14);
                                userToReturn.Losses = reader.GetInt32(15);
                            }
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Logger.LogError(error);
            }
            return userToReturn;
        }

        //GET USER BY USERNAME
        //THIS METHOD READS A USER BY THEIR USERNAME FROM THE SQL USER TABLE
        public UsersDAO GetUserByUsername(string Username)
        {
            UsersDAO userToReturn = new UsersDAO();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_UsersTable_GetUserByUsername", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", Username);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                userToReturn.UserID = reader.GetInt32(0);
                                userToReturn.Username = reader.GetString(1);
                                userToReturn.Password = reader.GetString(2);
                                userToReturn.RoleID = reader.GetInt32(3);
                            }
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Logger.LogError(error);
            }
            return userToReturn;
        }

        //UPDATE USER'S FAVORITE HERO
        //--ANY USER WILL BE ABLE TO UPDATE THEIR FAVORITE HERO.
        //--VALUES THAT CAN BE UPDATED FOR DATABASE ARE HERO NAME AND HERO TYPE

        //THIS METHOD UPDATES USERS FROM THE SQL USER TABLE
        public bool UpdateUsersFavoriteHero(int UserID, int HeroID)
        {
            //CHECKS TO SEE IF THE METHOD WAS SUCCESSFUL OR NOT
            bool success = false;

            //THIS TRY ATTEMPTS TO OPEN A CONNECTION. IF IT SUCCEEDS, IT CLOSES THE CONNECTION. 
            try
            {
                //TRY TO EXECUTE THE CODE HERE, IF IT COMPLETES THEN THE BOOL SUCCESS = TRUE
                //OPENS CONNECTION TO THE SQL DATABASE
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    //OPENS ACCESS TO SQL STORED PROCEDURE
                    using (SqlCommand command = new SqlCommand("sp_UsersTable_UpdateUsersFavoriteHero", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", UserID);
                        command.Parameters.AddWithValue("@HeroID", HeroID);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception error)
            {
                success = false;
                Logger.LogError(error);
            }
            return success;
        }
    }
}