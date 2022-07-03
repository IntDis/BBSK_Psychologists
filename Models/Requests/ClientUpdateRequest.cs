using BBSK_Psycho.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psycho.Models;

public class ClientUpdateRequest
{
    [Required(ErrorMessage = ApiErrorMessage.NameIsRequired)]
    [MaxLength(50, ErrorMessage = ApiErrorMessage.TheNumberOfCharactersExceedsTheAllowedValue)]
    public string Name { get; set; }

    [MaxLength(50, ErrorMessage = ApiErrorMessage.TheNumberOfCharactersExceedsTheAllowedValue)]
    public string? LastName { get; set; }

  
    public DateTime? BirthDate { get; set; }

}
