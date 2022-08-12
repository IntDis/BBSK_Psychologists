﻿using BBSK_DataLayer.Tests.TestCaseSources;
using BBSK_Psycho.BusinessLayer.Exceptions;
using BBSK_Psycho.BusinessLayer.Services;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.BusinessLayer.Tests.ModelControllerSource;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.BusinessLayer.Tests
{
    public class OrdersServiceTest
    {
        private OrdersService _sut;
        private IOrdersValidator _ordersValidator;
        private Mock<IOrdersRepository> _ordersRepository;
        private Mock<IClientsRepository> _clientsRepository;
        private Mock<IPsychologistsRepository> _psychologistsRepository;
        private ClaimModel _claimModel;

        [SetUp]
        public void Setup()
        {
            _ordersRepository = new Mock<IOrdersRepository>();
            _clientsRepository = new Mock<IClientsRepository>();
            _psychologistsRepository = new Mock<IPsychologistsRepository>();
            _ordersValidator = new OrdersValidator();
            _claimModel = new ClaimModel();
            _sut = new OrdersService(_ordersRepository.Object, 
                                     _clientsRepository.Object, 
                                     _psychologistsRepository.Object,
                                     _ordersValidator);
        }

        [Test]
        public void GetOrders_ValidRolePassed_OrdersReturned()
        {
            //given
            List<Order> orders = new List<Order>()
            {
                OrdersHelper.GetOrder(),
                OrdersHelper.GetOrder()
            };

            _claimModel.Role = Role.Manager;

            _ordersRepository.Setup(o => o.GetOrders().Result).Returns(orders);

            //when
            List<Order> actual = _sut.GetOrders(_claimModel);

            //then
            Assert.AreEqual(orders.Count, actual.Count);
            Assert.AreEqual(orders[0], actual[0]);
            Assert.AreEqual(orders[1], actual[1]);

            _ordersRepository.Verify(c => c.GetOrders(),Times.Once());
        }

        [Test]
        public void GetOrders_InvalidRolePassed_ThrowAccessDeniedException()
        {
            //given
            _claimModel.Role = Role.Client;

            //when-then
            Assert.Throws<AccessDeniedException>(() => _sut.GetOrders(_claimModel));

            _ordersRepository.Verify(c => c.GetOrders(), Times.Never());
        }

        [TestCase(Role.Client)]
        [TestCase(Role.Psychologist)]
        public void GetOrderById_CalledByValidUser_OrderReturned(Role role)
        {
            //given
            Order order = OrdersHelper.GetOrder();
            order.Id = 42;

            _claimModel.Role = role;

            if (role == Role.Client)
                _claimModel.Email = order.Client.Email;
            else
                _claimModel.Email = order.Psychologist.Email;

            _ordersRepository.Setup(c => c.GetOrderById(order.Id).Result).Returns(order);

            //when
            Order actualOrder = _sut.GetOrderById(order.Id, _claimModel);

            //then
            Assert.AreEqual(order, actualOrder);

            _ordersRepository.Verify(c => c.GetOrderById(order.Id), Times.Once);
        }

        [TestCase(Role.Client)]
        [TestCase(Role.Psychologist)]
        public void GetOrderById_OrderNotFound_ThrowEntityNotFoundException(Role role)
        {
            //given
            _claimModel.Role = role;

            _ordersRepository.Setup(c => c.GetOrderById(It.IsAny<int>()).Result).Returns((Order)null);

            //when-then
            Assert.Throws<EntityNotFoundException>(() => _sut.GetOrderById(It.IsAny<int>(), _claimModel));
            _ordersRepository.Verify(c => c.GetOrderById(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void GetOrderById_CalledByInvalidUser_ThrowAccessDeniedException()
        {
            //given
            Order order = OrdersHelper.GetOrder();
            order.Id = 42;

            _claimModel.Email = "eyes722@gmail.com";

            _ordersRepository.Setup(c => c.GetOrderById(order.Id).Result).Returns(order);

            //when-then
            Assert.Throws<AccessDeniedException>(() => _sut.GetOrderById(order.Id, _claimModel));
            _ordersRepository.Verify(c => c.GetOrders(), Times.Never());
        }

        [Test]
        public void AddOrder_ValidRequestPassed_AddedOrderAndReturnedId()
        {
            //given
            Order order = OrdersHelper.GetOrder();
            order.Id = 42;
            order.Psychologist = OrdersHelper.GetPsychologist();

            _claimModel = new() { Email = order.Client.Email, Id = order.Client.Id, Role = Role.Client };

            _psychologistsRepository.Setup(c => c.GetPsychologist(order.Psychologist.Id)).Returns(order.Psychologist);
            _clientsRepository.Setup(c => c.GetClientById(order.Client.Id).Result).Returns(order.Client);

            _ordersRepository.Setup(c => c.AddOrder(order).Result).Returns(42);

            //when
            int returnedId = _sut.AddOrder(order, _claimModel);

            //then
            Assert.AreEqual(order.Id, returnedId);

            _psychologistsRepository.Verify(c => c.GetPsychologist(order.Psychologist.Id), Times.Once);
            _clientsRepository.Verify(c => c.GetClientById(order.Client.Id), Times.Once);
        }

        [Test]
        public void AddOrder_PsychologistWasNotFound_ThrowEntityNotFoundException()
        {
            //given
            Order order = OrdersHelper.GetOrder();

            _claimModel.Role = Role.Client;

            _psychologistsRepository.Setup(c => c.GetPsychologist(order.Psychologist.Id)).Returns((Psychologist)null);

            //when-then
            Assert.Throws<EntityNotFoundException>(() => _sut.AddOrder(order, _claimModel));
            _psychologistsRepository.Verify(c => c.GetPsychologist(order.Psychologist.Id), Times.Once);
            _clientsRepository.Verify(c => c.GetClientById(order.Client.Id), Times.Never);
            _ordersRepository.Verify(c => c.AddOrder(It.IsAny<Order>()), Times.Never);
        }

        [Test]
        public void AddOrder_ClientWasNotFound_ThrowEntityNotFoundException()
        {
            //given
            Order order = OrdersHelper.GetOrder();
            order.Psychologist = OrdersHelper.GetPsychologist();
            _claimModel.Role = Role.Client;

            _psychologistsRepository.Setup(c => c.GetPsychologist(order.Psychologist.Id)).Returns(order.Psychologist);

            //when-then
            Assert.Throws<EntityNotFoundException>(() => _sut.AddOrder(order, _claimModel));
            _psychologistsRepository.Verify(c => c.GetPsychologist(order.Psychologist.Id), Times.Once);
            _clientsRepository.Verify(c => c.GetClientById(order.Client.Id), Times.Once);
            _ordersRepository.Verify(c => c.AddOrder(It.IsAny<Order>()), Times.Never);
        }

        [Test]
        public void DeleteOrder_ValidRequestPassed_OrderDeleted()
        {
            //given
            Order order = OrdersHelper.GetOrder();

            _claimModel.Role = Role.Manager;
            
            _ordersRepository.Setup(c => c.GetOrderById(order.Id).Result).Returns(order);
            _ordersRepository.Setup(c => c.DeleteOrder(order.Id));

            //when
            _sut.DeleteOrder(order.Id, _claimModel);

            //then
            _ordersRepository.Verify(c => c.DeleteOrder(order.Id), Times.Once);
            _ordersRepository.Verify(c => c.GetOrderById(order.Id), Times.Once);
        }

        [Test]
        public void DeleteOrder_OrderWasNotFound_ThrowEntityNotFoundException()
        {
            //given
            _claimModel.Role = Role.Manager;

            _ordersRepository.Setup(c => c.GetOrderById(It.IsAny<int>()).Result).Returns((Order)null);

            //when-then
            Assert.Throws<EntityNotFoundException>(() => _sut.GetOrderById(It.IsAny<int>(), _claimModel));

            _ordersRepository.Verify(c => c.GetOrderById(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void UpdateOrderStatuses_ValidRequestPassed_OrderUpdated()
        {
            //given
            Order order = OrdersHelper.GetOrder();
            order.OrderPaymentStatus = OrderPaymentStatus.Unpaid;
            order.OrderStatus = OrderStatus.Created;

            _claimModel.Role = Role.Manager;

            _ordersRepository.Setup(c => c.GetOrderById(order.Id).Result).Returns(order);
            _ordersRepository.Setup(c => c.UpdateOrderStatuses(order.Id, OrderStatus.Completed, OrderPaymentStatus.Paid));

            //when
            _sut.UpdateOrderStatuses(order.Id, OrderStatus.Completed, OrderPaymentStatus.Paid, _claimModel);

            //then
            Order expected = _sut.GetOrderById(order.Id, _claimModel);

            _ordersRepository.Verify(c => c.UpdateOrderStatuses(order.Id, OrderStatus.Completed, OrderPaymentStatus.Paid), Times.Once);
            _ordersRepository.Verify(c => c.GetOrderById(order.Id), Times.Exactly(2));
        }

        [Test]
        public void UpdateOrderStatuses_OrderWasNotFound_ThrowEntityNotFoundException()
        {
            //given
            _claimModel.Role = Role.Manager;

            _ordersRepository.Setup(c => c.GetOrderById(It.IsAny<int>()).Result).Returns((Order)null);

            //when-then
            Assert.Throws<EntityNotFoundException>(() => _sut.UpdateOrderStatuses(It.IsAny<int>(), It.IsAny<OrderStatus>(), It.IsAny<OrderPaymentStatus>(), _claimModel));

            _ordersRepository.Verify(c => c.UpdateOrderStatuses(It.IsAny<int>(), OrderStatus.Completed, OrderPaymentStatus.Paid), Times.Never);
            _ordersRepository.Verify(c => c.GetOrderById(It.IsAny<int>()), Times.Once);
        }
    }
}
