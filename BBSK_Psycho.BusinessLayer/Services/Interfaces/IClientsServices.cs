using BBSK_Psycho.DataLayer.Entities;
using System.Security.Claims;

namespace BBSK_Psycho.BusinessLayer.Services;

public interface IClientsServices
{
    int AddClient(Client client);
    void DeleteClient(int id, List<Claim>? identities);
    Client? GetClientById(int id, List<Claim>? identities);
    List<Client> GetClients();
    List<Comment> GetCommentsByClientId(int id, List<Claim>? identities);
    List<Order> GetOrdersByClientId(int id, List<Claim>? identities);
    void UpdateClient(Client newClientModel, int id, List<Claim>? identities);
}