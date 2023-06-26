using System;
using System.ComponentModel.DataAnnotations;

namespace MVCBasico.Utils
{
    public class MayorDeEdadAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (((DateTime)value).CompareTo(DateTime.Now.AddYears(-18)) > 0)
            {
                return new ValidationResult("Debe ser mayor de edad");
            }

            return ValidationResult.Success;
        }
    }
}
