using LaMiaPizzeria.Database;
using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaMiaPizzeria.Controllers
{
    public class MenùController : Controller
    {
        public IActionResult Index()
        {
            using(PizzaShopContext db = new PizzaShopContext())
            {
                List<Pizza> ourPizzas = db.Pizza.ToList();
                return View(ourPizzas);
            }
            
        }
    }
}
