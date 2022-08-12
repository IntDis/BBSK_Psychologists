﻿using BBSK_Psycho.BusinessLayer.Exceptions;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;

namespace BBSK_Psycho.BusinessLayer
{
    public class PsychologistService : IPsychologistService
    {
        private readonly IPsychologistsRepository _psychologistsRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IClientsRepository _clientsRepository;

        public PsychologistService(IPsychologistsRepository psychologistsRepository, IClientsRepository clientsRepository, IOrdersRepository ordersRepository)
        {
            _psychologistsRepository = psychologistsRepository;
            _ordersRepository = ordersRepository;
            _clientsRepository = clientsRepository;
        }

        public int AddCommentToPsyhologist(Comment comment, int psychologistId, ClaimModel claim)
        {
            var commonOrder = _ordersRepository.GetOrderByPsychIdAndClientId(psychologistId, comment.Client.Id);
            if (commonOrder == null)
            {
                throw new AccessException("$It is impossible to leave a comment to a psychologist with whom there have been no sessions!");
            }
            var client = _clientsRepository.GetClientById(comment.Client.Id);
            if (client == null)
            {
                throw new EntityNotFoundException($"Client not found");
            }
            if (claim.Email != client.Result.Email)
            {
                throw new AccessException($"Access denied");
            }

            var result = _psychologistsRepository.AddCommentToPsyhologist(comment, psychologistId);
            return result.Id;
        }

        public int AddPsychologist(Psychologist psychologist)
        {
            var isUnique = CheckEmailForUniqueness(psychologist.Email);
            if(!isUnique)
            {
                throw new UniquenessException($"That email is registred");
            }
            var result = _psychologistsRepository.AddPsychologist(psychologist);
            return result;
        }

        public void DeletePsychologist(int id, ClaimModel claim)
        {
            var psychologist = _psychologistsRepository.GetPsychologist(id);
            if (psychologist == null)

            {
                throw new EntityNotFoundException($"Psychologist {id} not found");
            }

            if (claim.Role == Role.Psychologist
                && claim.Id != id)
            {
                throw new AccessException($"Access denied");
            }
            _psychologistsRepository.DeletePsychologist(id);
        }

        public List<Psychologist> GetAllPsychologists(ClaimModel claim) => _psychologistsRepository.GetAllPsychologists().ToList();


        public List<Comment> GetCommentsByPsychologistId(int id, ClaimModel claim)
        {
            var psychologist = _psychologistsRepository.GetPsychologist(id);
            if (psychologist == null)
            {
                throw new EntityNotFoundException($"Psychologist {id} not found");
            }
            CheckAccessForPsychologistManagersAndClients(id, claim);

            var result= _psychologistsRepository.GetCommentsByPsychologistId(id);
            return result;
        }

        public List<Order> GetOrdersByPsychologistId(int id, ClaimModel claim)
        {

            var psycho = _psychologistsRepository.GetPsychologist(id);
            if (psycho == null)
            {
                throw new EntityNotFoundException($"Orders by psychologist {id} not found");
            }
            CheckAccessForPsychologistManagersAndClients(id, claim);

            return _psychologistsRepository.GetOrdersByPsychologistsId(id);
        }

        public Psychologist? GetPsychologist(int id, ClaimModel claim)
        {
            var result = _psychologistsRepository.GetPsychologist(id);
            CheckAccessOnlyForPsychologistAndManagers(id, claim);
            return result;
        }

        public void UpdatePsychologist(Psychologist psychologist, int id, ClaimModel claim)
        {
            var result = _psychologistsRepository.GetPsychologist(id);
            CheckAccessOnlyForPsychologistAndManagers(id, claim);
            _psychologistsRepository.UpdatePsychologist(psychologist, id);
        }

        public void CheckAccessOnlyForPsychologistAndManagers (int id, ClaimModel claim)
        {
            if (claim.Id != id
                && claim.Role != Role.Manager)
            {
                throw new AccessException($"Access denied");
            }
        }

        public void CheckAccessForPsychologistManagersAndClients(int id, ClaimModel claim)
        {
            if (claim.Role == Role.Psychologist
                && claim.Id != id)
            {
                throw new AccessException($"Access denied");
            }
        }
        private bool CheckEmailForUniqueness(string email) => _psychologistsRepository.GetPsychologistByEmail(email) == null;
    }
}