using BBSK_Psycho.Enums;
using BBSK_Psycho.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBSK_Psycho.Controllers
{
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {

        private readonly ILogger<ClientsController> _logger;

        public ClientsController(ILogger<ClientsController> logger)
        {
            _logger = logger;
        }


        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(int),StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public ActionResult <int> AddClient([FromBody] ClientRegisterRequest client)
        {
            int id = 2;
            return Created($"{Request.Scheme}://{Request.Host.Value}{Request.Path.Value}/{id}", id);
        }


        [AuthorizeByRole(Role.Client)]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult< ClientResponse> GetClientById([FromRoute] int id)
        {
            return Ok (new ClientResponse());
        }


        [AuthorizeByRole(Role.Client)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public ActionResult UpdateClientById([FromBody] ClientUpdateRequest request, [FromRoute] int id)
        {
            return NoContent();
        }


        [AuthorizeByRole(Role.Client)]
        [HttpGet("{id}/comments")]
        [ProducesResponseType(typeof(CommentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult <CommentResponse> GetCommentsByClientId([FromRoute] int id)
        {

            return Ok(new CommentResponse());
        }


        [AuthorizeByRole(Role.Client)]
        [HttpGet("{id}/orders")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult <OrderResponse> GetOrdersByClientId([FromRoute] int id)
        {
            return Ok(new OrderResponse());
        }


        [AuthorizeByRole(Role.Client)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpDelete("{id}")]
        public ActionResult DeleteClientById([FromRoute] int id)
        {
            return NoContent();
        }


        [Authorize(Roles = nameof(Role.Manager))]
        [HttpGet]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<ClientResponse> GetClients()
        {
            return Ok(new ClientResponse());
        }

    }
}
