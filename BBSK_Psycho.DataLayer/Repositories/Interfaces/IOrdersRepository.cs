using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;

namespace BBSK_Psycho.DataLayer.Repositories.Interfaces
{
    public interface IOrdersRepository
    {
        int AddOrder(Order order);
        void DeleteOrder(int id);
        List<Order> GetOrders();
        Order? GetOrderById(int id);
        void UpdateOrderStatuses(int orderId, OrderStatus orderStatus, OrderPaymentStatus orderPaymentStatus);
    }
}