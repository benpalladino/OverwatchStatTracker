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
    public class HeroDataAccess
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["OverwatchStatTracker"].ConnectionString;
        static ErrorLogger Logger = new ErrorLogger();

        //------------------------------------//
        //ADD HEROES
        //------------------------------------//
        public bool AddHero(HeroesDAO heroToAdd)
        {
            bool success = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_HeroesTable_AddHero", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@HeroName", heroToAdd.HeroName);
                        command.Parameters.AddWithValue("@HeroType", heroToAdd.HeroType);
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

        //------------------------------------//
        //VIEW HEROES
        //------------------------------------//
        public List<HeroesDAO> GetAllHeroes()
        {
            List<HeroesDAO> heroList = new List<HeroesDAO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_HeroesTable_GetAllHeroes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                HeroesDAO heroToList = new HeroesDAO();
                                heroToList.HeroID = reader.GetInt32(0);
                                heroToList.HeroName = reader.GetString(1);
                                heroToList.HeroType = reader.GetString(2);
                                heroList.Add(heroToList);
                            }
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Logger.LogError(error);
            }
            return heroList;
        }

        //------------------------------------//
        //UPDATE HEROES
        //------------------------------------//
        //THIS METHOD UPDATES HEROES FROM THE SQL USER TABLE
        public bool UpdateHero(HeroesDAO userToUpdate)
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
                    using (SqlCommand command = new SqlCommand("sp_HeroesTable_UpdateHero", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@HeroID", userToUpdate.HeroID);
                        command.Parameters.AddWithValue("@HeroName", userToUpdate.HeroName);
                        command.Parameters.AddWithValue("@HeroType", userToUpdate.HeroType);
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

        //------------------------------------//
        //DELETE HEROES
        //------------------------------------//
        public bool DeleteHero(int HeroID)
        {
            bool success = false;

            //THIS TRY ATTEMPTS TO OPEN A CONNECTION. IF IT SUCCEEDS, IT CLOSES THE CONNECTION. 
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_HeroesTable_DeleteHero", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
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

        //------------------------------------//
        //GET HEROES BY ID
        //------------------------------------//
        public HeroesDAO GetHeroByID(int HeroID)
        {
            HeroesDAO heroToReturn = new HeroesDAO();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_HeroesTable_GetHeroByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@HeroID", HeroID);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                heroToReturn.HeroID = reader.GetInt32(0);
                                heroToReturn.HeroName = reader.GetString(1);
                                heroToReturn.HeroType = reader.GetString(2);
                            }
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Logger.LogError(error);
            }
            return heroToReturn;
        }
    }
}
