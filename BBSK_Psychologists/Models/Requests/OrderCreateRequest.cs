using System;
using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models.Responses;
using System.ComponentModel.DataAnnotations;
using BBSK_Psycho.DataLayer.Enums;

namespace BBSK_Psycho.Models
{
    public class OrderCreateRequest
    {
        
        public int ClientId { get; set; }


        [Required(ErrorMessage = ApiErrorMessage.CostIsRequired)]
        public decimal Cost { get; set; }

        
        public int Duration { get; set; }

        [Required(ErrorMessage = ApiErrorMessage.MessageIsRequired)]
        public string Message { get; set; }

        [Required(ErrorMessage = ApiErrorMessage.SessionDateIsRequired)]
        public DateTime SessionDate { get; set; }

        [Required(ErrorMessage = ApiErrorMessage.SessionDateIsRequired)]
        public DateTime OrderDate { get; set; }

        public DateTime? PayDate { get; set; }


        [Required(ErrorMessage = ApiErrorMessage.OrderPaymentStatusIsRequired)]
        public OrderPaymentSatus OrderPaymentStatus { get; set; }
    }

}


