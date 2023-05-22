using LaMiaPizzeria.Database;
using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaMiaPizzeria.Controllers
{
    public class UserMessagesController : Controller
    {

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
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

                    return RedirectToAction("ContactUs", "Home");
                }
            }
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult UserMessages()
        {
            using (PizzaShopContext db = new PizzaShopContext())
            {
                List<UserMessages> usersMessages = db.UserMessages.ToList();
                return View(usersMessages);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public IActionResult DeleteMessage(int id)
        {
            using (PizzaShopContext db = new PizzaShopContext())
            {
                UserMessages? userMessageToDelete = db.UserMessages.Where(m => m.Id == id).FirstOrDefault();
                if (userMessageToDelete != null)
                {
                    db.Remove(userMessageToDelete);
                    db.SaveChanges();
                    return RedirectToAction("UserMessages");
                }
                else
                {
                    return NotFound("Il messaggio con questo id non esiste");
                }
            }
        }
    }
}
