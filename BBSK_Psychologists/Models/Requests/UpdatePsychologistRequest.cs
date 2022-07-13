using System;
using System.Collections.Generic;
using BBSK_Psycho.Infrastructure;
using System.ComponentModel.DataAnnotations;
using BBSK_Psycho.DataLayer;
using BBSK_Psycho.DataLayer.Enums;

namespace BBSK_Psycho.Models.Requests
{
    public class UpdatePsychologistRequest
    {
        
        [Required(ErrorMessage = ApiErrorMessage.NameIsRequired)]
        public string Name { get; set; }
        [Required(ErrorMessage = ApiErrorMessage.LastNameIsRequired)]
        public string LastName { get; set; }
        [Required(ErrorMessage = ApiErrorMessage.PatronymicIsRequired)]
        public string Patronymic { get; set; }
        [Required(ErrorMessage = ApiErrorMessage.PsychologistGenderIsRequired)]
        public Gender gender { get; set; }
        [Required(ErrorMessage = ApiErrorMessage.BirthDateIsRequired)]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = ApiErrorMessage.PhoneNumberIsRequired)]
        public string Phone { get; set; }

        [Required(ErrorMessage = ApiErrorMessage.PasswordIsRequire)]
        [MinLength(8, ErrorMessage = ApiErrorMessage.PasswordLengthIsLessThanAllowed)]
        public string Password { get; set; }
        [Required(ErrorMessage = ApiErrorMessage.EmailIsRequire)]
        public string Email { get; set; }
        [Required(ErrorMessage = ApiErrorMessage.WorkExperienceIsRequired)]
        public int WorkExperience { get; set; }
        [Required(ErrorMessage = ApiErrorMessage.PassportDataIsRequired)]
        public string PasportData { get; set; }
        [Required(ErrorMessage = ApiErrorMessage.EducationIsRequired)]
        public List<string> Education { get; set; }  // "2013 - Московский Государственный Университет - Факультет - Степень; Dev Education"
        [Required()]
        public CheckStatus checkStatus { get; set; }        //Enum
        [Required(ErrorMessage = ApiErrorMessage.TherapyMethodsIsRequired)]
        public List<string>? TherapyMethods { get; set; }
        [Required(ErrorMessage = ApiErrorMessage.ProblemsIsRequired)]
        public List<string>? Problems { get; set; }
        [Required(ErrorMessage = ApiErrorMessage.CostIsRequired)]
        public decimal Price { get; set; }
        [Required]
        public Dictionary<String, List<String>> Schedule { get; set; }
    }
}
