using BBSK_Psycho.BusinessLayer.Services;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.BusinessLayer.Services;

public class ClientsServices : IClientsServices
{
    private readonly IClientsRepository _clientsRepository;

    public ClientsServices(IClientsRepository clientsRepository)
    {
        _clientsRepository = clientsRepository;
    }

    public Client? GetClientById(int id)
    {
        var client = _clientsRepository.GetClientById(id);
        if (client == null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }
        else
            return client;
    }

    public List<Client> GetClients()
    {

        return _clientsRepository.GetClients();
    }

    public List<Comment> GetCommentsByClientId(int id)
    {
        var comments = _clientsRepository.GetCommentsByClientId(id);
        if (comments == null)
        {
            throw new EntityNotFoundException($"Comments by client {id} not found");
        }
        return comments;
    }

    public List<Order> GetOrdersByClientId(int id)
    {
        var orders = _clientsRepository.GetOrdersByClientId(id);
        if (orders == null)
        {
            throw new EntityNotFoundException($"Orders by client {id} not found");
        }
        return orders;
    }

    public int AddClient(Client client)
    {

        return _clientsRepository.AddClient(client);
    }

    public void UpdateClient(Client newClientModel, int id)
    {
        var client = _clientsRepository.GetClientById(id);

        if (client == null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }
        else
        {
            _clientsRepository.UpdateClient(newClientModel, id);
        }
    }

    public void DeleteClient(int id)
    {
        var client = _clientsRepository.GetClientById(id);

        if (client == null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }
        else
        {
            _clientsRepository.DeleteClient(id);
        }

    }
}
