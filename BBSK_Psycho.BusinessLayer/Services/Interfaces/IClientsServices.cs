
using BBSK_Psycho.DataLayer.Entities;
using System.Security.Claims;

namespace BBSK_Psycho.BusinessLayer.Services.Interfaces;

public interface IClientsServices
{
    int AddClient(Client client);
    void DeleteClient(int id, ClaimModel claim);
    Client? GetClientById(int id, ClaimModel claim);
    List<Client> GetClients();
    List<Comment> GetCommentsByClientId(int id, ClaimModel claim);
    List<Order> GetOrdersByClientId(int id, ClaimModel claim);
    void UpdateClient(Client newClientModel, int id, ClaimModel claim);
    List<ApplicationForPsychologistSearch> GetApplicationsForPsychologistByClientId(int id, ClaimModel claim);

}