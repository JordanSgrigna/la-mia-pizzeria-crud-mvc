using LaMiaPizzeria.Models.CustomValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaMiaPizzeria.Models
{
    public class Pizza
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo nome è obbligatorio!")]
        [StringLength(100, ErrorMessage = "Il campo nome può contenere al massimo 100 caratteri")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Il campo descrizione è obbligatorio!")]
        [DescriptionValidation(ErrorMessage = "Il campo descrizione deve contenere più di 5 parole")]
        [Column(TypeName = "text")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Il campo prezzo è obbligatorio!")]
        [PriceValidation(ErrorMessage = "Il prezzo deve essere maggiore di 0")]
        [Column(TypeName = "decimal(5,2)")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Il campo nome è obbligatorio!")]
        [Url(ErrorMessage = "L'URL inserito non è valido!")]
        [StringLength(500, ErrorMessage = "Il campo URL immagine può contenere al massimo 500 caratteri")]
        public string ImageUrl { get; set; }


        public Pizza(string name, string description, string imageUrl)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
        }

        public Pizza()
        {

        }
    }
}
