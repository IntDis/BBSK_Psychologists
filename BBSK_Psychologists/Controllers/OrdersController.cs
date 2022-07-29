using System.Collections.Generic;
using AutoMapper;
using BBSK_Psycho.BusinessLayer.Services;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.Extensions;
using BBSK_Psycho.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BBSK_Psycho.BusinessLayer;
using BBSK_Psycho.Models.Responses;

namespace BBSK_Psycho.Controllers
{

    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Route("[controller]")]

    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        private readonly IMapper _mapper;
        
        public OrdersController(IOrdersService ordersService, IMapper mapper)
        {
            _ordersService = ordersService;
            _mapper = mapper;
        }

        [AuthorizeByRole]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void),StatusCodes.Status403Forbidden)]
        [HttpGet]
        public ActionResult<List<AllOrdersResponse>> GetOrders()
        {
            ClaimModel claim = this.GetClaims();

            return Ok(_mapper.Map<List<AllOrdersResponse>>(_ordersService.GetOrders(claim)));
        }


        [AuthorizeByRole(Role.Psychologist, Role.Client)]
        [HttpGet("{orderId}")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult<OrderResponse> GetOrderById([FromRoute] int orderId)
        {
            ClaimModel claim = this.GetClaims();

            Order order = _ordersService.GetOrderById(orderId, claim);

            return Ok(_mapper.Map<OrderResponse>(order));
        }


        [AuthorizeByRole(Role.Client)]
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<int> AddOrder([FromBody] OrderCreateRequest request)
        {
            ClaimModel claim = this.GetClaims();

            Order newOrder = _mapper.Map<Order>(request);

            newOrder.Client = new() {Id = request.ClientId};
            newOrder.Psychologist = new() {Id = request.PsychologistId};

            _ordersService.AddOrder(newOrder, claim);

            return Created($"{this.GetRequestPath()}/{newOrder.Id}", newOrder.Id);
        }


        [Authorize(Roles = nameof(Role.Manager))]
        [HttpDelete("{orderId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public ActionResult DeleteOrderById([FromRoute] int orderId)
        {
            ClaimModel claim = this.GetClaims();

            _ordersService.DeleteOrder(orderId, claim);

            return NoContent();
        }

        [Authorize(Roles = nameof(Role.Manager))]
        [HttpPatch("{orderId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]

        public ActionResult UpdateOrderStatusById([FromRoute] int orderId, [FromBody] OrderStatusPatchRequest orderStatusPatch)
        {
            ClaimModel claim = this.GetClaims();

            _ordersService.UpdateOrderStatuses(orderId, orderStatusPatch.OrderStatus, orderStatusPatch.OrderPaymentStatus, claim);

            return NoContent();
        } 
    }
}