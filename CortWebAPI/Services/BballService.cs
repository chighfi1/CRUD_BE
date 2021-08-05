using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CortWebAPI.Services
{
    public interface IBballService
    {
        EnumerableRowCollection<Player> GetAllPlayers();
        int AddPlayer(Player player, string teamName);
    }

    public class BballService : IBballService
    {
        private readonly IConfiguration _configuration;

        public BballService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public EnumerableRowCollection<Player> GetAllPlayers()
        {
            string query = "SelectPlayers";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BballCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    /*
                    myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    myCommand.Parameters.Add(new SqlParameter("@teamName", teamName));
                    myCommand.ExecuteNonQuery();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                    */
                }
            }

            var convertedLists = (from rw in table.AsEnumerable()
                                  select new Player()
                                  {
                                      name = Convert.ToString(rw["name"]),
                                      position = Convert.ToString(rw["position"]),
                                      TeamId = Convert.ToInt32(rw["teamId"]),
                                      Id = Convert.ToInt32(rw["playerId"])
                                  }
                );
            return convertedLists;
        }

        public int AddPlayer(Player player, string teamName)
        {
            string query = "InsertPlayer";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BballCon");
            var playerId = 0;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    myCommand.Parameters.Add(new SqlParameter("@teamName", teamName));
                    myCommand.Parameters.Add(new SqlParameter("@name", player.name));
                    myCommand.Parameters.Add(new SqlParameter("@position", player.position));
                    myCommand.Parameters.Add("@PlayerId", SqlDbType.Int).Direction=ParameterDirection.Output;                    
                    playerId = (int)myCommand.Parameters["@PlayerId"].Value;
                    myCon.Close();
                }
            }

            return playerId;
        }
    }
}
