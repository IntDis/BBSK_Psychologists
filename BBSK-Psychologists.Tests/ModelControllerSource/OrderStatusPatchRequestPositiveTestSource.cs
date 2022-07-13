using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models;
using System.Collections;
using System;
using BBSK_Psycho.DataLayer.Enums;

namespace BBSK_Psychologists.Tests.ModelControllerSource
{
    public class OrderStatusPatchRequestPositiveTestSource : IEnumerable
    {

        public OrderStatusPatchRequest GetOrderPatchingModel()
        {
            OrderStatusPatchRequest orderStatusPatchRequest = new OrderStatusPatchRequest()
            {
                OrderStatus = OrderStatus.Created,
                OrderPaymentStatus = OrderPaymentStatus.Unpaid
            };

            return orderStatusPatchRequest;
        }

        public IEnumerator GetEnumerator()
        {
            OrderStatusPatchRequest request = GetOrderPatchingModel();

            yield return new object[]
            {

            };
        }
    }
}
