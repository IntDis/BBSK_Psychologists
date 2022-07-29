using Microsoft.EntityFrameworkCore;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;

namespace BBSK_Psycho.DataLayer.Repositories;

public class ClientsRepository : IClientsRepository
{
    private readonly BBSK_PsychoContext _context;

    public ClientsRepository(BBSK_PsychoContext context)
    {
        _context = context;
    }

    public Client? GetClientById(int id) => _context.Clients
        .Include(c => c.ApplicationForPsychologistSearch.Where(a=>a.IsDeleted == false))
        .FirstOrDefault(c => c.Id == id);


    public List<Client> GetClients() => _context.Clients
        .Where(c => c.IsDeleted== false)
        .AsNoTracking()
        .ToList();


    public List<Comment> GetCommentsByClientId(int id) => _context.Comments.Where(c => c.IsDeleted == false && c.Client.Id == id).ToList();

    public List<Order> GetOrdersByClientId(int id) => _context.Orders.Where(c => c.IsDeleted == false && c.Client.Id == id).ToList();

    public Client? GetClientByEmail(string email) => _context.Clients.FirstOrDefault(c => c.Email == email);

    public int AddClient(Client client)
    {
        _context.Clients.Add(client);
        _context.SaveChanges();

        return client.Id;
    }

    public void UpdateClient(Client newModel)
    {
        _context.Clients.Update(newModel);
        _context.SaveChanges();
    }

    public void DeleteClient(Client client)
    {
        client.IsDeleted = true;
        _context.SaveChanges();
    }

}
