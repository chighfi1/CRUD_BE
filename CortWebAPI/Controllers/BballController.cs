using CortWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CortWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BballController : ControllerBase
    {
        private readonly ILogger<BballController> _logger;
        private readonly IConfiguration _configuration;
        private IMemoryCache _cache;
        private IBballService _bballService;

        public BballController(ILogger<BballController> logger, IConfiguration configuration, IMemoryCache memoryCache, IBballService bballService)
        {
            _logger = logger;
            _configuration = configuration;
            _cache = memoryCache;
            _bballService = bballService;
        }
        [HttpGet]
        public EnumerableRowCollection<Player> Get()
        {
            return _bballService.GetAllPlayers();
            
        }
        [HttpPost]
        public int Post(Player player, [FromQuery(Name = "teamName")] string teamName)
        {
            return _bballService.AddPlayer(player, teamName);
        }
    }
    
}
