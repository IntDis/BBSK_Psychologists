using System;

namespace BBSK_Psycho.Models.Responses
{
    public class GetCommentsByPsychologistIdResponse
    {
        public int PsychologistId { get; set; }

        public string Text { get; set; }

        public int Rating { get; set; }     // Оценка от 1 до 5 

        public DateTime Date { get; set; }
    }
}
