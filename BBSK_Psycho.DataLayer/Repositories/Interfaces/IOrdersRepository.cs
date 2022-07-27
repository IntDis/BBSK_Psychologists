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
        void UpdateOrderStatus(int orderId, OrderStatus orderStatus, OrderPaymentStatus paymentStatus);
        Order? GetOrderByPsychIdAndClientId(int psychId, int clientId);
    }
}