using BBSK_Psycho.BusinessLayer.Services;
using BBSK_Psycho.BusinessLayer.Tests.ModelControllerSource;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;
using Moq;


namespace BBSK_Psycho.BusinessLayer.Tests
{
    public class ClientServicesTests
    {
        private ClientsService _sut;
        private Mock<IClientsRepository> _clientsRepositoryMock;

        private ClaimModel _claims;

        [SetUp]
        public void Setup()
        {

            _clientsRepositoryMock = new Mock<IClientsRepository>();
            _sut = new ClientsService(_clientsRepositoryMock.Object);
        }



        [Test]
        public void AddClient_ValidRequestPassed_AddClientAndIdReturned()
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

            Assert.AreEqual(actual, expectedId);
            _clientsRepositoryMock.Verify(c => c.AddClient(client), Times.Once);
        }



        [Test]
        public void AddClient_NotUniqueEmail_ThrowUniquenessException()
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
            _clientsRepositoryMock.Setup(c => c.GetClientByEmail("J@gmail.com")).Returns(clients[0]);


            //when,then
            Assert.Throws<Exceptions.UniquenessException>(() => _sut.AddClient(clientNew));

            _clientsRepositoryMock.Verify(c => c.AddClient(It.IsAny<Client>()), Times.Never);


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


            _clientsRepositoryMock.Setup(o => o.GetClients()).Returns(clients);

            //when
            var actual = _sut.GetClients();

