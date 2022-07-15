using AutoMapper;
using BBSK_Psycho.BusinessLayer.Services;
using BBSK_Psycho.BusinessLayer.Tests.ModelControllerSource;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Repositories;
using BBSK_Psycho.Models;
using Moq;
using System.Data;
using System.Security.Claims;

namespace BBSK_Psycho.BusinessLayer.Tests
{
    public class ClientServicesTests
    {
        private ClientsServices _sut;
        private Mock<IClientsRepository> _clientsRepositoryMock;

        private List<Claim> _identities;

        [SetUp]
        public void Setup()
        {

            _clientsRepositoryMock = new Mock<IClientsRepository>();
            _sut = new ClientsServices(_clientsRepositoryMock.Object);
        }



        [Test]
        public void AddClient_ValidRequestPassed_CreatedResultReceived()
        {
            //given
            _clientsRepositoryMock.Setup(c => c.AddClient(It.IsAny<Client>()))
                 .Returns(1);
            var expectedId = 1;

            var client = new Client()
            {
                Name = "Petro",
                LastName = "Petrov",
                Password = "12124564773",
                Email = "p@petrov.com",
                PhoneNumber = "89119118696",
                BirthDate = DateTime.Now,
            };

            //when
            var actual = _sut.AddClient(client);
   

            //then
            
            Assert.True(actual == expectedId);
            _clientsRepositoryMock.Verify(c => c.AddClient(It.IsAny<Client>()), Times.Once);
        }

        [Test]
        public void AddClient_IncorrectedBirthDate_ThrowDataException()
        {
            //given
            _clientsRepositoryMock.Setup(c => c.AddClient(It.IsAny<Client>()))
                 .Returns(1);

            var client = new Client()
            {
                Name = "Petro",
                LastName = "Petrov",
                Password = "12124564773",
                Email = "p@petrov.com",
                PhoneNumber = "89119118696",
                BirthDate =new DateTime( 2023, 05, 05),
            };

            //when

            //then
            Assert.Throws<Exceptions.DataException>(() => _sut.AddClient(client));
        }

        [Test]
        public void AddClient_NotUniqueEmail_ThrowDataException()
        {
            //given
            var clients = new List<Client>
        {
            new Client()
            {
                Name = "John",
                LastName = "Petrov",
                Email = "J@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375",
            },
            new Client()
            {
                Name = "Vasya",
                LastName = "Petrov",
                Email = "Va@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375",
                IsDeleted = true,
            },
            new Client()
            {
                 Name = "Petya",
                 LastName = "Petrov",
                 Email = "P@gmail.com",
                 Password = "12345678dad",
                 PhoneNumber = "89119856375",
            }
        };

            _clientsRepositoryMock.Setup(c => c.GetClients())
                 .Returns(clients);

            var clientNew = new Client()
            {
                Name = "Petro",
                LastName = "Petrov",
                Password = "12124564773",
                Email = "J@gmail.com",
                PhoneNumber = "89119118696",
                BirthDate = new DateTime(2020, 05, 05),
            };

            //when

            //then
            Assert.Throws<Exceptions.UniquenessException>(() => _sut.AddClient(clientNew));
        }


        [TestCaseSource(typeof(ClientsModelIncorrectedPhoneNumber))]
        public void AddClient_IncorrectedPhoneNumber_ThrowDataException(Client client)
        {
            //given
            _clientsRepositoryMock.Setup(c => c.AddClient(It.IsAny<Client>()))
                 .Returns(1);

            //when

            //then
            Assert.Throws<Exceptions.DataException>(() => _sut.AddClient(client));
        }


        [Test]
        public void GetClients_ValidRequestPassed_ClientsReceived()
        {
            //given
            var clients = new List<Client>
        {
            new Client()
            {
                Name = "John",
                LastName = "Petrov",
                Email = "Va@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375",
            },
            new Client()
            {
                Name = "Vasya",
                LastName = "Petrov",
                Email = "Va@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375",
                IsDeleted = true,
            },
            new Client()
            {
                 Name = "Petya",
                 LastName = "Petrov",
                 Email = "Va@gmail.com",
                 Password = "12345678dad",
                 PhoneNumber = "89119856375",
            }
        };

            
            _clientsRepositoryMock.Setup(o => o.GetClients()).Returns(clients).Verifiable();

            //when
            var actual = _sut.GetClients();

            //then
            Assert.NotNull(actual);
            Assert.True(actual.GetType() == typeof(List<Client>));
            Assert.AreEqual(actual[0].Comments, null);
            Assert.AreEqual(actual[1].Orders, null);
            Assert.True(actual[0].IsDeleted == false);
            Assert.True(actual[1].IsDeleted == true);
            _clientsRepositoryMock.Verify(c => c.GetClients(), Times.Once);
        }



        [Test]
        public void GetClients_EmptyRequest_ThrowEntityNotFoundException()
        {
            //given

            List<Client> clients = null;

            _clientsRepositoryMock.Setup(o => o.GetClients()).Returns(clients).Verifiable();

            //when


            //then
            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.GetClients());
        }



        [Test]
        public void GetClientById_ValidRequestPassed_ClientReceived()
        {

            //given
            var clientInDb = new Client()
            {
                Id = 1,
                Name = "Roma",
                LastName = "Petrov",
                Email = "Va@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375",
            };

           

            _identities = new List<Claim> { new Claim(ClaimTypes.Name, clientInDb.Email) };


            _clientsRepositoryMock.Setup(o => o.GetClientById(clientInDb.Id)).Returns(clientInDb);


            //when
            var actual = _sut.GetClientById(clientInDb.Id, _identities);

            //then




            Assert.True(actual.Id == clientInDb.Id);
            Assert.True(actual.Name == clientInDb.Name);
            Assert.True(actual.LastName == clientInDb.LastName);
            Assert.True(actual.Email == clientInDb.Email);
            Assert.True(actual.Password == clientInDb.Password);
            Assert.True(actual.PhoneNumber == clientInDb.PhoneNumber);
            Assert.True(actual.IsDeleted == false);
            _clientsRepositoryMock.Verify(c => c.AddClient(It.IsAny<Client>()), Times.Never);
            _clientsRepositoryMock.Verify(c => c.DeleteClient(It.IsAny<int>()), Times.Never);
            _clientsRepositoryMock.Verify(c => c.GetClientById(It.IsAny<int>()), Times.Once);
            _clientsRepositoryMock.Verify(c => c.GetClients(), Times.Never);
 
        }
    }
}