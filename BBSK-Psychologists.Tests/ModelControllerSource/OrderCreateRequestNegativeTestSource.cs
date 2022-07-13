using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models;
using System.Collections;
using System;
using BBSK_Psycho.DataLayer.Enums;

namespace ModelControllerSource
{
    public class OrderCreateRequestNegativeTestSource : IEnumerable
    {
        Random random = new Random();

        public OrderCreateRequest GetOrderTestingModel()
        {
            OrderCreateRequest order = new OrderCreateRequest()
            {
                ClientId = random.Next(1, 100),
                Cost = random.Next(1000, 10000),
                Duration = (SessionDuration)random.Next(1, 3),
                Message = "blah-blah",
                SessionDate = DateTime.Now,
                OrderDate = DateTime.Now,
                PayDate = DateTime.Now,
                OrderPaymentStatus = (OrderPaymentStatus)random.Next(1, 4)
            };

            return order;
        }

        public IEnumerator GetEnumerator()
        {
            OrderCreateRequest order = GetOrderTestingModel();
            order.Message = null;

            yield return new object[] //Message
            {
                order,
                ApiErrorMessage.MessageIsRequired
            };
        }
    }
}
