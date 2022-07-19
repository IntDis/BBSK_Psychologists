using BBSK_Psycho.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psycho.CustomAttributes;

public class ClientBirthDate : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateTime birthDate = DateTime.Parse(value.ToString());
        

        if (birthDate > DateTime.Now)
        {
             return new ValidationResult(ApiErrorMessage.InvalidDate);
        }
        

        return null;
    }
}
