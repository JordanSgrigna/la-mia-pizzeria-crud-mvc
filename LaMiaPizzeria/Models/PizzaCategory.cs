using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaMiaPizzeria.Models
{
    public class PizzaCategory
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(50, ErrorMessage = "Il campo deve contenere meno di 50 caratteri")]
        public string Type { get; set; }

        public List<Pizza> Pizzas { get; set; }

        public PizzaCategory(string type)
        {
            Type = type;
            Pizzas = new List<Pizza>();
        }

        public PizzaCategory()
        {

        }
    }
}
