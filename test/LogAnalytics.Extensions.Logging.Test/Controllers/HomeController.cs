using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogAnalytics.Extensions.Logging.Test.Controllers
{
    public class HomeController : Controller
    {
		private ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger) {
			_logger = logger;
		}

		public IActionResult Index()
		{
			_logger.LogCritical("Something odd happened", new InvalidOperationException("Yikes..."));
			return NoContent();
		}
    }
}
