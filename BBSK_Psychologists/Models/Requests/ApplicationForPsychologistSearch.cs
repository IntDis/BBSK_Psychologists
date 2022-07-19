using System;
using BBSK_Psycho.Infrastructure;
using System.ComponentModel.DataAnnotations;
using BBSK_Psycho.DataLayer.Enums;

namespace BBSK_Psycho.Models
{
    public class ApplicationForPsychologistSearch
    {
        [Required(ErrorMessage = ApiErrorMessage.NameIsRequired)]
        public string Name { get; set; }


        [Required(ErrorMessage = ApiErrorMessage.PhoneNumberIsRequired)]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = ApiErrorMessage.DescriptionIsRequired)]
        public string Description { get; set; }


        [Required(ErrorMessage = ApiErrorMessage.PsychologistGenderIsRequired)]
        public Gender PsychologistGender { get; set; } // enum


        [Required(ErrorMessage = ApiErrorMessage.CostMinIsRequired)]
        public decimal CostMin { get; set; }


        [Required(ErrorMessage = ApiErrorMessage.CostMaxIsRequired)]
        public decimal CostMax { get; set; }


        [Required(ErrorMessage = ApiErrorMessage.DateIsRequired)]
        public DateTime Date { get; set; }


        [Required(ErrorMessage = ApiErrorMessage.TimeIsRequired)]
        public TimeOfDay Time { get; set; } // enum


        [Required(ErrorMessage = ApiErrorMessage.ClientIdIsRequired)]
        public int ClientId { get; set; }
    }
}