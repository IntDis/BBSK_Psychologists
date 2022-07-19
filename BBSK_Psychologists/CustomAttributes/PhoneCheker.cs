using BBSK_Psycho.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BBSK_Psycho.CustomAttributes;

public class PhoneCheker : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var phoneNumber = value.ToString();

        if(!(phoneNumber.StartsWith("+7") || phoneNumber.StartsWith("8")))
        {
            return new ValidationResult(ApiErrorMessage.InvalidPhoneNumber);

        }

        if (phoneNumber.Length > 12)
        {
            return new ValidationResult(ApiErrorMessage.LengthExceeded);

        }

        return null;
    }
}
