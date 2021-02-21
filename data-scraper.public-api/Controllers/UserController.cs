using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DataScraper.Model;

namespace data_scraper.admin_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<GameData> Get()
        {
			return new[]
			{
				new GameData
				{
					GameCountry = "Test GameCountry",
				 	GameLeague = "Test GameLeague",
					GameDate = "Test GameDate",
					GameTime = "Test GameTime",
					FirstTeam = "Test FirstTeam",
					SecondTeam = "Test SecondTeam",
					GameScore = "Test GameScore"
				}
			};
        }
    }
}
