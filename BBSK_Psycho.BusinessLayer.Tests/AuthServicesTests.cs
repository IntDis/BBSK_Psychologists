using BBSK_Psycho.BusinessLayer.Services;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;
using Moq;

namespace BBSK_Psycho.BusinessLayer.Tests
{
    public class AuthServicesTests
    {
        private AuthServices _sut;
        private Mock<IClientsRepository> _clientsRepositoryMock;
        private Mock<IPsychologistsRepository> _psychologistsRepository;
        private Mock<IManagerRepository> _managerRepository;

        [SetUp]
        public void Setup()
        {

            _clientsRepositoryMock = new Mock<IClientsRepository>();
            _psychologistsRepository = new Mock<IPsychologistsRepository>();
            _managerRepository = new Mock<IManagerRepository>();
            _sut = new AuthServices(_clientsRepositoryMock.Object, _psychologistsRepository.Object, _managerRepository.Object);
        }


        [Test]
        public void Login_ValidManagerEmailAndPassword_ClaimModelReturned()
        {
            //given
            var managerPassword = "12345678954";
            var managerExpected = new Manager()
            {
                Password = PasswordHash.HashPassword("12345678954"),
                Email = "J@gmail.com",
                IsDeleted = false,
            };

            _managerRepository.Setup(m => m.GetManagerByEmail(managerExpected.Email)).Returns(managerExpected);
            //when

            var claim= _sut.GetUserForLogin(managerExpected.Email, managerPassword);
            //then

            Assert.True(claim.Role == Role.Manager);
            Assert.True(claim.Email == managerExpected.Email);
            _managerRepository.Verify(c => c.GetManagerByEmail(It.IsAny<string>()), Times.Once);
            _psychologistsRepository.Verify(c => c.GetPsychologistByEmail(It.IsAny<string>()), Times.Never);
            _clientsRepositoryMock.Verify(c => c.GetClientByEmail(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void Login_ValidClientEmailAndPassword_ClaimModelReturned()
        {
            //given
            var clientPassword = "12345678";
            var clientExpected = new Client()
            {
                Name = "Petro",
                LastName = "Petrov",
                Password = PasswordHash.HashPassword("12345678"),
                Email = "J@gmail.com",
                PhoneNumber = "89119118696",
                BirthDate = new DateTime(2020, 05, 05),
            };

            _clientsRepositoryMock.Setup(c => c.GetClientByEmail(clientExpected.Email)).Returns(clientExpected);


            //when
            var claim = _sut.GetUserForLogin(clientExpected.Email, clientPassword);

            //then
            Assert.True(claim.Role == Role.Client);
            Assert.True(claim.Email == clientExpected.Email);
            _clientsRepositoryMock.Verify(c => c.GetClientByEmail(It.IsAny<string>()), Times.Once);
            _managerRepository.Verify(c => c.GetManagerByEmail(It.IsAny<string>()), Times.Once);
            _psychologistsRepository.Verify(c => c.GetPsychologistByEmail(It.IsAny<string>()), Times.Once);

        }

        [Test]
        public void Login_ValidPsychologistsEmailAndPassword_ClaimModelReturned()
        {
            //given
            var passwordPsychologistsExpected = "12334534";
            var psychologistsExpected = new Psychologist()
            {
                Name = "Dantes",
                LastName = "Don",
                Email = "ros@fja.com",
                Password = PasswordHash.HashPassword("12334534")
            };


            _psychologistsRepository.Setup(c => c.GetPsychologistByEmail(psychologistsExpected.Email)).Returns(psychologistsExpected);

            //when
            var claim = _sut.GetUserForLogin(psychologistsExpected.Email, passwordPsychologistsExpected);

            //then
            Assert.True(claim.Role == Role.Psychologist);
            Assert.True(claim.Email == psychologistsExpected.Email);
            _clientsRepositoryMock.Verify(c => c.GetClientByEmail(It.IsAny<string>()), Times.Once);
            _managerRepository.Verify(c => c.GetManagerByEmail(It.IsAny<string>()), Times.Once);
            _psychologistsRepository.Verify(c => c.GetPsychologistByEmail(It.IsAny<string>()), Times.Once);
        }


        [Test]
        public void Login_BadPassword_ThrowEntityNotFoundException()
        {
            //given
            var badPassword = "123456789541";
            var psychologistsExpected = new Psychologist()
            {
                Name = "Dantes",
                LastName = "Don",
                Patronymic = "Petrovich",   
                Email = "ros@fja.com",             
                Password = PasswordHash.HashPassword("12334534"),
            };
            
            _psychologistsRepository.Setup(c => c.GetPsychologistByEmail(psychologistsExpected.Email)).Returns(psychologistsExpected);


            //when, then
            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.GetUserForLogin(psychologistsExpected.Email, badPassword));
            
        }

        [Test]
        public void Login_IsDeletedTrue_ThrowEntityNotFoundException()
        {
            //given
            var password = "12334534";
            var clientExpected = new Client()
            {
                Name = "Dantes",
                LastName = "Don",
                Email = "ros@fja.com",
                Password = PasswordHash.HashPassword("12334534"),
                IsDeleted = true
            };

            _clientsRepositoryMock.Setup(c => c.GetClientByEmail(clientExpected.Email)).Returns(clientExpected);


            //when, then
            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.GetUserForLogin(clientExpected.Email, password));

        }

        [Test]
        public void Login_UserNotFound_ThrowEntityNotFoundException()
        {
            //given
            var badEmail = "ad@mmm.com";
            var clientExpected = new Client()
            {
                Id = 1,
                Name = "Petro",
                LastName = "Petrov",
                Password = "12345678",
                Email = "J@gmail.com",
                PhoneNumber = "89119118696",
                BirthDate = new DateTime(2020, 05, 05),
            };



            //when, then
            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.GetUserForLogin(badEmail, clientExpected.Password));
            
        }

        [Test]
        public void GetToken_ValidData_TokenReturned()
        {
            //given
            var model = new ClaimModel()
            {
                Email = "ada@gmail.com",
                Role = Role.Client
            };

            //when
            var actual=_sut.GetToken(model);

            //then

            Assert.True(actual is not null);
           
        }

        [Test]
        public void GetToken_EmailEmpty_ThrowDataException()
        {
            //given
            var model = new ClaimModel()
            {
                Email = null,
                Role = Role.Client
            };

            //when, then
            Assert.Throws<Exceptions.DataException>(() => _sut.GetToken(model));

        }

        
        [Test]
        public void GetToken_PropertysEmpty_ThrowDataException()
        {
            //given
            var model = new ClaimModel();
            

            //when, then
            Assert.Throws<Exceptions.DataException>(() => _sut.GetToken(model));

        }
    }
}

