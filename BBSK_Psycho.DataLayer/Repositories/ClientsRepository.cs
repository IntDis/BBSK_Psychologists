﻿using Microsoft.EntityFrameworkCore;
using BBSK_Psycho.DataLayer.Entities;

namespace BBSK_Psycho.DataLayer.Repositories;

public class ClientsRepository : IClientsRepository
{
    private readonly BBSK_PsychoContext _context;

    public ClientsRepository(BBSK_PsychoContext context)
    {
        _context = context;
    }

    public Client? GetClientById(int id) => _context.Clients.FirstOrDefault(c => c.Id == id);

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

    public void UpdateClient(Client newModel, int id)
    {
        var client=GetClientById(id);

        client.Name = newModel.Name;
        client.LastName = newModel.LastName;
        client.BirthDate = newModel.BirthDate;

        _context.Clients.Update(client);
        _context.SaveChanges();
    }

    public void DeleteClient(int id)
    {
        var client=GetClientById(id);
        client.IsDeleted = true;
        _context.SaveChanges();
    }

}
