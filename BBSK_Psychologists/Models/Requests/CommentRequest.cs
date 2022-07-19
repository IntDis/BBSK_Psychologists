using BBSK_Psycho.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psycho.Models.Requests
{
    public class CommentRequest
    {
        public int ClientId { get; set; }

        public int PsychologistId { get; set; }

        public string? Text { get; set; }

        [Range(1,5, ErrorMessage =ApiErrorMessage.RatingIsRequired)]
        public int Rating { get; set; }     // Оценка от 1 до 5 

        public DateTime Date { get; set; }

    }
}
