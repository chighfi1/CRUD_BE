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
        JsonResult AddPlayer(Player player, string teamName);
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
            string query = @"select * from dbo.Player";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BballCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
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

        public JsonResult AddPlayer(Player player, string teamName)
        {
            string query = @"insert into dbo.Player VALUES ((SELECT teamId from dbo.Team WHERE name = '" + teamName + @"'), '" + player.name + @"', '" + player.position + @"')";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BballCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
    }
}
