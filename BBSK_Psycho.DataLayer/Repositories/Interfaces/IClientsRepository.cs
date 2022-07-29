using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psycho.DataLayer.Repositories.Interfaces;

public interface IClientsRepository
{
    int AddClient(Client client);
    void DeleteClient(Client client);
    Client? GetClientById(int id);
    public List<Client> GetClients();
    List<Comment> GetCommentsByClientId(int id);
    List<Order> GetOrdersByClientId(int id);
    void UpdateClient(Client client);
    Client? GetClientByEmail(string email);
}