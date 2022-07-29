using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BBSK_Psycho.DataLayer.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly BBSK_PsychoContext _context;

        public OrdersRepository(BBSK_PsychoContext context)
        {
            _context = context;
        }

        public List<Order> GetOrders() => _context.Orders.AsNoTracking().Where(o => !o.IsDeleted).ToList();
        

        public Order? GetOrderById(int orderId) => _context.Orders.Include(o => o.Client).Include(o => o.Psychologist).FirstOrDefault(o => o.Id == orderId);
        

        public int AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();

            return order.Id;
        }

        public void DeleteOrder(int orderId)
        {
            Order order = _context.Orders.FirstOrDefault(o => o.Id == orderId);

            order.IsDeleted = true;

            _context.SaveChanges();
        }

        public void UpdateOrderStatuses(int orderId, OrderStatus orderStatus, OrderPaymentStatus orderPaymentStatus)
        {
            Order order = _context.Orders.FirstOrDefault(o => o.Id == orderId);

            order.OrderStatus = orderStatus;
            order.OrderPaymentStatus = orderPaymentStatus;

            _context.Orders.Update(order);
            _context.SaveChanges();
        }
    }
}