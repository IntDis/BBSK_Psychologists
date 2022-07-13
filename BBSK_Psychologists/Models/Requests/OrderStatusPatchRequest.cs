using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psycho.Models
{
    public class OrderStatusPatchRequest
    {
        [Required(ErrorMessage = ApiErrorMessage.OrderStatusIsRequired)]
        public OrderStatus OrderStatus { get; set; }

        [Required(ErrorMessage = ApiErrorMessage.OrderPaymentStatusIsRequired)]
        public OrderPaymentStatus OrderPaymentStatus { get; set; }
    }
}