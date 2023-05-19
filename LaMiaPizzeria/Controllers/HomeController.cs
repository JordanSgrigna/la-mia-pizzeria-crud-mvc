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
			return View();
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
	
		[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserMessages(UserMessages userMessages)
        {
            if (!ModelState.IsValid)
            {
                return View(userMessages);
            }
            else
            {
                using (PizzaShopContext db = new PizzaShopContext())
                {
                    db.UserMessages.Add(userMessages);
                    db.SaveChanges();

                    return RedirectToAction("ContactUs");
                }
            }
        }

		[HttpGet]
		public IActionResult UserMessages()
		{
			using(PizzaShopContext db = new PizzaShopContext())
			{
				List<UserMessages> usersMessages = db.UserMessages.ToList();
				return View(usersMessages);
			}
		}


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}