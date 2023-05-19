using LaMiaPizzeria.Models;
using Microsoft.EntityFrameworkCore;

namespace LaMiaPizzeria.Database
{
    public class PizzaShopContext : DbContext
    {
        public DbSet<Pizza> Pizza { get; set; }
        public DbSet<UserMessages> UserMessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=EFMyPizzaShop;" +
                "Integrated Security=True;TrustServerCertificate=True");
        }
    }
}
