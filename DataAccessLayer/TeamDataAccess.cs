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
    public class TeamDataAccess
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["OverwatchStatTracker"].ConnectionString;
        static ErrorLogger Logger = new ErrorLogger();

        //------------------------------------//
        //ADD TEAMS
        //------------------------------------//

        public bool AddTeam(TeamsDAO teamToAdd)
        {
            bool success = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using(SqlCommand command = new SqlCommand("sp_TeamsTable_AddTeam", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TeamName", teamToAdd.TeamName);
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
        //VIEW TEAMS
        //------------------------------------//

        public List<TeamsDAO> GetAllTeams()
        {
            List<TeamsDAO> teamList = new List<TeamsDAO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_TeamsTable_GetAllTeams", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TeamsDAO teamToList = new TeamsDAO();
                                teamToList.TeamID = reader.GetInt32(0);
                                teamToList.TeamName = reader.GetString(1);
                                teamToList.TeamRanking = reader.GetInt32(2);
                                teamList.Add(teamToList);
                            }
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Logger.LogError(error);
            }
            return teamList;
        }

        //------------------------------------//
        //UPDATE TEAMS
        //------------------------------------//

        public bool UpdateTeam(TeamsDAO teamToUpdate)
        {
            bool success = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_TeamsTable_UpdateTeam", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TeamID", teamToUpdate.TeamID);
                        command.Parameters.AddWithValue("@TeamName", teamToUpdate.TeamName);
                        command.Parameters.AddWithValue("@TeamRanking", teamToUpdate.TeamRanking);

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
        //DELETE TEAMS
        //------------------------------------//

        public bool DeleteTeam(int TeamID)
        {
            bool success = false;

            //THIS TRY ATTEMPTS TO OPEN A CONNECTION. IF IT SUCCEEDS, IT CLOSES THE CONNECTION. 
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_TeamsTable_DeleteTeam", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TeamID", TeamID);
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
        //GET TEAMS BY ID
        //------------------------------------//

        public TeamsDAO GetTeamByID(int TeamID)
        {
            TeamsDAO teamToReturn = new TeamsDAO();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_TeamsTable_GetTeamByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TeamID", TeamID);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                teamToReturn.TeamID = reader.GetInt32(0);
                                teamToReturn.TeamName = reader.GetString(1);
                                teamToReturn.TeamRanking = reader.GetInt32(2);
                            }
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Logger.LogError(error);
            }
            return teamToReturn;
        }
    }
}
