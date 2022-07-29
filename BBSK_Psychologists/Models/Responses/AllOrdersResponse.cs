using System;
using BBSK_Psycho.DataLayer.Enums;

namespace BBSK_Psycho.Models.Responses
{
    public class AllOrdersResponse
    {
        public int Id { get; set; }
        public decimal Cost { get; set; }
        public SessionDuration Duration { get; set; }
        public string Message { get; set; }
        public DateTime SessionDate { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime PayDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public OrderPaymentStatus OrderPaymentStatus { get; set; }
        public bool IsDeleted { get; set; }
    }
}
