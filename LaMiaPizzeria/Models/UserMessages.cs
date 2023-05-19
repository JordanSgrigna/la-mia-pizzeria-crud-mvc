using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaMiaPizzeria.Models
{
    public class UserMessages
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo nome è obbligatorio!")]
        [StringLength(100, ErrorMessage = "Il campo nome non può avere più di 100 caratteri")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Il campo cognome è obbligatorio")]
        [StringLength(100, ErrorMessage = "Il campo cognome non può avere più di 100 caratteri")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Il campo email è obbligatorio!")]
        [EmailAddress(ErrorMessage = "L'email inserita non è valida")]
        [StringLength(100, ErrorMessage = "Il campo email non può avere più di 100 caratteri")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Il campo messaggio è obbligatorio")]
        [Column(TypeName = "text")]
        public string Message { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "datetime")]
        public DateTime MessageDateTime { get; set; } = DateTime.Now;


        public UserMessages(string name, string surname, string email, string message)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Message = message;
        }

        
        public UserMessages()
        {

        }
    }
}
