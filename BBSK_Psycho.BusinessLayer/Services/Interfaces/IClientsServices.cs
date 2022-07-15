using BBSK_Psycho.DataLayer.Entities;
using System.Security.Claims;

namespace BBSK_Psycho.BusinessLayer.Services;

public interface IClientsServices
{
    int AddClient(Client client);
    void DeleteClient(int id, List<ClaimsIdentity>? identities);
    Client? GetClientById(int id, List<ClaimsIdentity>? identities);
    List<Client> GetClients();
    List<Comment> GetCommentsByClientId(int id, List<ClaimsIdentity>? identities);
    List<Order> GetOrdersByClientId(int id, List<ClaimsIdentity>? identities);
    void UpdateClient(Client newClientModel, int id, List<ClaimsIdentity>? identities);
}