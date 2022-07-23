﻿using BBSK_Psycho.BusinessLayer.Exceptions;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;

namespace BBSK_Psycho.BusinessLayer.Services;

public class ApplicationForPsychologistSearchServices : IApplicationForPsychologistSearchServices
{

    private readonly IApplicationForPsychologistSearchRepository _applicationForPsychologistSearchRepository;

    public ApplicationForPsychologistSearchServices(IApplicationForPsychologistSearchRepository applicationForPsychologistSearchRepository)
    {
        _applicationForPsychologistSearchRepository = applicationForPsychologistSearchRepository;
       
    }

    public int AddApplicationForPsychologist(ApplicationForPsychologistSearch application, ClaimModel claim)
    {
        if(claim is null)
        {
            throw new AccessException($"Access denied");
        }
        

        return _applicationForPsychologistSearchRepository.AddApplicationForPsychologist(application, (int)claim.Id);
    }

    public List<ApplicationForPsychologistSearch> GetAllApplicationsForPsychologist() =>
        _applicationForPsychologistSearchRepository.GetAllApplicationsForPsychologist();




    public ApplicationForPsychologistSearch? GetApplicationForPsychologistById(int id, ClaimModel claim)
    {
        var application = _applicationForPsychologistSearchRepository.GetApplicationForPsychologistById(id);

        if (application == null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        if (!(((claim.Email == application.Client.Email
           || claim.Role == Role.Manager.ToString())
           && claim.Role != Role.Psychologist.ToString()) && claim is not null))
        {
            throw new AccessException($"Access denied");
        }
        else
            return application;
    }


   
    public void DeleteApplicationForPsychologist(int id, ClaimModel claim)
    {
        var application = _applicationForPsychologistSearchRepository.GetApplicationForPsychologistById(id);

        if (application == null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        if (!(((claim.Email == application.Client.Email
           || claim.Role == Role.Manager.ToString())
           && claim.Role != Role.Psychologist.ToString()) && claim is not null))
        {
            throw new AccessException($"Access denied");
        }
        else
            _applicationForPsychologistSearchRepository.DeleteApplicationForPsychologist(id);
    }


    public void UpdateApplicationForPsychologist(ApplicationForPsychologistSearch newModel, int id, ClaimModel claim)
    {
        var application = _applicationForPsychologistSearchRepository.GetApplicationForPsychologistById(id);

        if (application == null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        if (!(((claim.Email == application.Client.Email
           || claim.Role == Role.Manager.ToString())
           && claim.Role != Role.Psychologist.ToString()) && claim is not null))
        {
            throw new AccessException($"Access denied");
        }
        else
        {
            application.Name = newModel.Name;
            application.PhoneNumber = newModel.PhoneNumber;
            application.Description = newModel.Description;
            application.PsychologistGender = newModel.PsychologistGender;
            application.CostMin = newModel.CostMin;
            application.CostMax = newModel.CostMax;
            application.Date = newModel.Date;
            application.Time = newModel.Time;
            _applicationForPsychologistSearchRepository.UpdateApplicationForPsychologist(newModel, id);
        }

    }

            
}
