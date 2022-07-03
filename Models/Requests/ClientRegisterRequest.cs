using BBSK_Psycho.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psycho.Models;

public class ClientRegisterRequest
{
    [Required(ErrorMessage = ApiErrorMessage.NameIsRequired)]
    [MaxLength(50, ErrorMessage = ApiErrorMessage.TheNumberOfCharactersExceedsTheAllowedValue)]
    public string Name { get; set; }

    [MaxLength(50, ErrorMessage = ApiErrorMessage.TheNumberOfCharactersExceedsTheAllowedValue)]
    public string? LastName { get; set; }


    [Required(ErrorMessage = ApiErrorMessage.PasswordIsRequire)]
    [MinLength(8, ErrorMessage = ApiErrorMessage.PasswordLengthIsLessThanAllowed)]
    [MaxLength(50, ErrorMessage = ApiErrorMessage.TheNumberOfCharactersExceedsTheAllowedValue)]
    public string Password { get; set; }


    [Required(ErrorMessage = ApiErrorMessage.EmailIsRequire)]
    [EmailAddress(ErrorMessage = ApiErrorMessage.InvalidCharacterInEmail)]
    [MaxLength(50, ErrorMessage = ApiErrorMessage.TheNumberOfCharactersExceedsTheAllowedValue)]
    public string Email { get; set; }


    
    public string? PhoneNumber { get; set; }



    public DateTime? BirthDate { get; set; }
}
