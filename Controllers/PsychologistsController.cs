﻿using BBSK_Psycho.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BBSK_Psycho.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PsychologistsController : ControllerBase
    {
        private readonly ILogger<PsychologistsController> _logger;


        public PsychologistsController(ILogger<PsychologistsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public GetPsychologistResponse GetPsychologist(int id)
        {
            return new GetPsychologistResponse();
        }

        [HttpGet()]
        public  List<GetAllPsychologistsResponse> GetAllPsychologists()
        {
            var psychologists = new List<GetAllPsychologistsResponse>() { new GetAllPsychologistsResponse() { Name = "123" }, new () { Name = "12aaaa3" }, new Psychologist() { Name = "68596" } };
            return psychologists;
        }
        //[HttpGet("{psid}")]
        //public List <Comment> GetCommentsByPsychologist(int id)
        //{
        //    return null;
        //}

        [HttpPost()]
        public Psychologist AddPsychologist([FromBody] Psychologist psychologist)
        {
            psychologist.Id = 123;
            return psychologist;
        }

        [HttpPut("{id}")]
        public Psychologist UpdatePsychologist([FromBody] Psychologist psychologist, int id)
        {
            var psychologistOld = new Psychologist();

            psychologistOld.Name = psychologist.Name;
            psychologistOld.Status = psychologist.Status;

            return psychologistOld;
        }

        [HttpDelete("{id}")]
        public void DeletePsychologist(int id)
        {

        }

        [HttpGet("comments/{psychologistId}")]
        public List <Comment> GetCommentsByPsychologistId(int psychologistId)
        {
            var comments = new List<Comment>() {
                new Comment()
                {
                    PsychologistId = 1,Rating=100 , Text = "AALlaa"
                },
                new Comment()
                {
                    PsychologistId = 1,Rating=100 , Text = "AALlaa222"
                },
                new Comment()
                {
                    PsychologistId = 2,Rating=100 , Text = "AALlaa3333"
                },
                new Comment()
                {
                    PsychologistId = 2,Rating=100 , Text = "AALlaa4444"
                }
            };
            var result = comments.Where(c => c.PsychologistId == psychologistId).ToList();

            return result;
        }
    }
}
