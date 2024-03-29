﻿using AutoMapper;
using BBSK_Psycho.BusinessLayer;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.Extensions;
using BBSK_Psycho.Models;
using BBSK_Psycho.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBSK_Psycho.Controllers;

[ApiController]
[Authorize]
[Produces("application/json")]
[Route("search-requests")]
public class SearchRequestsController : ControllerBase
{
    private readonly IApplicationForPsychologistSearchServices _applicationForPsychologistSearchServices;
    private readonly IMapper _mapper;

    public ClaimModel Claims;

    public SearchRequestsController(IApplicationForPsychologistSearchServices applicationForPsychologistSearchServices, IMapper mapper)
    {
        _applicationForPsychologistSearchServices = applicationForPsychologistSearchServices;
        _mapper = mapper;

    }


    [Authorize(Roles = nameof(Role.Client))]
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<int>> AddApplicationForPsychologist([FromBody] SearchRequest request)
    {
        var claims = this.GetClaims();
        var id = await _applicationForPsychologistSearchServices.AddApplicationForPsychologist(_mapper.Map<ApplicationForPsychologistSearch>(request), claims);

        return Created($"{this.GetRequestPath()}/{id}", id);
    }

    [AuthorizeByRole(Role.Client)]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SearchResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SearchResponse>> GetApplicationForPsychologistById([FromRoute] int id)
    {
        var claims = this.GetClaims();

        var request = await _applicationForPsychologistSearchServices.GetApplicationForPsychologistById(id, claims);
        return Ok(_mapper.Map<SearchResponse>(request));
    }


    [AuthorizeByRole]
    [HttpGet]
    [ProducesResponseType(typeof(SearchResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<List<SearchResponse>>> GetAllApplicationsForPsychologist()
    {
        var request = await _applicationForPsychologistSearchServices.GetAllApplicationsForPsychologist();
        return Ok(_mapper.Map<List<SearchResponse>>(request));

    }

    
    [AuthorizeByRole(Role.Client)]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteApplicationForPsychologist([FromRoute] int id)
    {
        var claimsUser = this.GetClaims();

        await _applicationForPsychologistSearchServices.DeleteApplicationForPsychologist(id, claimsUser);
        return NoContent();
    }


    [AuthorizeByRole(Role.Client)]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> UpdateClientById([FromBody] SearchRequest newModel, [FromRoute] int id)
    {
        var claims = this.GetClaims();

       var request = _mapper.Map<ApplicationForPsychologistSearch>(newModel);

        await _applicationForPsychologistSearchServices.UpdateApplicationForPsychologist(request, id, claims);

        return NoContent();
    }

}
