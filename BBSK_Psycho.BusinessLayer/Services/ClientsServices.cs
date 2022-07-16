using BBSK_Psycho.BusinessLayer.Exceptions;
using BBSK_Psycho.BusinessLayer.Services;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;
using System.Security.Claims;


namespace BBSK_Psycho.BusinessLayer.Services;

public class ClientsServices : IClientsServices
{
    private readonly IClientsRepository _clientsRepository;

    public ClientsServices(IClientsRepository clientsRepository)
    {
        _clientsRepository = clientsRepository;
    }

    public Client? GetClientById(int id, List<Claim>? identities)
    {
       
        var client = _clientsRepository.GetClientById(id);
     
        if (client == null || client.Id == 0)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }
        if ((identities[0].Value != (string)client.Email && identities[1].Value != $"{Role.Manager}") || identities is null)
        {

            throw new AccessException($"Access denied");
        }
        else
            return client;
    }


    public List<Client> GetClients()//??????????
    {
        var clients = _clientsRepository.GetClients();
      
        return clients;
    }


    public List<Comment> GetCommentsByClientId(int id, List<Claim> identities)
    {
        var client = _clientsRepository.GetClientById(id);
        var comments = _clientsRepository.GetCommentsByClientId(id);
        
        if (client == null || client.Id == 0)
        {
            throw new EntityNotFoundException($"Client { id } not found");
        }
       
        if ((identities[0].Value != (string)client.Email && identities[1].Value != $"{Role.Manager}") || identities is null)
        {

            throw new AccessException($"Access denied");

        }
        return comments;
    }


    public List<Order> GetOrdersByClientId(int id, List<Claim> identities)
    {
        var client = _clientsRepository.GetClientById(id);
        var orders = _clientsRepository.GetOrdersByClientId(id);


        if (client == null || client.Id == 0)
        {
            throw new EntityNotFoundException($"Orders by client {id} not found");
        }
        if ((identities[0].Value != (string)client.Email && identities[1].Value != $"{Role.Manager}") || identities is null)
        {

            throw new AccessException($"Access denied");

        }
        else
            return orders;
    }

    public int AddClient(Client client)
    {

        var isChecked = CheckingEmailForUniqueness(client.Email);


        if (isChecked)
        {
            throw new UniquenessException($"That email is registred");
        }
        if(client.PhoneNumber is not null)
        {
            if (!(client.PhoneNumber.StartsWith("+7") || client.PhoneNumber.StartsWith("8") && client.PhoneNumber.Length <= 11))
            {
                throw new DataException($"Invalid phone number");
            }
        }
        if (client.BirthDate>DateTime.Now)
        {
            throw new DataException($"Invalid birthday");
        }
        else
             return _clientsRepository.AddClient(client);

    }

    public void UpdateClient(Client newClientModel, int id, List<Claim> identities)
    {
        var client = _clientsRepository.GetClientById(id);


        if (client == null || client.Id ==0)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }
        if ((identities[0].Value != (string)client.Email && identities[1].Value != $"{Role.Manager}") || identities is null)
        {

            throw new AccessException($"Access denied");

        }
        else
            _clientsRepository.UpdateClient(newClientModel, id);
        
    }

    public void DeleteClient(int id, List<Claim> identities)
    {
        var client = _clientsRepository.GetClientById(id);


        if (client == null || client.Id == 0)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        if ((identities[0].Value != (string)client.Email && identities[1].Value != $"{Role.Manager}" ) || identities is null)
        {

            throw new AccessException($"Access denied");
            
        }
        else
            _clientsRepository.DeleteClient(id);
    }


    private bool CheckingEmailForUniqueness(string email)
    {
        var clients = _clientsRepository.GetClients();

        if(clients is not null)
        {
            var uniqueEmail = clients.Any(c => c.Email == email);
            return uniqueEmail;
        }
            else return false;

    }

}
