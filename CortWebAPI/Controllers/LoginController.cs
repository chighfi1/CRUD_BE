using CortWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CortWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public bool Post([FromBody] LoginUser user)
        {

            var _rsaHelper = new RsaHelper();
            try
            {
                var isAuth = false;
                var clearTextPassword = _rsaHelper.Decrypt(user.Password);
                isAuth = user.Name.Equals("person") && clearTextPassword.Equals("personpass");
                return isAuth;
            }
            catch (Exception ex)
            {
                // Log ex 
                return false;
            }
        }
    }
}
