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

namespace BBSK_Psycho.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PsychologistsController : ControllerBase
    {
        //private readonly ILogger<PsychologistsController> _logger;


        //public PsychologistsController(ILogger<PsychologistsController> logger)
        //{
        //    _logger = logger;
        //}

        [AuthorizeByRole]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PsychologistResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public ActionResult<PsychologistResponse> GetPsychologist(int id)
        {
            return Ok(new PsychologistResponse());
        }

        [AuthorizeByRole(Role.Client)]
        [HttpGet()]
        [ProducesResponseType(typeof(GetAllPsychologistsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public ActionResult<List<GetAllPsychologistsResponse>> GetAllPsychologists()
        {
            return Ok(new List<GetAllPsychologistsResponse>());
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

        [AuthorizeByRole(Role.Psychologist)]
        [HttpPost()]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<int> AddPsychologist([FromBody] AddPsychologistRequest psychologistRequest)
        {
            var id = 42;
            return Created($"{this.GetRequestPath()}/{id}", id);
        }

        [AuthorizeByRole(Role.Psychologist)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public ActionResult UpdatePsychologist([FromBody] UpdatePsychologistRequest psychologistRequest, int id)
        {
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
            return NoContent();
        }


        [HttpGet("{psychologistId}/comments")]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public List<GetCommentsByPsychologistIdResponse> GetCommentsByPsychologistId(int psychologistId)
        {
            return new List<GetCommentsByPsychologistIdResponse>();
        }

        [AuthorizeByRole(Role.Client)]
        [HttpPost("request-psyhologist-search")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
        public ActionResult <int> AddRequestPsyhologistSearch([FromBody] RequestPsyhologistSearch requestPsyhologistSearch)
        {
            int id = 2;
            return Created($"{this.GetRequestPath()}/{id}", id);
        }

        [Authorize(Roles = nameof(Role.Client))]
        [HttpPost("{psychologistId}/comments")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
        public ActionResult <int> AddCommentToPsyhologist([FromBody] CommentRequest comment, int psychologistId)
        {
            int id = 2;
            return Created($"{this.GetRequestPath()}/{id}", id);
        }
    }
}
