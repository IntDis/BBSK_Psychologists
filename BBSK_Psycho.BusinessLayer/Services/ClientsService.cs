using BBSK_Psycho.BusinessLayer.Exceptions;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;


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
     
        if (client == null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }
        if (!(((claims.Email == (string)client.Email
            || claims.Role == Role.Manager.ToString()) 
            && claims.Role != Role.Psychologist.ToString()) && claims is not null))
        {
            throw new AccessException($"Access denied");
        }
        else
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

        
        if (client == null )
        {
            throw new EntityNotFoundException($"Client { id } not found");
        }

        if (!(((claims.Email == (string)client.Email
            || claims.Role == Role.Manager.ToString())
            && claims.Role != Role.Psychologist.ToString()) && claims is not null))
        {
            throw new AccessException($"Access denied");
        }

        else
            return _clientsRepository.GetCommentsByClientId(id);
    }


    public List<Order> GetOrdersByClientId(int id, ClaimModel claims)
    {
        var client = _clientsRepository.GetClientById(id);

        if (client == null)
        {
            throw new EntityNotFoundException($"Orders by client {id} not found");
        }
        if (!(((claims.Email == (string)client.Email
            || claims.Role == Role.Manager.ToString())
            && claims.Role != Role.Psychologist.ToString()) && claims is not null))
        {
            throw new AccessException($"Access denied");
        }

        else
            return _clientsRepository.GetOrdersByClientId(id);
    }

    public int AddClient(Client client)
    {

        var isChecked = CheckEmailForUniqueness(client.Email);


        if (!isChecked)
        {
            throw new UniquenessException($"That email is registred");
        }
        
        else
             return _clientsRepository.AddClient(client);

    }

    public void UpdateClient(Client newClientModel, int id, ClaimModel claims)
    {
        var client = _clientsRepository.GetClientById(id);


        if (client == null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }
        if (!(((claims.Email == (string)client.Email
            || claims.Role == Role.Manager.ToString())
            && claims.Role != Role.Psychologist.ToString()) && claims is not null))
        {
            throw new AccessException($"Access denied");
        }
        else
            _clientsRepository.UpdateClient(newClientModel, id);
        
    }

    public void DeleteClient(int id, ClaimModel claims)
    {
        var client = _clientsRepository.GetClientById(id);


        if (client == null)
        {
            throw new EntityNotFoundException($"Client {id} not found");
        }

        if (!(((claims.Email == (string)client.Email
             || claims.Role == Role.Manager.ToString())
             && claims.Role != Role.Psychologist.ToString()) && claims is not null))
        {
            throw new AccessException($"Access denied");
        }
        else
            _clientsRepository.DeleteClient(id);

    }


    private bool CheckEmailForUniqueness(string email) => _clientsRepository.GetClientByEmail(email) == null;


}
