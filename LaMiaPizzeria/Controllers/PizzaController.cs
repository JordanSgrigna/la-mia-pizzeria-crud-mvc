using LaMiaPizzeria.Database;
using LaMiaPizzeria.Models;
using LaMiaPizzeria.Models.ModelForViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LaMiaPizzeria.Controllers
{
    public class PizzaController : Controller
    {
        [HttpGet]
        [Authorize]
        public IActionResult PizzaDetails(int id)
        {
            using (PizzaShopContext db = new PizzaShopContext())
            {
                Pizza? pizzaDetails = db.Pizza.Where(pizza => pizza.Id == id).Include(pizza => pizza.Category).FirstOrDefault();

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
        public IActionResult AddPizza()
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
        public IActionResult AddPizza(PizzaListCategory data)
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

                    return RedirectToAction("Index", "Menù");
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
                    List<PizzaCategory> pizzaCategories = db.PizzaCategories.ToList();

                    PizzaListCategory modelView = new PizzaListCategory();
                    modelView.Pizza = pizzaToUpdate;
                    modelView.PizzaCategory = pizzaCategories;

                    return View(modelView);
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
        public IActionResult UpdatePizza(int id, PizzaListCategory data)
        {
            if (!ModelState.IsValid)
            {
                using (PizzaShopContext db = new PizzaShopContext())
                {
                    List<PizzaCategory> pizzaCategories = new List<PizzaCategory>();
                    data.PizzaCategory = pizzaCategories;
                    return View(data);
                }
            }
                   
            using (PizzaShopContext db = new PizzaShopContext())
            {
                Pizza? pizzaToModify = db.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();
                if(pizzaToModify != null)
                {
                    pizzaToModify.Name = data.Pizza.Name;
                    pizzaToModify.Description = data.Pizza.Name;
                    pizzaToModify.Price = data.Pizza.Price;
                    pizzaToModify.ImageUrl = data.Pizza.ImageUrl;
                    pizzaToModify.CategoryId = data.Pizza.CategoryId;

                    db.SaveChanges();
                    return RedirectToAction("Index", "Menù");
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
                    return RedirectToAction("Index", "Menù");
                }
                else
                {
                    return NotFound("Non esiste una pizza da eliminare con questo id");
                }
            }
        }
    }

}
