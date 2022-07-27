using System.Collections.Generic;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.Extensions;
using BBSK_Psycho.Models;
using BBSK_Psycho.Models.Requests;
using BBSK_Psycho.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BBSK_Psycho.DataLayer.Repositories;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.BusinessLayer.Exceptions;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using AutoMapper;
using BBSK_Psycho.BusinessLayer;

namespace BBSK_Psycho.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PsychologistsController : ControllerBase
    {

        private readonly IPsychologistsRepository _psychologistsRepository;
        private readonly IPsychologistServices _psychologistServices;
        private readonly IMapper _mapper;
        public ClaimModel Claims;
        public PsychologistsController(IPsychologistServices psychologistServices, IMapper mapper)
        {
            _psychologistServices = psychologistServices;
            _mapper = mapper;
        }

        [AuthorizeByRole]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PsychologistResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult<PsychologistResponse> GetPsychologist(int id)
        {
            var claims = this.GetClaims();

            var result = _psychologistServices.GetPsychologist(id, claims);
            if (result == null)
                return NotFound();
            else
                return Ok(_mapper.Map<PsychologistResponse>(result));
        }

        [AuthorizeByRole(Role.Client)]
        [HttpGet()]
        [ProducesResponseType(typeof(GetAllPsychologistsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public ActionResult<List<GetAllPsychologistsResponse>> GetAllPsychologists()
        {
            var claims = this.GetClaims();
            var result = _psychologistServices.GetAllPsychologists(claims);

            return Ok(_mapper.Map<List<GetAllPsychologistsResponse>>(result));
        }

        [HttpGet("avg-price")]
        [Authorize(Roles = nameof(Role.Psychologist))]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public ActionResult<decimal> GetAveragePsychologistPrice()
        {
            return 0.20m;
        }

        [AllowAnonymous]
        [HttpPost()]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<int> AddPsychologist([FromBody] AddPsychologistRequest psychologistRequest)
        {
            var tmp = _mapper.Map<Psychologist>(psychologistRequest);
            var result = _psychologistServices.AddPsychologist(_mapper.Map<Psychologist>(psychologistRequest));
            return Created("", result);
        }

        [AuthorizeByRole(Role.Psychologist)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public ActionResult UpdatePsychologist([FromBody] UpdatePsychologistRequest psychologistRequest, int id)
        {
            var claims = this.GetClaims();
            _psychologistServices.UpdatePsychologist(_mapper.Map<Psychologist>(psychologistRequest), id, claims);
            return NoContent();
        }

        [AuthorizeByRole(Role.Psychologist)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public ActionResult DeletePsychologist(int id)
        {
            var claims = this.GetClaims();
            _psychologistServices.DeletePsychologist(id, claims);
            return NoContent();
        }

        [AuthorizeByRole(Role.Psychologist, Role.Client)]
        [HttpGet("{psychologistId}/comments")]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public ActionResult<List<GetCommentsByPsychologistIdResponse>> GetCommentsByPsychologistId(int psychologistId)
        {
            var claims = this.GetClaims();
            var result = _psychologistServices.GetCommentsByPsychologistId(psychologistId, claims);
            return Ok(_mapper.Map<List<GetCommentsByPsychologistIdResponse>>(result));
        }

        [AuthorizeByRole(Role.Psychologist, Role.Client)]
        [HttpGet("{psychologistId}/orders")]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public ActionResult <List<OrderResponse>> GetOrdersByPsychologistId(int id)
        {
            var claims = this.GetClaims();
            var result = _psychologistServices.GetOrdersByPsychologistId(id, claims);
            return Ok(_mapper.Map<List<OrderResponse>>(result));
        }

        // Этот метод будет перенесен в клиента!!!!
        [AuthorizeByRole(Role.Client)]
        [HttpPost("request-psyhologist-search")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<int> AddRequestPsyhologistSearch([FromBody] Models.ApplicationForPsychologistSearch requestPsyhologistSearch)
        {
            int id = 2;
            return Created($"{this.GetRequestPath()}/{id}", id);
        }

        [AuthorizeByRole(Role.Client)]
        [HttpPost("{psychologistId}/comments")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<int> AddCommentToPsyhologist([FromBody] CommentRequest commentRequest, int psychologistId)
        {
            var claims = this.GetClaims();
            var result = _psychologistServices.AddCommentToPsyhologist(_mapper.Map<Comment>(commentRequest), psychologistId, claims);
            return Created("", result);
        }
    }
}
