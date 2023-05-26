using LaMiaPizzeria.Database;
using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LaMiaPizzeria.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}
		
		[HttpGet] // Viene messa di Default
		public IActionResult Index()
		{
			return View("~/Views/Home/Index.cshtml");
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[HttpGet]
		public IActionResult ContactUs()
		{
			return View("ContactUs");
		}
	



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}