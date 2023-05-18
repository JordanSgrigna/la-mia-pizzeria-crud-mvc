using System.ComponentModel.DataAnnotations;

namespace LaMiaPizzeria.Models.CustomValidation
{
    public class DescriptionValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string fieldInput = (string)value;

            if (fieldInput == null ||  fieldInput.Split(" ").Length < 5)
            {
                return new ValidationResult("La descrizione deve contenere almeno 5 parole");
            }

            return ValidationResult.Success;
        }
    }
}
