using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CortWebAPI.Services
{
    using global::CortWebAPI.Models;
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
        public interface IUserService
        {
            bool AuthUser(LoginUser user);
        }

        public class UserService : IUserService
        {
            private readonly IConfiguration _configuration;

            public UserService(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public bool AuthUser(LoginUser user)
            {
                string query = "GetUser";

                var _rsaHelper = new RsaHelper();

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("BballCon");
                var match = false;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        var clearTextPassword = _rsaHelper.Decrypt(user.Password);
                        myCommand.Parameters.Add(new SqlParameter("@name", user.Name));
                        myCommand.Parameters.Add(new SqlParameter("@password", clearTextPassword));
                        myCommand.Parameters.Add("@match", SqlDbType.Int).Direction = ParameterDirection.Output;
                        match = (int)myCommand.Parameters["@match"].Value == 1;
                        myCon.Close();
                    }
                }

                return match;
            }
        }
    }

}
