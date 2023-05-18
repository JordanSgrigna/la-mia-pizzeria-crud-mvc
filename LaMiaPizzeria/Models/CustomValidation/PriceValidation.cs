using System.ComponentModel.DataAnnotations;

namespace LaMiaPizzeria.Models.CustomValidation
{
    public class PriceValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            float fieldValue = (float)value;

            if (fieldValue <= 0.0f )
            {
                return new ValidationResult("Il campo prezzo deve avere un prezzo maggiore di 0");
            }

            return ValidationResult.Success;
        }
    }
}
