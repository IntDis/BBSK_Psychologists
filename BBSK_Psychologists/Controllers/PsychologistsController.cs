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

namespace BBSK_Psycho.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PsychologistsController : ControllerBase
    {

        private readonly IPsychologistsRepository _psychologistsRepository;
        public PsychologistsController(IPsychologistsRepository psychologistsRepository)
        {
            _psychologistsRepository = psychologistsRepository;
        }

        [AuthorizeByRole(Role.Manager)]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PsychologistResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<PsychologistResponse> GetPsychologist(int id)
        {
            var result = _psychologistsRepository.GetPsychologist(id);
            if (result == null)
                return NotFound();
            else
            return Ok(result);
        }

        [AuthorizeByRole(Role.Client, Role.Psychologist)]
        [HttpGet()]
        public ActionResult<List<GetAllPsychologistsResponse>> GetAllPsychologists()
        {
            var result = _psychologistsRepository.GetAllPsychologists();
            
            return Ok(result);
        }

        [AuthorizeByRole(Role.Psychologist)]
        [HttpPost()]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<int> AddPsychologist([FromBody] AddPsychologistRequest psychologistRequest)
        {
            var psychologist = new Psychologist
            {
                Name = psychologistRequest.Name,
                LastName= psychologistRequest.LastName,
                Patronymic= psychologistRequest.Patronymic,
                BirthDate= (DateTime)psychologistRequest.BirthDate,
                Gender= (Gender)psychologistRequest.gender,
                Phone= psychologistRequest.Phone,
                Email= psychologistRequest.Email,
                Password= psychologistRequest.Password,
                PasportData= psychologistRequest.PasportData,
                CheckStatus= psychologistRequest.checkStatus,
                Price= psychologistRequest.Price
            };
            var id = 42;
            var result = _psychologistsRepository.AddPsychologist(psychologist);
            return Created("", result);
            //return Created($"{this.GetRequestPath()}/{id}", id); 
            //return psychologistRequest.Id;
        }

        [AuthorizeByRole(Role.Psychologist)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult UpdatePsychologist([FromBody] UpdatePsychologistRequest psychologistRequest, int id)
        {
            var psychologist = new Psychologist
            {
                Name = psychologistRequest.Name,
                BirthDate = psychologistRequest.BirthDate
            };
            _psychologistsRepository.UpdatePsychologist(psychologist);
            return NoContent();
        }

        [AuthorizeByRole(Role.Psychologist)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult DeletePsychologist(int id)
        {
            _psychologistsRepository.DeletePsychologist(id);
            return NoContent();
        }

        [AuthorizeByRole(Role.Client, Role.Psychologist)]
        [HttpGet("{psychologistId}/comments")]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult <List<GetCommentsByPsychologistIdResponse>> GetCommentsByPsychologistId(int psychologistId)
        {
            var result=_psychologistsRepository.GetCommentsByPsychologistId(psychologistId);
                return Ok(result);
        }

        [AuthorizeByRole(Role.Client)]
        [HttpPost("request-psyhologist-search")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public ActionResult <int> AddRequestPsyhologistSearch([FromBody] Models.ApplicationForPsychologistSearch requestPsyhologistSearch)
        {
            int id = 2;
            return Created($"{this.GetRequestPath()}/{id}", id);
        }

        [Authorize(Roles = nameof(Role.Client))]
        [HttpPost("{psychologistId}/comments")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<int> AddCommentToPsyhologist([FromBody] CommentRequest commentRequest, int psychologistId)
        {
            var comment = new Comment
            {
                Text = commentRequest.Text,
                Rating = commentRequest.Rating,
                Date = commentRequest.Date,
                Client = new Client() { Id = commentRequest.ClientId }
            };

            var result = _psychologistsRepository.AddCommentToPsyhologist(comment, psychologistId);
            return Created("", result);
        }

    }
}
