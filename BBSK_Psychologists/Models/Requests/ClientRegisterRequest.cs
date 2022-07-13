using System;
using BBSK_Psycho.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psycho.Models;

public class ClientRegisterRequest
{
    [Required(ErrorMessage = ApiErrorMessage.NameIsRequired)]
   
    public string Name { get; set; }

    public string? LastName { get; set; }


    [Required(ErrorMessage = ApiErrorMessage.PasswordIsRequire)]
    [MinLength(8, ErrorMessage = ApiErrorMessage.PasswordLengthIsLessThanAllowed)]
    public string Password { get; set; }


    [Required(ErrorMessage = ApiErrorMessage.EmailIsRequire)]
    [EmailAddress(ErrorMessage = ApiErrorMessage.InvalidCharacterInEmail)]
    public string Email { get; set; }


    [MaxLength(12, ErrorMessage = ApiErrorMessage.LengthExceeded)]
    public string? PhoneNumber { get; set; }


    public DateTime? BirthDate { get; set; }
}