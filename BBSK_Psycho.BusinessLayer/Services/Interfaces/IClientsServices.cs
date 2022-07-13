using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psycho.BusinessLayer.Services;

public interface IClientsServices
{
    int AddClient(Client client);
    void DeleteClient(int id);
    Client? GetClientById(int id);
    List<Client> GetClients();
    List<Comment> GetCommentsByClientId(int id);
    List<Order> GetOrdersByClientId(int id);
    void UpdateClient(Client newClientModel, int id);
}