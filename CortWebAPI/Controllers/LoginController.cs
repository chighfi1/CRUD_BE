using CortWebAPI.Models;
using CortWebAPI.Services.CortWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
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

        private IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public bool Post([FromBody] LoginUser user)
        {
            try
            {
                var isAuth = false;
                isAuth = _userService.AuthUser(user);
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
