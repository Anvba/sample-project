using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DataScraper.Model;
using DataScraper.AdminAPI.DataAccess;
using DataAccess.AdminAPI.Model;

namespace data_scraper.admin_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;

		private readonly IScraperModelDataService<WebResourceModel> _scraperModelDataService;

        public AdminController(IScraperModelDataService<WebResourceModel> scraperModelDataService, 
							   ILogger<AdminController> logger)
        {
            _logger = logger;
			_scraperModelDataService = scraperModelDataService;
        }

        [HttpGet("{id}")]
        public WebResourceModel Get(string id)
        {
				_logger.LogInformation("Get document: {0}", id);
				return _scraperModelDataService.Find(id);
        }

        [HttpPost]
        public WebResourceModel Post([FromBody]WebResourceModel webResourceModel)
        {
			_scraperModelDataService.Create(webResourceModel);	
			return webResourceModel;	
        }
    }
}
