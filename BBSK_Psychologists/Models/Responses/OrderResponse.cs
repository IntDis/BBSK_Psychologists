using System;
using BBSK_Psycho.DataLayer.Enums;

namespace BBSK_Psycho.Models.Responses

{

    public class OrderResponse
    {
        public int Id { get; set; }

        public PsychologistResponse psychologistResponse { get; set; }

        public decimal Cost { get; set; }

        public int Duration { get; set; }

        public string Message { get; set; } 

        public DateTime SessionDate { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime PayDate { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public OrderPaymentStatus OrderPaymentStatus { get; set; }
    }
}