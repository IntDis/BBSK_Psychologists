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
        private ClientsService _sut;
        private Mock<IClientsRepository> _clientsRepositoryMock;

        private List<Claim> _identities;

        [SetUp]
        public void Setup()
        {

            _clientsRepositoryMock = new Mock<IClientsRepository>();
            _sut = new ClientsService(_clientsRepositoryMock.Object);
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


        [Test]
        public void GetClientById_EmptyRequest_ClientReceived()
        {

            //given
            var testId = 2;

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


            //then

            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.GetClientById(testId, _identities));

        }


        [Test]
        public void GetClientById_ClientGetSomeoneElsesProfile_ThrowAccessException()
        {

            //given
            var testEmail = "bnb@gamil.ru";

            var clientInDb = new Client()
            {
                Id = 1,
                Name = "Roma",
                LastName = "Petrov",
                Email = "Va@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375",
                
            };


            _identities = new List<Claim> { new Claim(ClaimTypes.Name, testEmail), new Claim(ClaimTypes.Name, "Client") };


            _clientsRepositoryMock.Setup(o => o.GetClientById(clientInDb.Id)).Returns(clientInDb);


            //when


            //then

            Assert.Throws<Exceptions.AccessException>(() => _sut.GetClientById(clientInDb.Id, _identities));

        }


        [Test]
        public void GetCommentsByClientId_ValidRequestPassed_CommentsReceived()
        {
            //given
            var clientInDb = new Client()
            {

                Id = 1,
                Name = "Vasya",
                LastName = "Petrov",
                Email = "Va@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375",
                Comments = new()
                {
                    new()
                    {
                             Id = 1, Text="ApAp",Rating=1,Date=DateTime.Now
                    },

                    new()
                    {
                        Id = 2, Text="222",Rating=3,Date=DateTime.Now
                    },
                }
                 
            };
            _identities = new List<Claim> { new Claim(ClaimTypes.Name, clientInDb.Email), new Claim(ClaimTypes.Name, "Client") };

            _clientsRepositoryMock.Setup(o => o.GetClientById(clientInDb.Id)).Returns(clientInDb);
            _clientsRepositoryMock.Setup(o => o.GetCommentsByClientId(clientInDb.Id)).Returns(clientInDb.Comments);

            //when
            var actual = _sut.GetCommentsByClientId(clientInDb.Id,_identities);

            //then

            Assert.True(clientInDb.Comments.Count == actual.Count);
            Assert.True(actual[0].Id == clientInDb.Comments[0].Id);
            Assert.True(actual[1].Id == clientInDb.Comments[1].Id);
            Assert.True(actual[0].Rating == clientInDb.Comments[0].Rating);
            Assert.True(actual[1].Rating == clientInDb.Comments[1].Rating);
            _clientsRepositoryMock.Verify(c => c.GetClientById(It.IsAny<int>()), Times.Once);
            _clientsRepositoryMock.Verify(c => c.GetClients(), Times.Never);
            _clientsRepositoryMock.Verify(c => c.GetCommentsByClientId(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void GetCommentsByClientId_EmptyClientRequest_ThrowEntityNotFoundException()
        {
            //given
            var clientInDb = new Client();
            var testEmail = "bnb@gamil.ru";

            _identities = new List<Claim> { new Claim(ClaimTypes.Name, testEmail), new Claim(ClaimTypes.Name, "Client") };

            _clientsRepositoryMock.Setup(o => o.GetClientById(clientInDb.Id)).Returns(clientInDb);
            _clientsRepositoryMock.Setup(o => o.GetCommentsByClientId(clientInDb.Id)).Returns(clientInDb.Comments);
            //when
          

            //then
            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.GetCommentsByClientId(clientInDb.Id, _identities));

        }

        [Test]
        public void GetCommentsByClientId_ClientGetSomeoneElsesProfile_ThrowAccessException()
        {

            //given
            var clientInDb = new Client()
            {

                Id = 1,
                Name = "Vasya",
                LastName = "Petrov",
                Email = "Va@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375",
                Comments = new()
                {
                    new()
                    {
                             Id = 1, Text="ApAp",Rating=1,Date=DateTime.Now
                    },

                    new()
                    {
                        Id = 2, Text="222",Rating=3,Date=DateTime.Now
                    },
                }

            };
            var testEmail = "bnb@gamil.ru";

            _identities = new List<Claim> { new Claim(ClaimTypes.Name, testEmail), new Claim(ClaimTypes.Name, "Client") };

            _clientsRepositoryMock.Setup(o => o.GetClientById(clientInDb.Id)).Returns(clientInDb);
            _clientsRepositoryMock.Setup(o => o.GetCommentsByClientId(clientInDb.Id)).Returns(clientInDb.Comments);
            //when


            //then
            Assert.Throws<Exceptions.AccessException>(() => _sut.GetCommentsByClientId(clientInDb.Id, _identities));

        }


        [Test]
        public void GetOrdersByClientId_ValidRequestPassed_RequestedTypeReceived()
        {
            //given

            var clientInDb = new Client()
            {
                Id= 1,
                Name = "Vasya",
                LastName = "Petrov",
                Email = "Va@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375",
                Orders = new()
            {
                new()
                {
                    Id = 1, Message="ApAp",Cost=1,PayDate=DateTime.Now
                },
                new()
                {
                    Id = 2, Message="222",Cost=3,PayDate=DateTime.Now
                }
            },

            };

            _identities = new List<Claim> { new Claim(ClaimTypes.Name, clientInDb.Email), new Claim(ClaimTypes.Name, "Client") };

            _clientsRepositoryMock.Setup(o => o.GetClientById(clientInDb.Id)).Returns(clientInDb);
            _clientsRepositoryMock.Setup(o => o.GetOrdersByClientId(clientInDb.Id)).Returns(clientInDb.Orders);

            //when
            var actual = _sut.GetOrdersByClientId(clientInDb.Id, _identities);


            //then
            Assert.True(clientInDb.Orders.Count == actual.Count);
            Assert.True(actual[0].Id == clientInDb.Orders[0].Id);
            Assert.True(actual[1].Id == clientInDb.Orders[1].Id);
            Assert.True(actual[0].Cost == clientInDb.Orders[0].Cost);
            Assert.True(actual[1].Cost == clientInDb.Orders[1].Cost);
            _clientsRepositoryMock.Verify(c => c.GetClientById(It.IsAny<int>()), Times.Once);  
            _clientsRepositoryMock.Verify(c => c.GetOrdersByClientId(It.IsAny<int>()), Times.Once);
            _clientsRepositoryMock.Verify(c => c.GetCommentsByClientId(It.IsAny<int>()), Times.Never);
        }


        [Test]
        public void GetOrdersByClientId_EmptyClientRequest_ThrowEntityNotFoundException()
        {
            //given
            var clientInDb = new Client();
            var testEmail = "bnb@gamil.ru";

            _identities = new List<Claim> { new Claim(ClaimTypes.Name, testEmail), new Claim(ClaimTypes.Name, "Client") };

            _clientsRepositoryMock.Setup(o => o.GetClientById(clientInDb.Id)).Returns(clientInDb);
            _clientsRepositoryMock.Setup(o => o.GetOrdersByClientId(clientInDb.Id)).Returns(clientInDb.Orders);
            //when


            //then
            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.GetOrdersByClientId(clientInDb.Id, _identities));

        }


        [Test]
        public void GetOrdersByClientId_ClientGetSomeoneElsesProfile_ThrowAccessException()
        {

            //given
            var clientInDb = new Client()
            {

                Id = 1,
                Name = "Vasya",
                LastName = "Petrov",
                Email = "Va@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375",
                Comments = new()
                {
                    new()
                    {
                             Id = 1, Text="ApAp",Rating=1,Date=DateTime.Now
                    },

                    new()
                    {
                        Id = 2, Text="222",Rating=3,Date=DateTime.Now
                    },
                }

            };
            var testEmail = "bnb@gamil.ru";

            _identities = new List<Claim> { new Claim(ClaimTypes.Name, testEmail), new Claim(ClaimTypes.Name, "Client") };

            _clientsRepositoryMock.Setup(o => o.GetClientById(clientInDb.Id)).Returns(clientInDb);
            _clientsRepositoryMock.Setup(o => o.GetOrdersByClientId(clientInDb.Id)).Returns(clientInDb.Orders);
            //when


            //then
            Assert.Throws<Exceptions.AccessException>(() => _sut.GetCommentsByClientId(clientInDb.Id, _identities));

        }


        [Test]
        public void UpdateClient_ValidRequestPassed_ChangesProperties()
        {
            //given

            var client = new Client()
            {
                Id = 1,
                Name = "Vasya",
                LastName = "Petrov",
                Email = "Va@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375",
                BirthDate = new DateTime(1990, 05, 02),
            };


            Client newClientModel = new Client()
            {
                Name = "Petro",
                LastName = "Sobakov",
                BirthDate = new DateTime(1998, 10, 10),
            };

            _identities = new List<Claim> { new Claim(ClaimTypes.Name, client.Email), new Claim(ClaimTypes.Name, "Client") };
            _clientsRepositoryMock.Setup(o => o.GetClientById(client.Id)).Returns(client);
            _clientsRepositoryMock.Setup(o => o.UpdateClient(newClientModel, client.Id));


            //when
             _sut.UpdateClient(newClientModel, client.Id, _identities);

            //then
            var actual = _sut.GetClientById(client.Id, _identities);


            Assert.True(client.Name == actual.Name);
            Assert.True(client.LastName == actual.LastName);
            Assert.True(client.BirthDate == actual.BirthDate);
            _clientsRepositoryMock.Verify(c => c.GetClientById(It.IsAny<int>()), Times.Exactly(2));
            _clientsRepositoryMock.Verify(c => c.UpdateClient(It.IsAny<Client>(), It.IsAny<int>()), Times.Once);
            _clientsRepositoryMock.Verify(c => c.GetOrdersByClientId(It.IsAny<int>()), Times.Never);
            _clientsRepositoryMock.Verify(c => c.GetCommentsByClientId(It.IsAny<int>()), Times.Never);

        }

        [Test]
        public void UpdateClient_EmptyClientRequest_ThrowEntityNotFoundException()
        {
            //given

            var client = new Client();
            var testEmail = "bnb@gamil.ru";

            Client newClientModel = new Client()
            {
                Name = "Petro",
                LastName = "Sobakov",
                BirthDate = new DateTime(1998, 10, 10),
            };

            _identities = new List<Claim> { new Claim(ClaimTypes.Name, testEmail), new Claim(ClaimTypes.Name, "Client") };

            _clientsRepositoryMock.Setup(o => o.UpdateClient(newClientModel, client.Id));


            //when


            //then
            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.UpdateClient(newClientModel, client.Id, _identities));

        }


        [Test]
        public void UpdateClient_ClientGetSomeoneElsesProfile_ThrowAccessException()
        {
            //given
            var testEmail = "bnb@gamil.ru";

            var client = new Client()
            {
                Id = 1,
                Name = "Vasya",
                LastName = "Petrov",
                Email = "Va@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375",
                BirthDate = new DateTime(1990, 05, 02),
            };


            Client newClientModel = new Client()
            {
                Name = "Petro",
                LastName = "Sobakov",
                BirthDate = new DateTime(1998, 10, 10),
            };

            _identities = new List<Claim> { new Claim(ClaimTypes.Name, testEmail), new Claim(ClaimTypes.Name, "Client") };
            _clientsRepositoryMock.Setup(o => o.GetClientById(client.Id)).Returns(client);
            _clientsRepositoryMock.Setup(o => o.UpdateClient(newClientModel, client.Id));


            //when


            //then
            Assert.Throws<Exceptions.AccessException>(() => _sut.UpdateClient(newClientModel, client.Id, _identities));

        }


        [Test]
        public void DeleteClient_ValidRequestPassed_DeleteClient()
        {
            //given
            var expectedClient = new Client()
            {
                Id = 1,
                Name = "Vasya",
                LastName = "Petrov",
                Email = "Va@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375",
                IsDeleted = false

            };

           

            _clientsRepositoryMock.Setup(o => o.GetClientById(expectedClient.Id)).Returns(expectedClient);
            _clientsRepositoryMock.Setup(o => o.DeleteClient(expectedClient.Id));
            _identities = new List<Claim> { new Claim(ClaimTypes.Name, expectedClient.Email), new Claim(ClaimTypes.Name, "Client") };


            //when
             _sut.DeleteClient(expectedClient.Id, _identities);


            //then

            var allClients = _sut.GetClients();

            Assert.True(allClients is null);
             _clientsRepositoryMock.Verify(c => c.DeleteClient(It.IsAny<int>()), Times.Once);
            _clientsRepositoryMock.Verify(c => c.GetClientById(It.IsAny<int>()), Times.Once);
            _clientsRepositoryMock.Verify(c => c.GetClients(), Times.Once);
            _clientsRepositoryMock.Verify(c => c.UpdateClient(It.IsAny<Client>(), It.IsAny<int>()), Times.Never);

        }


        [Test]
        public void DeleteClient_EmptyClientRequest_ThrowEntityNotFoundException()
        {
            //given
            var testId = 1;
            var client = new Client();
            var testEmail = "bnb@gamil.ru";

            

            _identities = new List<Claim> { new Claim(ClaimTypes.Name, testEmail), new Claim(ClaimTypes.Name, "Client") };

            _clientsRepositoryMock.Setup(o => o.DeleteClient(testId));


            //when


            //then
            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.DeleteClient(testId, _identities));

        }


        [Test]
        public void DeleteClient_ClientGetSomeoneElsesProfile_ThrowAccessException()
        {
            //given

            var clientFirst = new Client()
            {
                Id = 1,
                Name = "Vasya",
                LastName = "Petrov",
                Email = "Vasya@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375",
                IsDeleted = false

            };
            var clientSecond = new Client()
            {
                Id = 2,
                Name = "Vasya",
                LastName = "Petrov",
                Email = "Va@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375",
                IsDeleted = false

            };




            _identities = new List<Claim> { new Claim(ClaimTypes.Name, clientFirst.Email), new Claim(ClaimTypes.Name, "Client") };
            _clientsRepositoryMock.Setup(o => o.GetClientById(clientSecond.Id)).Returns(clientSecond);
            
            //when


            //then
            Assert.Throws<Exceptions.AccessException>(() => _sut.DeleteClient(clientSecond.Id, _identities));

        }


        
    }
}