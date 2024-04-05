using Microsoft.AspNetCore.Mvc;
using Support_Manager.Models;
using System.Diagnostics;

namespace Support_Manager.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult TheEnding()
		{
			return View();
		}

        public IActionResult AllCase()
        {
            return View();
        }

        public IActionResult CaseDetails()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