            //then
            Assert.NotNull(actual);
            Assert.True(actual.GetType() == typeof(List<Client>));
            Assert.AreEqual(actual[0].Comments, null);
            Assert.AreEqual(actual[1].Orders, null);
            Assert.False(actual[0].IsDeleted);
            Assert.True(actual[1].IsDeleted);
            _clientsRepositoryMock.Verify(c => c.GetClients(), Times.Once);
        }





        [TestCase("Client")]
        [TestCase("Manager")]
        public void GetClientById_ValidRequestPassed_ClientReceived(string roleString)
        {

            //given
            Role role = Enum.Parse<Role>(roleString);
            var clientInDb = new Client()
            {
                Id = 1,
                Name = "Roma",
                LastName = "Petrov",
                Email = "Va@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375",
            };


            if (role == Role.Manager)
            {
                clientInDb.Email = null;
            }

            _claims = new() { Email = clientInDb.Email, Role = role };


            _clientsRepositoryMock.Setup(o => o.GetClientById(clientInDb.Id)).Returns(clientInDb);


            //when
            var actual = _sut.GetClientById(clientInDb.Id, _claims);

            //then

            Assert.AreEqual(actual.Id, clientInDb.Id);
            Assert.AreEqual(actual.Name, clientInDb.Name);
            Assert.AreEqual(actual.LastName, clientInDb.LastName);
            Assert.AreEqual(actual.Email, clientInDb.Email);
            Assert.AreEqual(actual.Password, clientInDb.Password);
            Assert.AreEqual(actual.PhoneNumber, clientInDb.PhoneNumber);
            Assert.False(actual.IsDeleted);
            _clientsRepositoryMock.Verify(c => c.GetClientById(clientInDb.Id), Times.Once);

        }


        [Test]
        public void GetClientById_EmptyRequest_ThrowEntityNotFoundException()
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



            _claims = new() { Email = clientInDb.Email, Role = Role.Client };


            _clientsRepositoryMock.Setup(o => o.GetClientById(clientInDb.Id)).Returns(clientInDb);



            //when, then

            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.GetClientById(testId, _claims));
            _clientsRepositoryMock.Verify(c => c.GetClientById(testId), Times.Once);

        }


        [TestCase("Client")]
        [TestCase("Psychologist")]
        public void GetClientById_ClientGetAccessToAnotherClientOrRolePsychologist_ThrowAccessException(string roleString)
        {

            //given
            Role role = Enum.Parse<Role>(roleString);
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

            if (role == Role.Psychologist)
            {
                testEmail = clientInDb.Email;
            }

            _claims = new() { Email = testEmail, Role = role };


            _clientsRepositoryMock.Setup(o => o.GetClientById(clientInDb.Id)).Returns(clientInDb);



            //when, then

            Assert.Throws<Exceptions.AccessException>(() => _sut.GetClientById(clientInDb.Id, _claims));
            _clientsRepositoryMock.Verify(c => c.GetClientById(clientInDb.Id), Times.Once);

        }


        [TestCase("Client")]
        [TestCase("Manager")]
        public void GetCommentsByClientId_ValidRequestPassed_CommentsReceived(string roleString)
        {
            //given
            Role role = Enum.Parse<Role>(roleString);
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

            if (role == Role.Manager)
            {
                clientInDb.Email = null;
            }
            _claims = new() { Email = clientInDb.Email, Role = role };

            _clientsRepositoryMock.Setup(o => o.GetClientById(clientInDb.Id)).Returns(clientInDb);
            _clientsRepositoryMock.Setup(o => o.GetCommentsByClientId(clientInDb.Id)).Returns(clientInDb.Comments);

            //when
            var actual = _sut.GetCommentsByClientId(clientInDb.Id, _claims);

            //then

            Assert.AreEqual(clientInDb.Comments.Count, actual.Count);
            Assert.AreEqual(actual[0].Id, clientInDb.Comments[0].Id);
            Assert.AreEqual(actual[1].Id,clientInDb.Comments[1].Id);
            Assert.AreEqual(actual[0].Rating,clientInDb.Comments[0].Rating);
            Assert.AreEqual(actual[1].Rating,clientInDb.Comments[1].Rating);
            _clientsRepositoryMock.Verify(c => c.GetClientById(clientInDb.Id), Times.Once);
            _clientsRepositoryMock.Verify(c => c.GetCommentsByClientId(clientInDb.Id), Times.Once);
        }

        [Test]
        public void GetCommentsByClientId_EmptyClientRequest_ThrowEntityNotFoundException()
        {
            //given
            var clientInDb = new Client();
            Client? eptyClient = null;


            _claims = new() { Email = clientInDb.Email, Role = Role.Client };

            _clientsRepositoryMock.Setup(o => o.GetClientById(clientInDb.Id)).Returns(eptyClient);
            _clientsRepositoryMock.Setup(o => o.GetCommentsByClientId(clientInDb.Id)).Returns(clientInDb.Comments);


            //when, then
            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.GetCommentsByClientId(clientInDb.Id, _claims));
            _clientsRepositoryMock.Verify(c => c.GetCommentsByClientId(It.IsAny<int>()), Times.Never);


        }

        [TestCase("Client")]
        [TestCase("Psychologist")]
        public void GetCommentsByClientId_ClientGetAccessToAnotherClientOrRolePsychologist_ThrowAccessException(string roleString)
        {

            //given
            Role role = Enum.Parse<Role>(roleString);
            var testEmail = "bnb@gamil.ru";

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


            if (role == Role.Psychologist)
            {
                testEmail = clientInDb.Email;
            }

            _claims = new() { Email = testEmail, Role = role };

            _clientsRepositoryMock.Setup(o => o.GetClientById(clientInDb.Id)).Returns(clientInDb);
            _clientsRepositoryMock.Setup(o => o.GetCommentsByClientId(clientInDb.Id)).Returns(clientInDb.Comments);


            //when, then
            Assert.Throws<Exceptions.AccessException>(() => _sut.GetCommentsByClientId(clientInDb.Id, _claims));
            _clientsRepositoryMock.Verify(c => c.GetCommentsByClientId(It.IsAny<int>()), Times.Never);

        }


        [TestCase("Client")]
        [TestCase("Manager")]
        public void GetOrdersByClientId_ValidRequestPassed_RequestedTypeReceived(string roleString)
        {
            //given
            Role role = Enum.Parse<Role>(roleString);

            var clientInDb = new Client()
            {
                Id = 1,
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

            if (role == Role.Manager)
            {
                clientInDb.Email = null;
            }
            _claims = new() { Email = clientInDb.Email, Role = role };

            _clientsRepositoryMock.Setup(o => o.GetClientById(clientInDb.Id)).Returns(clientInDb);
            _clientsRepositoryMock.Setup(o => o.GetOrdersByClientId(clientInDb.Id)).Returns(clientInDb.Orders);

            //when
            var actual = _sut.GetOrdersByClientId(clientInDb.Id, _claims);


            //then
            Assert.True(clientInDb.Orders.Count == actual.Count);
            Assert.True(actual[0].Id == clientInDb.Orders[0].Id);
            Assert.True(actual[1].Id == clientInDb.Orders[1].Id);
            Assert.True(actual[0].Cost == clientInDb.Orders[0].Cost);
            Assert.True(actual[1].Cost == clientInDb.Orders[1].Cost);
            _clientsRepositoryMock.Verify(c => c.GetClientById(clientInDb.Id), Times.Once);
            _clientsRepositoryMock.Verify(c => c.GetOrdersByClientId(clientInDb.Id), Times.Once);

        }


        [Test]
        public void GetOrdersByClientId_EmptyClientRequest_ThrowEntityNotFoundException()
        {
            //given
            var clientInDb = new Client();
            Client? emptyClient = null;


            _claims = new() { Email = clientInDb.Email, Role = Role.Client };

            _clientsRepositoryMock.Setup(o => o.GetClientById(clientInDb.Id)).Returns(emptyClient);
            _clientsRepositoryMock.Setup(o => o.GetOrdersByClientId(clientInDb.Id)).Returns(clientInDb.Orders);

            //when, then
            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.GetOrdersByClientId(clientInDb.Id, _claims));
            _clientsRepositoryMock.Verify(c => c.GetOrdersByClientId(It.IsAny<int>()), Times.Never);

        }


        [TestCase("Client")]
        [TestCase("Psychologist")]
        public void GetOrdersByClientId_ClientGetAccessToAnotherClientOrRolePsychologist_ThrowAccessException(string roleString)
        {

            //given
            Role role = Enum.Parse<Role>(roleString);

            var testEmail = "bnb@gamil.ru";

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
            if (role == Role.Psychologist)
            {
                testEmail = clientInDb.Email;
            }
            _claims = new() { Email = testEmail, Role = role };

            _clientsRepositoryMock.Setup(o => o.GetClientById(clientInDb.Id)).Returns(clientInDb);
            _clientsRepositoryMock.Setup(o => o.GetOrdersByClientId(clientInDb.Id)).Returns(clientInDb.Orders);


            //when, then
            Assert.Throws<Exceptions.AccessException>(() => _sut.GetCommentsByClientId(clientInDb.Id, _claims));
            _clientsRepositoryMock.Verify(c => c.GetOrdersByClientId(It.IsAny<int>()), Times.Never);

        }


        [TestCase("Client")]
        [TestCase("Manager")]
        public void UpdateClient_ValidRequestPassed_ChangesProperties(string roleString)
        {
            //given
            Role role = Enum.Parse<Role>(roleString);

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

            if (role == Role.Manager)
            {
                client.Email = null;
            }
            _claims = new() { Email = client.Email, Role = role };

            _clientsRepositoryMock.Setup(o => o.GetClientById(client.Id)).Returns(client);


            //when
            _sut.UpdateClient(newClientModel, client.Id, _claims);

            //then
            var actual = _sut.GetClientById(client.Id, _claims);


            Assert.True(client.Name == actual.Name);
            Assert.True(client.LastName == actual.LastName);
            Assert.True(client.BirthDate == actual.BirthDate);
            _clientsRepositoryMock.Verify(c => c.GetClientById(client.Id), Times.Exactly(2));
            _clientsRepositoryMock.Verify(c => c.UpdateClient(It.Is<Client>(c=>
            c.Name == newClientModel.Name &&
            c.LastName == newClientModel.LastName &&
            c.BirthDate == newClientModel.BirthDate &&
            !c.IsDeleted)), Times.Once);


        }

        [Test]
        public void UpdateClient_ReferringToNonExixtenObject_ThrowEntityNotFoundException()
        {
            //given

            var client = new Client();

            Client newClientModel = new Client()
            {
                Name = "Petro",
                LastName = "Sobakov",
                BirthDate = new DateTime(1998, 10, 10),
            };

            _claims = new() { Email = client.Email, Role = Role.Client };

            //when, then
            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.UpdateClient(newClientModel, client.Id, _claims));
            _clientsRepositoryMock.Verify(c => c.UpdateClient(It.IsAny<Client>()), Times.Never);

        }


        [TestCase("Client")]
        [TestCase("Psychologist")]
        public void UpdateClient_ClientGetAccessToAnotherClientOrRolePsychologist_ThrowAccessException(string roleString)
        {
            //given
            Role role = Enum.Parse<Role>(roleString);
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

            if (role == Role.Psychologist)
            {
                testEmail = client.Email;
            }
            _claims = new() { Email = testEmail, Role = role };

            _clientsRepositoryMock.Setup(o => o.GetClientById(client.Id)).Returns(client);

            //when, then
            Assert.Throws<Exceptions.AccessException>(() => _sut.UpdateClient(newClientModel, client.Id, _claims));
            _clientsRepositoryMock.Verify(c => c.UpdateClient(It.IsAny<Client>()), Times.Never);

        }


        [TestCase("Client")]
        [TestCase("Manager")]
        public void DeleteClient_ValidRequestPassed_DeleteClient(string roleString)
        {
            //given
            Role role = Enum.Parse<Role>(roleString);
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

            if (role == Role.Manager)
            {
                expectedClient.Email = null;
            }

            _claims = new() { Email = expectedClient.Email, Role = role };

            _clientsRepositoryMock.Setup(o => o.GetClientById(expectedClient.Id)).Returns(expectedClient);

            //when
            _sut.DeleteClient(expectedClient.Id, _claims);


            //then

            var allClients = _sut.GetClients();
            Assert.True(allClients is null);
            _clientsRepositoryMock.Verify(c => c.DeleteClient(expectedClient), Times.Once);
            _clientsRepositoryMock.Verify(c => c.GetClientById(expectedClient.Id), Times.Once);
            _clientsRepositoryMock.Verify(c => c.GetClients(), Times.Once);


        }


        [Test]
        public void DeleteClient_ReferringToNonExixtenObject_ThrowEntityNotFoundException()
        {
            //given
            var testId = 1;

            _claims = new();


            //when, then
            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.DeleteClient(testId, _claims));
            _clientsRepositoryMock.Verify(c => c.DeleteClient(It.IsAny<Client>()), Times.Never);

        }


        [TestCase("Client")]
        [TestCase("Psychologist")]
        public void DeleteClient_ClientGetAccessToAnotherClientOrRolePsychologist_ThrowAccessException(string roleString)
        {
            //given
            Role role = Enum.Parse<Role>(roleString);
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


            if (role == Role.Psychologist)
            {
                clientFirst.Email = clientSecond.Email;
            }

            _claims = new() { Email = clientFirst.Email, Role = role };
            _clientsRepositoryMock.Setup(o => o.GetClientById(clientSecond.Id)).Returns(clientSecond);


            //when, then
            Assert.Throws<Exceptions.AccessException>(() => _sut.DeleteClient(clientSecond.Id, _claims));
            _clientsRepositoryMock.Verify(c => c.DeleteClient(It.IsAny<Client>()), Times.Never);
        }


        [TestCase("Client")]
        [TestCase("Manager")]
        public void GetApplicationsForPsychologistByClientId_ValidRequestPassed_ApplicationsReceived(string roleString)
        {
            //given
            Role role = Enum.Parse<Role>(roleString);

            var client = new Client()
            {
                Id = 1,
                Name = "Vasya",
                Email = "a@gmail.com",
                ApplicationForPsychologistSearch = new()
                {

                    new()
                    {
                         Id=1,
                         Description ="Help"

                    },
                    new()
                    {
                        Id=2,
                        Description ="Hi"
                    }

                }
            };


            if (role == Role.Manager)
            {
                client.Email = null;
            }
            _claims = new() { Email = client.Email, Role = role };

            _clientsRepositoryMock.Setup(c => c.GetClientById(client.Id)).Returns(client);

            //when
            var actual = _sut.GetApplicationsForPsychologistByClientId(client.Id, _claims);

            //then

            Assert.AreEqual(actual, client.ApplicationForPsychologistSearch);
            _clientsRepositoryMock.Verify(a => a.GetClientById(client.Id), Times.Once);

        }

        [Test]
        public void GetApplicationsForPsychologistByClientId_ReferringToNonExixtenObject_ThrowEntityNotFoundException()
        {
            //given
            var client = new Client()
            {
                Id = 1,
                Name = "Vasya",
                Email = "a@gmail.com",

            };
            _claims = new() { Email = client.Email, Role = Role.Client };

            //when, then
            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.GetApplicationsForPsychologistByClientId(client.Id, _claims));
            _clientsRepositoryMock.Verify(c => c.GetClientById(client.Id), Times.Once);

        }


        [TestCase("Client")]
        [TestCase("Psychologist")]
        public void GetApplicationsForPsychologistByClientId_ClientGetAccessToAnotherClientOrRolePsychologist_ThrowAccessException(string roleString)
        {
            //given
            Role role = Enum.Parse<Role>(roleString);

            var testEmail = "pp@mail.com";

            var client = new Client()
            {
                Id = 1,
                Name = "Vasya",
                Email = "a@gmail.com",
                ApplicationForPsychologistSearch = new()
                {
                    new()
                    {
                        Id=1,
                        Description ="Help"
                    },
                    new()
                    {
                        Id=2,
                        Description ="Hi"
                    }
                }
            };

            if (role == Role.Psychologist)
            {
                testEmail = client.Email;
            }

            _claims = new() { Email = testEmail, Role = role };
            _clientsRepositoryMock.Setup(o => o.GetClientById(client.Id)).Returns(client);

            //when, then
            Assert.Throws<Exceptions.AccessException>(() => _sut.GetApplicationsForPsychologistByClientId(client.Id, _claims));
            _clientsRepositoryMock.Verify(c => c.GetClientById(client.Id), Times.Once);
        }
    }
}