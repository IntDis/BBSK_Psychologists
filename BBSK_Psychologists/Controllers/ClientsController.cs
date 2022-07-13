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
    public class ClientsController : ControllerBase
    {

        //private readonly ILogger<ClientsController> _logger;

        //public ClientsController(ILogger<ClientsController> logger)
        //{
        //    _logger = logger;
        //}


        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(int),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void),StatusCodes.Status422UnprocessableEntity)]
        public ActionResult <int> AddClient([FromBody] ClientRegisterRequest client)
        {
            int id = 2;
            return Created($"{this.GetRequestPath()}/{id}", id);
        }


        [AuthorizeByRole(Role.Client)]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void),StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult< ClientResponse> GetClientById([FromRoute] int id)
        {
            return Ok (new ClientResponse());
        }


        [AuthorizeByRole(Role.Client)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void),StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void),StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult UpdateClientById([FromBody] ClientUpdateRequest request, [FromRoute] int id)
        {
            return NoContent();
        }


        [AuthorizeByRole(Role.Client)]
        [HttpGet("{id}/comments")]
        [ProducesResponseType(typeof(CommentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void),StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult <CommentResponse> GetCommentsByClientId([FromRoute] int id)
        {

            return Ok(new List<CommentResponse>());
        }


        [AuthorizeByRole(Role.Client)]
        [HttpGet("{id}/orders")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void),StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult <OrderResponse> GetOrdersByClientId([FromRoute] int id)
        {
            return Ok(new List<OrderResponse>());
        }


        [AuthorizeByRole(Role.Client)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void),StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult DeleteClientById([FromRoute] int id)
        {
            return NoContent();
        }


        [Authorize(Roles = nameof(Role.Manager))]
        [HttpGet]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void),StatusCodes.Status403Forbidden)]
        public ActionResult<ClientResponse> GetClients()
        {
            return Ok(new List<ClientResponse>());
            
        }

    }
}
