using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;

namespace BBSK_Psycho.BusinessLayer.Services.Interfaces
{
    public interface IOrdersService
    {
        int AddOrder(Order order, ClaimModel claim);
        void DeleteOrder(int id, ClaimModel claim);
        Order? GetOrderById(int id, ClaimModel claim);
        List<Order> GetOrders(ClaimModel claim);
        void UpdateOrderStatuses(int id, OrderStatus orderStatus, OrderPaymentStatus orderPaymentStatus, ClaimModel claim);
    }
}