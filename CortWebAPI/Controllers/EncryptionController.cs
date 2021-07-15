using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CortWebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CortWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncryptionController : ControllerBase
    {

        public class EncryptionResult
        {
            public string Encrypted { get; set; }
            public string Decrypted { get; set; }
        }

        // GET: api/<EncryptionController>
        [HttpGet]
        public EncryptionResult Get()
        {
            var rsaHelper = new RsaHelper();
            var s = "Password";

            return new EncryptionResult
            {
                Encrypted = "",
                Decrypted = ""
            };
        }        
    }
}
