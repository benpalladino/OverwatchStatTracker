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

        //ADD USER
        //THIS METHOD ADDS A USER TO THE SQL USER TABLE
        public bool AddUser(UsersDAO userToAdd)
        {
            //TRY TO EXECUTE THE CODE HERE, IF IT COMPLETES THEN THE BOOL SUCCESS = TRUE
            bool success = true;

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

            finally
            {

            }
            return success;
        }

        //GET USER
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
                                userToList.HeroID = reader.GetInt32(5);
                                userToList.RoleID = reader.GetInt32(6);
                                userToList.StatsID = reader.GetInt32(7);
                                userToList.TeamID = reader.GetInt32(8);
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

            finally
            {

            }
            return userList;
        }

        //UPDATE USER
        //THIS METHOD READS A USER FROM THE SQL USER TABLE
        public bool UpdateUser(UsersDAO userToUpdate)
        {
            //CHECKS TO SEE IF THE METHOD WAS SUCCESSFUL OR NOT
            bool success = true;

            //THIS TRY ATTEMPTS TO OPEN A CONNECTION. IF IT SUCCEEDS, IT CLOSES THE CONNECTION. 
            try
            {
                //TRY TO EXECUTE THE CODE HERE, IF IT COMPLETES THEN THE BOOL SUCCESS = TRUE
                //OPENS CONNECTION TO THE SQL DATABASE
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    //OPENS ACCESS TO SQL STORED PROCEDURE
                    using (SqlCommand command = new SqlCommand("sp_UsersTable_UpdateUser"))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", userToUpdate.Username);
                        command.Parameters.AddWithValue("@Password", userToUpdate.Password);
                        command.Parameters.AddWithValue("@Email", userToUpdate.Email);
                        command.Parameters.AddWithValue("@BattleNet", userToUpdate.BattleNet);
                        command.Parameters.AddWithValue("@HeroID", userToUpdate.HeroID);
                        command.Parameters.AddWithValue("@RoleID", userToUpdate.RoleID);
                        command.Parameters.AddWithValue("@StatsID", userToUpdate.StatsID);
                        command.Parameters.AddWithValue("@TeamID", userToUpdate.TeamID);
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
            finally
            {

            }
            return success;
        }

        //DELETE USER
        //THIS METHOD READS A USER FROM THE SQL USER TABLE
        public bool DeleteUser(int UserID)
        {
            bool success = true;

            //THIS TRY ATTEMPTS TO OPEN A CONNECTION. IF IT SUCCEEDS, IT CLOSES THE CONNECTION. 
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_UsersTable_DeleteUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", UserID);
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
            finally
            {

            }
            return success = true;
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
                                userToReturn.UserID = reader.GetInt32(0);
                                userToReturn.Username = reader.GetString(1);
                                userToReturn.Password = reader.GetString(2);
                                userToReturn.Email = reader.GetString(3);
                                userToReturn.BattleNet = reader.GetString(4);
                                userToReturn.HeroID = reader.GetInt32(5);
                                userToReturn.RoleID = reader.GetInt32(6);
                                userToReturn.StatsID = reader.GetInt32(7);
                                userToReturn.TeamID = reader.GetInt32(8);
                            }
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Logger.LogError(error);
            }
            finally
            {

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
            finally
            {

            }
            return userToReturn;
        }
    }
}
