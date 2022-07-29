using AutoMapper;
using BBSK_Psycho.BusinessLayer;
using BBSK_Psycho.BusinessLayer.Services;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;
using BBSK_Psycho.Extensions;
using BBSK_Psycho.Models;
using BBSK_Psycho.Models.Responses;
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
        private readonly IClientsServices _clientsServices;

        private readonly IMapper _mapper;

        public ClaimModel Claims;

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
            var claims = this.GetClaims();

            var client = _clientsServices.GetClientById(id, claims);
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
            var claims = this.GetClaims();

            var client = new Client()
            {
                Name = request.Name,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
            };


            _clientsServices.UpdateClient(client, id, claims);

            return NoContent();
        }


        [AuthorizeByRole(Role.Client)]
        [HttpGet("{id}/comments")]
        [ProducesResponseType(typeof(CommentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public ActionResult<List<CommentResponse>> GetCommentsByClientId([FromRoute] int id)
        {
            var claims = this.GetClaims();

            var clientComents = _clientsServices.GetCommentsByClientId(id, claims);

            return Ok(_mapper.Map<List<CommentResponse>>(clientComents));
        }


        [AuthorizeByRole(Role.Client)]
        [HttpGet("{id}/orders")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public ActionResult<List<OrderResponse>> GetOrdersByClientId([FromRoute] int id)
        {
            var claims = this.GetClaims();

            var clientOrders = _clientsServices.GetOrdersByClientId(id, claims);

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
            var claimsUser = this.GetClaims();

            _clientsServices.DeleteClient(id, claimsUser);
            return NoContent();
        }


        [Authorize(Roles = nameof(Role.Manager))]
        [HttpGet]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public ActionResult<List<ClientResponse>> GetClients()
        {
            var clients = _clientsServices.GetClients();
            return Ok(_mapper.Map<List<ClientResponse>>(clients));
        }

        [AuthorizeByRole(Role.Client)]
        [HttpGet("{id}/search-requests")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public ActionResult<List<SearchResponse>> GetApplicationsForPsychologistByClientId([FromRoute] int id)
        {
            var claims = this.GetClaims();

            var clientRequests = _clientsServices.GetApplicationsForPsychologistByClientId(id, claims);

            return Ok(_mapper.Map<List<SearchResponse>>(clientRequests));
        }

    }
}