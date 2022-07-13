using System.Collections.Generic;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.Extensions;
using BBSK_Psycho.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BBSK_Psycho.Controllers
{

    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Route("[controller]")]

    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = nameof(Role.Manager))]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void),StatusCodes.Status403Forbidden)]
        [HttpGet]
        public ActionResult<OrderResponse> GetAllOrders()
        {
            return Ok(new List <OrderResponse>());

        }


        [AuthorizeByRole(Role.Psychologist, Role.Client)]
        [HttpGet("{orderId}")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult<OrderResponse> GetOrderById([FromRoute] int orderId)
        {
            return Ok(new OrderResponse());
        }


        [AuthorizeByRole(Role.Client)]
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public ActionResult<int> AddOrder([FromBody] OrderCreateRequest request)
        {
            int id = 2;
            return Created($"{this.GetRequestPath()}/{id}", id);
        }


        [Authorize(Roles = nameof(Role.Manager))]
        [HttpDelete("{orderId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult DeleteOrderById([FromRoute] int orderId)
        {
            return NoContent();
        }

        [Authorize(Roles = nameof(Role.Manager))]
        [HttpPatch("{orderId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult UpdateOrderStatusById([FromRoute] int orderId, [FromBody] OrderStatusPatchRequest orderStatusPatch)
        {
            return NoContent();
        }

        
    }
}
