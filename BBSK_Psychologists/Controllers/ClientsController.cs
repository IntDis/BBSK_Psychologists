using AutoMapper;
using BBSK_Psycho.BusinessLayer.Services;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;
using BBSK_Psycho.Extensions;
using BBSK_Psycho.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BBSK_Psycho.Controllers
{
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsServices _clientsServices;

        private readonly IMapper _mapper;

        public ClientsController(IClientsServices clientsServices, IMapper mapper)
        {
            _clientsServices = clientsServices;
            _mapper = mapper;   
        }



        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<int> AddClient([FromBody] ClientRegisterRequest client)
        {
            var id = _clientsServices.AddClient(_mapper.Map<Client>(client));
            return Created($"{this.GetRequestPath()}/{id}", id);
        }


        [AuthorizeByRole(Role.Client)]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult<ClientResponse> GetClientById([FromRoute] int id)
        {
            List<ClaimsIdentity> identities = this.User.Identities.ToList();

            var client = _clientsServices.GetClientById(id, identities);

            if (client is null)
                return NotFound();
            else
                return Ok(_mapper.Map<ClientResponse>(client));
        }


        [AuthorizeByRole(Role.Client)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public ActionResult UpdateClientById([FromBody] ClientUpdateRequest request, [FromRoute] int id)
        {
            List<ClaimsIdentity> identities = this.User.Identities.ToList();

            var client = new Client()
            {
                Name = request.Name,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
            };
            

            _clientsServices.UpdateClient(client, id, identities);

            return NoContent();
        }


        [AuthorizeByRole(Role.Client)]
        [HttpGet("{id}/comments")]
        [ProducesResponseType(typeof(CommentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public ActionResult <List<CommentResponse>> GetCommentsByClientId([FromRoute] int id)
        {
            List<ClaimsIdentity> identities = this.User.Identities.ToList();

            var clientComents = _clientsServices.GetCommentsByClientId(id, identities);
            if (clientComents is null)
                return NotFound();
            else
                return Ok(_mapper.Map<List<CommentResponse>>(clientComents));
        }


        [AuthorizeByRole(Role.Client)]
        [HttpGet("{id}/orders")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public ActionResult <List<OrderResponse>> GetOrdersByClientId([FromRoute] int id)
        {
            List<ClaimsIdentity> identities = this.User.Identities.ToList();

            var clientOrders = _clientsServices.GetOrdersByClientId(id, identities);
            if(clientOrders is null)
                return NotFound();
            else
                return Ok(_mapper.Map<List<OrderResponse>>(clientOrders));
        }


        [AuthorizeByRole(Role.Client)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public ActionResult DeleteClientById([FromRoute] int id)
        {
            List<ClaimsIdentity> identities = this.User.Identities.ToList();
            

            var client = _clientsServices.GetClientById(id, null);

            if (client is null)
                return NotFound();
            else
                _clientsServices.DeleteClient(id, identities);
                return NoContent();
        }


        [Authorize(Roles = nameof(Role.Manager))]
        [HttpGet]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public ActionResult <List<ClientResponse>> GetClients()
        {
            var clients = _clientsServices.GetClients();
            return Ok(_mapper.Map <List<ClientResponse>>(clients));


        }

    }
}
