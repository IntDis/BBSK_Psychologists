using BBSK_Psycho.BusinessLayer.Exceptions;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.BusinessLayer.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IClientsRepository _clientsRepository;
        private readonly IPsychologistsRepository _psychologistsRepository;

        public OrdersService(IOrdersRepository ordersRepository,
                             IClientsRepository clientsRepository,
                             IPsychologistsRepository psychologistsRepository)
        {
            _ordersRepository = ordersRepository;
            _clientsRepository = clientsRepository;
            _psychologistsRepository = psychologistsRepository;
        }

        public List<Order> GetOrders(ClaimModel claim)
        {
            CheckClaimForRoles(claim, Role.Manager);

            return _ordersRepository.GetOrders();
        }


        public Order? GetOrderById(int id, ClaimModel claim)
        {
            CheckClaimForRoles(claim, Role.Manager, Role.Client, Role.Psychologist);

            Order? order = _ordersRepository.GetOrderById(id);

            if (order == null)
                throw new EntityNotFoundException($"Заказ с ID {id} не найден");

            CheckClaimForEmail(claim, order);

            return order;
        }

        public int AddOrder(Order order, ClaimModel claim)
        {
            CheckClaimForRoles(claim, Role.Manager, Role.Client);

            Psychologist? psychologist = _psychologistsRepository.GetPsychologist(order.Psychologist.Id);
            
            if (psychologist == null)
                throw new EntityNotFoundException($"Психолог с ID {order.Psychologist.Id} не найден");

            order.Psychologist = psychologist;

            IsOrderValid(order);

            Client? client = _clientsRepository.GetClientById(order.Client.Id);

            if (client == null)
                throw new EntityNotFoundException($"Клиент c ID {order.Client.Id} не найден");

            order.Client = client;

            CheckClaimForEmail(claim, order);

            return _ordersRepository.AddOrder(order);
        }

        public void DeleteOrder(int id, ClaimModel claim)
        {
            CheckClaimForRoles(claim, Role.Manager);

            Order? order = _ordersRepository.GetOrderById(id);

            if (order == null)
                throw new EntityNotFoundException($"Заказ с ID {id} не был найден");

            _ordersRepository.DeleteOrder(id);
        }

        public void UpdateOrderStatuses(int id, OrderStatus orderStatus, OrderPaymentStatus orderPaymentStatus, ClaimModel claim)
        {
            CheckClaimForRoles(claim, Role.Manager);

            Order? order = _ordersRepository.GetOrderById(id);

            if (order == null)
                throw new EntityNotFoundException($"Заказ с ID {id} не найден");

            if (!(orderStatus == OrderStatus.Completed && orderPaymentStatus == OrderPaymentStatus.Paid) ||
                 (orderStatus == OrderStatus.Created && orderPaymentStatus == OrderPaymentStatus.Paid) ||
                 (orderStatus == OrderStatus.Cancelled && orderPaymentStatus == OrderPaymentStatus.Unpaid) ||
                 (orderStatus == OrderStatus.Cancelled && orderPaymentStatus == OrderPaymentStatus.MoneyReturned) ||
                 (orderStatus == OrderStatus.Deleted && orderPaymentStatus == OrderPaymentStatus.MoneyReturned) ||
                 (orderStatus == OrderStatus.Deleted && orderPaymentStatus == OrderPaymentStatus.Unpaid))
                throw new DataException($"Статус заказа {orderStatus} и статус оплаты {orderPaymentStatus} не могут быть использованы вместе");

                _ordersRepository.UpdateOrderStatuses(id, orderStatus, orderPaymentStatus);
        }




        public void CheckClaimForRoles(ClaimModel claim, params Role[] roles)
        {
            if (!roles.Contains(claim.Role))
                throw new AccessDeniedException("Доступ запрещён");
        }
        public void CheckClaimForEmail(ClaimModel claim, Order order)
        {
            if (!((order.Client.Email == claim.Email) || 
                  (order.Psychologist.Email == claim.Email) ||
                   claim.Role == Role.Manager))
                throw new AccessDeniedException("Доступ запрещён");
        }

        public void IsOrderValid(Order order)
        {
            if (order.IsDeleted)
                throw new DataException($"Нельзя добавить удалённый заказ");
            else if (order.Cost < order.Psychologist.Price)
                throw new DataException($"Цена не может быть ниже ставки психолога");
            else if (order.SessionDate < order.OrderDate)
                throw new DataException($"Дата оказания услуги не может быть раньше даты создания заказа");
            else if (order.SessionDate > order.OrderDate.AddMonths(1))
                throw new DataException("Дата оказания услуги не может быть позднее, чем через 1 месяц от даты создания заказа");
            else if (order.PayDate < order.OrderDate)
                throw new DataException($"Дата оплаты заказа не может быть раньше даты создания заказа");
            else if (order.SessionDate < order.PayDate)
                throw new DataException($"Услуга должна быть оказана после оплаты закака");
            else if (!Enum.IsDefined(typeof(SessionDuration), order.Duration))
                throw new DataException($"Неверно указана длительность консультации");
            else if (order.Message.Trim() == "")
                throw new DataException($"Неверно указано сообщение для психолога");
        }
    }
}