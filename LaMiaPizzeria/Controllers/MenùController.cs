using LaMiaPizzeria.Database;
using LaMiaPizzeria.Models;
using LaMiaPizzeria.Models.ModelForViews;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
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
        [Authorize(Roles = "ADMIN")]
        public IActionResult AggiungiPizza()
        {
            using(PizzaShopContext db = new PizzaShopContext())
            {
                List<PizzaCategory> pizzaCategories = db.PizzaCategories.ToList();
                PizzaListCategory modelForView = new PizzaListCategory();
                modelForView.Pizza = new Pizza();
                modelForView.PizzaCategory = pizzaCategories;

                return View(modelForView);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public IActionResult AggiungiPizza(PizzaListCategory data)
        {
            if (!ModelState.IsValid)
            {
                using (PizzaShopContext db = new PizzaShopContext())
                {
                    List<PizzaCategory> pizzaCategory = db.PizzaCategories.ToList();
                    data.PizzaCategory = pizzaCategory;
                    return View(data);
                }
            }
            else
            {
                using (PizzaShopContext db = new PizzaShopContext())
                {
                    db.Pizza.Add(data.Pizza);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
        }

        //UPDATE
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
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
        [Authorize(Roles = "ADMIN")]
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
        [Authorize(Roles = "ADMIN")]
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
