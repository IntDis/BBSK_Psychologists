using BBSK_Psycho.BusinessLayer.Exceptions;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;

namespace BBSK_Psycho.BusinessLayer.Services;

public class ClientsService : IClientsServices
{
    private readonly IClientsRepository _clientsRepository;

    public ClientsService(IClientsRepository clientsRepository)
    {
        _clientsRepository = clientsRepository;
    }

    public Client? GetClientById(int id, ClaimModel claims)
    {
        var client = _clientsRepository.GetClientById(id);

        if (client is null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        CheckAccess(claims, client);

        return client;
    }


    public List<Client> GetClients()
    {
        var clients = _clientsRepository.GetClients();

        return clients;
    }


    public List<Comment> GetCommentsByClientId(int id, ClaimModel claims)
    {
        var client = _clientsRepository.GetClientById(id);

        if (client is null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        CheckAccess(claims, client);

        return _clientsRepository.GetCommentsByClientId(id);
    }


    public List<Order> GetOrdersByClientId(int id, ClaimModel claims)
    {
        var client = _clientsRepository.GetClientById(id);

        if (client is null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        CheckAccess(claims, client);

        return _clientsRepository.GetOrdersByClientId(id);
    }

    public int AddClient(Client client)
    {

        CheckEmailForUniqueness(client.Email);

        client.Password = PasswordHash.HashPassword(client.Password);
        return _clientsRepository.AddClient(client);
    }

    public void UpdateClient(Client newClientModel, int id, ClaimModel claims)
    {
        var client = _clientsRepository.GetClientById(id);

        if (client is null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        CheckAccess(claims, client);

        client.Name = newClientModel.Name;
        client.LastName = newClientModel.LastName;
        client.BirthDate = newClientModel.BirthDate;

        _clientsRepository.UpdateClient(newClientModel);
    }

    public void DeleteClient(int id, ClaimModel claims)
    {
        var client = _clientsRepository.GetClientById(id);

        if (client is null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        CheckAccess(claims, client);

        _clientsRepository.DeleteClient(client);
    }

    public List<ApplicationForPsychologistSearch> GetApplicationsForPsychologistByClientId(int id, ClaimModel claims)
    {
        var client = _clientsRepository.GetClientById(id);

        if (client is null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        CheckAccess(claims, client);

        return client.ApplicationForPsychologistSearch.ToList();
    }




    private void CheckAccess(ClaimModel claims, Client client)
    {
        if ((!(((claims.Email == client.Email
         || claims.Role == Role.Manager)
         && claims.Role != Role.Psychologist) &&
         claims is not null)))
            throw new AccessException($"Access denied");
    }


    private void CheckEmailForUniqueness(string email) 
    {
        if( _clientsRepository.GetClientByEmail(email) != null)
        {
            throw new UniquenessException($"That email is registred");
        }
    }
}