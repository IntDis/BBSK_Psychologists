using BBSK_Psycho.Infrastructure;
using System.ComponentModel.DataAnnotations;
using BBSK_Psycho.DataLayer.Enums;using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psycho.Models
{
    public class OrderCreateRequest
    {
        public int ClientId { get; set; }
        public int PsychologistId { get; set; }

        [Required(ErrorMessage = ApiErrorMessage.CostIsRequired)]
        public decimal Cost { get; set; }

        public SessionDuration Duration { get; set; }

        [Required(ErrorMessage = ApiErrorMessage.MessageIsRequired)]
        public string Message { get; set; }

        [Required(ErrorMessage = ApiErrorMessage.SessionDateIsRequired)]
        public DateTime SessionDate { get; set; }

        [Required(ErrorMessage = ApiErrorMessage.SessionDateIsRequired)]
        public DateTime OrderDate { get; set; }

        public DateTime? PayDate { get; set; }
        public OrderStatus OrderStatus { get; set; }

        [Required(ErrorMessage = ApiErrorMessage.OrderPaymentStatusIsRequired)]
        public OrderPaymentStatus OrderPaymentStatus { get; set; }
    }
}


