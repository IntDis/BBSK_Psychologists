using AutoMapper;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;
using BBSK_Psycho.Extensions;
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
        private readonly IClientsRepository _clientsRepository;

        private readonly IMapper _mapper;

        public ClientsController(IClientsRepository clientsRepository, IMapper mapper)
        {
            _clientsRepository = clientsRepository;
            _mapper = mapper;   
        }
       


        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<int> AddClient([FromBody] ClientRegisterRequest client)
        {
            var id = _clientsRepository.AddClient(_mapper.Map<Client>(client));
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
            var client = _clientsRepository.GetClientById(id);

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
            var client = new Client()
            {
                Name = request.Name,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
            };
            

            _clientsRepository.UpdateClient(client, id);

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
            var clientComents = _clientsRepository.GetCommentsByClientId(id);
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
            var clientOrders = _clientsRepository.GetOrdersByClientId(id);
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
            var client = _clientsRepository.GetClientById(id);
            if (client is null)
                return NotFound();
            else
                _clientsRepository.DeleteClient(id);
                return NoContent();
        }


        [Authorize(Roles = nameof(Role.Manager))]
        [HttpGet]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public ActionResult <List<ClientResponse>> GetClients()
        {
            var clients = _clientsRepository.GetClients();
            return Ok(_mapper.Map <List<ClientResponse>>(clients));


        }

    }
}
