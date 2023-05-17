using LaMiaPizzeria.Database;
using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        public IActionResult Details(int id)
        {
            using (PizzaShopContext db = new PizzaShopContext())
            {
                Pizza? pizzaDetails = db.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();

                if(pizzaDetails != null)
                {
                    return View(pizzaDetails);
                }
                else
                {
                    return NotFound($"La pizza con id {id} non è stato trovato");
                }
            }
        }
    }

}
