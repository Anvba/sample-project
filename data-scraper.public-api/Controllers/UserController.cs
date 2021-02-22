using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DataScraper.DataAccess;
using DataScraper.PublicApi.Model;

namespace data_scraper.admin_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

		private readonly IGameDataModelService<GameDataModel> _gameDataModelService;

        public UserController(IGameDataModelService<GameDataModel> gameDataModelService, 
							  ILogger<UserController> logger)
        {
            _logger = logger;
			_gameDataModelService = gameDataModelService;
        }

        [HttpGet("{id}")]
        public GameDataModel Get(string id)
        {
				_logger.LogInformation("Get document: {0}", id);
				return _gameDataModelService.Find(id);
        }
    }
}
