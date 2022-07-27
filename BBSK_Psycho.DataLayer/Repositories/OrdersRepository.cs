using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;

namespace BBSK_Psycho.DataLayer.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly BBSK_PsychoContext _context;

        public OrdersRepository(BBSK_PsychoContext context)
        {
            _context = context;
        }

        public List<Order> GetOrders() => _context.Orders.Where(o => !o.IsDeleted).ToList();
        public Order? GetOrderByPsychIdAndClientId(int psychId, int clientId) => _context.Orders.FirstOrDefault(o => o.Psychologist.Id == psychId && o.Client.Id == clientId);

        public Order? GetOrderById(int id) => _context.Orders.FirstOrDefault(o => o.Id == id);
        

        public int AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();

            return order.Id;
        }

        public void DeleteOrder(int id)
        {
            Order order = _context.Orders.FirstOrDefault(o => o.Id == id);

            order.IsDeleted = true;

            _context.SaveChanges();
        }

        public void UpdateOrderStatus(int orderId, OrderStatus orderStatus, OrderPaymentStatus paymentStatus)
        {
            Order order = GetOrderById(orderId);
            order.OrderStatus = (OrderStatus)orderStatus;
            order.OrderPaymentStatus = (OrderPaymentStatus)paymentStatus;

            _context.Orders.Update(order);
            _context.SaveChanges();
        }

    }
}