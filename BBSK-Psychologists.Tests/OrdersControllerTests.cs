using BBSK_Psycho.DataLayer.Repositories.Interfaces;
using BBSK_DataLayer.Tests.TestCaseSources;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BBSK_Psycho.Controllers;
using BBSK_Psycho.Models;
using NUnit.Framework;
using AutoMapper;
using Moq;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.BusinessLayer;
using BBSK_Psycho;
using BBSK_Psycho.Models.Responses;

namespace BBSK_Psychologists.Tests
{
    public class OrdersControllerTests
    {
        private OrdersController _sut;
        private Mock<IOrdersService> _ordersService;
        private ClaimModel _claimModel;
        private IMapper _mapper;


        [SetUp]
        public void Setup()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MapperConfigStorage>()));
            _ordersService = new Mock<IOrdersService>();
            _sut = new OrdersController(_ordersService.Object, _mapper);
            _claimModel = new ClaimModel();
        }

        [Test]
        public void GetOrders_NoValidationRequired_RequestedTypeReceived()
        {
            //given
            List<Order> allOrders = new List<Order>();

            allOrders.Add(OrdersHelper.GetOrder());
            allOrders.Add(OrdersHelper.GetOrder());

            _ordersService.Setup(c => c.GetOrders(It.IsAny<ClaimModel>())).Returns(allOrders);

            //when
            var actual = _sut.GetOrders();


            //then
            var actualResult = actual.Result as ObjectResult;
            var actualValues = actualResult.Value as List<AllOrdersResponse>;

            Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
            
            Assert.IsNotNull(actual);
            
            for(int i = 0; i < allOrders.Count; i++)
            {
                Assert.AreEqual(actualValues[i].Duration, allOrders[i].Duration);
                Assert.AreEqual(actualValues[i].IsDeleted, allOrders[i].IsDeleted);
                Assert.AreEqual(actualValues[i].Message, allOrders[i].Message);
                Assert.AreEqual(actualValues[i].OrderDate, allOrders[i].OrderDate);
                Assert.AreEqual(actualValues[i].OrderPaymentStatus, allOrders[i].OrderPaymentStatus);
                Assert.AreEqual(actualValues[i].OrderStatus, allOrders[i].OrderStatus);
                Assert.AreEqual(actualValues[i].PayDate, allOrders[i].PayDate);
                Assert.AreEqual(actualValues[i].SessionDate, allOrders[i].SessionDate);
            }

            _ordersService.Verify(c => c.GetOrders(It.IsAny<ClaimModel>()), Times.Once);
        }

        [Test]
        public void GetOrderById_ValidIdPassed_OkReceived()
        {
            //given
            var expectedOrder = OrdersHelper.GetOrder();

            _ordersService.Setup(c => c.GetOrderById(expectedOrder.Id, It.IsAny<ClaimModel>())).Returns(expectedOrder);

            //when
            var actual = _sut.GetOrderById(expectedOrder.Id);

            //then
            var actualResult = actual.Result as ObjectResult;
            var actualValue = actualResult.Value as OrderResponse;


            Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
            Assert.AreEqual(actualResult.Value.GetType(), typeof(OrderResponse));

            Assert.AreEqual((SessionDuration)actualValue.Duration, expectedOrder.Duration);
            Assert.AreEqual(actualValue.Message, expectedOrder.Message);
            Assert.AreEqual(actualValue.OrderDate, expectedOrder.OrderDate);
            Assert.AreEqual((OrderPaymentStatus)actualValue.OrderPaymentStatus, expectedOrder.OrderPaymentStatus);
            Assert.AreEqual(actualValue.OrderStatus, expectedOrder.OrderStatus);
            Assert.AreEqual(actualValue.PayDate, expectedOrder.PayDate);
            Assert.AreEqual(actualValue.SessionDate, expectedOrder.SessionDate);

            _ordersService.Verify(c => c.GetOrderById(It.IsAny<int>(), It.IsAny<ClaimModel>()), Times.Once);
        }

        [Test]
        public void AddOrder_ValidRequestPassed_CreatedResultReceived()
        {
            //given
            _ordersService.Setup(c => c.AddOrder(It.IsAny<Order>(), It.IsAny<ClaimModel>()));

            OrderCreateRequest givenRequest = new()
            {
                ClientId = 1,
                Cost = 1100,
                Duration = SessionDuration.OneAcademicHour,
                Message = "",
                OrderDate = DateTime.Now,
                OrderPaymentStatus = OrderPaymentStatus.Paid,
                OrderStatus = OrderStatus.Created,
                PayDate = DateTime.Now,
                PsychologistId = 2,
                SessionDate = DateTime.Now
            };

            //when
            var actual = _sut.AddOrder(givenRequest);

            //then
            var actualResult = actual.Result as CreatedResult;

            Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);

            _ordersService.Verify(c => c.AddOrder(It.IsAny<Order>(), It.IsAny<ClaimModel>()), Times.Once);
        }

        [Test]
        public void DeleteOrderById_ValidIdPassed_NoContentReceived()
        {
            //given
            Order givenOrder = new();

            _ordersService.Setup(c => c.DeleteOrder(givenOrder.Id, It.IsAny<ClaimModel>()));

            //when
            var actual = _sut.DeleteOrderById(givenOrder.Id);

            //then
            var actualResult = actual as NoContentResult;

            Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

            _ordersService.Verify(c => c.DeleteOrder(It.IsAny<int>(), _claimModel), Times.Once);
        }

        [Test]
        public void UpdateOrderStatusById_ValidRequestAndIdPassed_NoContentReceived()
        {
            //given
            Order givenOrder = OrdersHelper.GetOrder();

            var givenRequest = new OrderStatusPatchRequest() 
            { 
                OrderPaymentStatus = OrderPaymentStatus.MoneyReturned,
                OrderStatus = OrderStatus.Cancelled
            };

            _ordersService.Setup(c => c.UpdateOrderStatuses(givenOrder.Id, givenRequest.OrderStatus, givenRequest.OrderPaymentStatus, It.IsAny<ClaimModel>()));

            //when
            var actual = _sut.UpdateOrderStatusById(givenOrder.Id, givenRequest);

            //then
            var actualResult = actual as NoContentResult;

            Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

            _ordersService.Verify(c => c.UpdateOrderStatuses(givenOrder.Id, givenRequest.OrderStatus, givenRequest.OrderPaymentStatus, It.IsAny<ClaimModel>()), Times.Once);
        }
    }
}
