using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psycho.BusinessLayer.Services.Interfaces;

public interface IApplicationForPsychologistSearchServices
{
    int AddApplicationForPsychologist(ApplicationForPsychologistSearch application, ClaimModel claim);
    void DeleteApplicationForPsychologist(int id, ClaimModel claim);
    List<ApplicationForPsychologistSearch> GetAllApplicationsForPsychologist();
    ApplicationForPsychologistSearch? GetApplicationForPsychologistById(int id, ClaimModel claim);
    void UpdateApplicationForPsychologist(ApplicationForPsychologistSearch newModel, int id, ClaimModel claim);
}