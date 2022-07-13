using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psycho.DataLayer.Repositories
{
    public interface IClientsRepository
    {
        int AddClient(Client client);
        void DeleteClient(int id);
        Client? GetClientById(int id);
        public List<Client> GetClients();
        List<Comment> GetCommentsByClientId(int id);
        List<Order> GetOrdersByClientId(int id);
        void UpdateClient(Client client, int id);
    }
}