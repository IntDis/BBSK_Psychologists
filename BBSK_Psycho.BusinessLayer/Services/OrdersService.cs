﻿using BBSK_Psycho.BusinessLayer.Exceptions;
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
        private readonly IOrdersValidator _ordersValidator;

        public OrdersService(IOrdersRepository ordersRepository,
                             IClientsRepository clientsRepository,
                             IPsychologistsRepository psychologistsRepository,
                             IOrdersValidator ordersValidator)
        {
            _ordersRepository = ordersRepository;
            _clientsRepository = clientsRepository;
            _psychologistsRepository = psychologistsRepository;
            _ordersValidator = ordersValidator;
        }

        public List<Order> GetOrders(ClaimModel claim)
        {
            _ordersValidator.CheckClaimForRoles(claim, Role.Manager);

            return _ordersRepository.GetOrders();
        }


        public Order? GetOrderById(int id, ClaimModel claim)
        {

            Order? order = _ordersRepository.GetOrderById(id);

            if (order == null)
                throw new EntityNotFoundException($"Заказ с ID {id} не найден");

            _ordersValidator.CheckClaimForEmail(claim, order);

            return order;
        }

        public int AddOrder(Order order, ClaimModel claim)
        {
            _ordersValidator.CheckClaimForRoles(claim, Role.Manager, Role.Client);

            Psychologist? psychologist = _psychologistsRepository.GetPsychologist(order.Psychologist.Id);
            
            if (psychologist == null)
                throw new EntityNotFoundException($"Психолог с ID {order.Psychologist.Id} не найден");

            order.Psychologist = psychologist;

            _ordersValidator.IsOrderValid(order);

            Client? client = _clientsRepository.GetClientById(order.Client.Id).Result;

            if (client == null)
                throw new EntityNotFoundException($"Клиент c ID {order.Client.Id} не найден");

            order.Client = client;

            _ordersValidator.CheckClaimForEmail(claim, order);

            return _ordersRepository.AddOrder(order);
        }

        public void DeleteOrder(int id, ClaimModel claim)
        {
            _ordersValidator.CheckClaimForRoles(claim, Role.Manager);

            Order? order = _ordersRepository.GetOrderById(id);

            if (order == null)
                throw new EntityNotFoundException($"Заказ с ID {id} не был найден");

            _ordersRepository.DeleteOrder(id);
        }

        public void UpdateOrderStatuses(int id, OrderStatus orderStatus, OrderPaymentStatus orderPaymentStatus, ClaimModel claim)
        {
            _ordersValidator.CheckClaimForRoles(claim, Role.Manager);

            Order? order = _ordersRepository.GetOrderById(id);

            if (order == null)
                throw new EntityNotFoundException($"Заказ с ID {id} не найден");

            _ordersValidator.AreOrderStatusesValid(orderStatus, orderPaymentStatus);

            _ordersRepository.UpdateOrderStatuses(id, orderStatus, orderPaymentStatus);
        }
    }
}