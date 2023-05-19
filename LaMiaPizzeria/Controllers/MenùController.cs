using LaMiaPizzeria.Database;
using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LaMiaPizzeria.Controllers
{
    public class MenùController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            using(PizzaShopContext db = new PizzaShopContext())
            {
                List<Pizza> ourPizzas = db.Pizza.ToList();
                return View(ourPizzas);
            }
            
        }

        [HttpGet]
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

        //CREATE
        [HttpGet]
        public IActionResult AggiungiPizza()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AggiungiPizza(Pizza newPizza)
        {
            if (!ModelState.IsValid)
            {
                return View(newPizza);
            }
            else
            {
                using (PizzaShopContext db = new PizzaShopContext())
                {
                    db.Pizza.Add(newPizza);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
        }

        //UPDATE
        [HttpGet]
        public IActionResult UpdatePizza(int id)
        {
            using(PizzaShopContext db = new PizzaShopContext())
            {
                Pizza? pizzaToUpdate = db.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();
                if (pizzaToUpdate != null)
                {
                    return View(pizzaToUpdate);
                }
                else
                {
                    return NotFound("La pizza con questo id non esiste");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePizza(int id, Pizza updatedPizza)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedPizza);
            }
                   
            using (PizzaShopContext db = new PizzaShopContext())
            {
                Pizza? pizzaToModify = db.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();
                if(pizzaToModify != null)
                {
                    pizzaToModify.Name = updatedPizza.Name;
                    pizzaToModify.Description = updatedPizza.Description;
                    pizzaToModify.Price = updatedPizza.Price;
                    pizzaToModify.ImageUrl = updatedPizza.ImageUrl;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("Non esiste la pizza con questo id");
                }
            }
            
        }

        //DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePizza(int id)
        {
            using (PizzaShopContext db = new PizzaShopContext())
            {
                Pizza? pizzaToDelete = db.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();
                if(pizzaToDelete != null)
                {
                    db.Remove(pizzaToDelete);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("Non esiste una pizza da eliminare con questo id");
                }
            }
        }
    }

}
