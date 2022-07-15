using AutoMapper;
using BBSK_Psycho.BusinessLayer.Services;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Repositories;
using BBSK_Psycho.Models;
using Moq;


namespace BBSK_Psycho.BusinessLayer.Tests
{
    public class ClientServicesTests
    {
        private ClientsServices _sut;
        private Mock<IClientsRepository> _clientsRepositoryMock;

        [SetUp]
        public void Setup()
        {

            _clientsRepositoryMock = new Mock<IClientsRepository>();
            _sut = new ClientsServices(_clientsRepositoryMock.Object);
        }

        [Test]
        public void AddClient_ValidRequestPassed_CreatedResultReceived()
        {
           
        }

          
    }
}