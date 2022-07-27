using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.BusinessLayer.Tests
{
    public class PsychologistServicesTests
    {
        private PsychologistService _sut;
        private Mock<IPsychologistsRepository> _psychologistsRepositoryMock;
        private Mock <IOrdersRepository> _ordersRepositoryMock;
        private Mock <IClientsRepository> _clientsRepositoryMock;
        private ClaimModel _claims;
        //given
        //when
        //then
        [SetUp]
        public void Setup()
        {
            _psychologistsRepositoryMock = new Mock<IPsychologistsRepository>();
            _ordersRepositoryMock = new Mock<IOrdersRepository>();
            _clientsRepositoryMock = new Mock<IClientsRepository>();
            _sut = new PsychologistService(_psychologistsRepositoryMock.Object, _clientsRepositoryMock.Object, _ordersRepositoryMock.Object);
        }

        [Test]
        public void AddCommentToPsychologist_WhenPsychologistIsNotNull_ReturnComment()
        {
            //given
            var comment = new Comment();
            var order= new Order();
            var client = new Client()
            {
                Email = "test@mail.ru"
            };
            _ordersRepositoryMock.Setup(o => o.GetOrderByPsychIdAndClientId(It.IsAny<int>(), It.IsAny<int>())).Returns(order);
            _psychologistsRepositoryMock.Setup(c => c.AddCommentToPsyhologist(It.IsAny<Comment>(), (It.IsAny<int>())))
                .Returns(comment);
            _clientsRepositoryMock.Setup(c => c.GetClientById(It.IsAny<int>())).Returns(client);
            var expectedComment = comment;

            var commentActual = new Comment()
            {
                Text = "very cool",
                Rating = 5,
                Date = DateTime.Now,
                Psychologist = new Psychologist(),
                Client = new Client()
            };
            _claims = new()
            {
                Email = "test@mail.ru"
            };
            //when
            var actual = _sut.AddCommentToPsyhologist(commentActual, commentActual.Id, _claims);

            //then
            Assert.AreEqual(expectedComment.Id, actual);
            _psychologistsRepositoryMock.Verify(c => c.AddCommentToPsyhologist(commentActual, commentActual.Id), Times.Once);
        }

        [Test]
        public void AddCommentToPsychologist_InValidClientId_ReturnEntityNotFoundException()
        {
            //given
            var comment = new Comment()
            {
                Text = "very cool",
                Rating = 5,
                Date = DateTime.Now,
                Psychologist = new Psychologist(),
                Client = new Client()
            };
            var psychologistId = 1;
            _claims = new();

            _ordersRepositoryMock.Setup(o => o.GetOrderByPsychIdAndClientId(It.IsAny<int>(), It.IsAny<int>())).Returns((Order?)null);
            //when
            //then
            Assert.Throws<Exceptions.AccessException>(() => _sut.AddCommentToPsyhologist(comment, psychologistId, _claims));
        }


        [Test]
        public void AddCommentToPsychologist_InvalidRequestPassed_ReturnEntityNotFoundException()
        {
            //given
            var comment = new Comment();
            var order = new Order();
            var client = new Client()
            {
                Email = "test@mail.ru"
            };
            _ordersRepositoryMock.Setup(o => o.GetOrderByPsychIdAndClientId(It.IsAny<int>(), It.IsAny<int>())).Returns(order);
            var expectedComment = comment;
            var psychologistId = 1;
            var commentActual = new Comment()
            {
                Text = "very cool",
                Rating = 5,
                Date = DateTime.Now,
                Psychologist = new Psychologist(),
                Client = new Client()
            };
            _claims = new();
            //when

            //then
            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.AddCommentToPsyhologist(commentActual, psychologistId, _claims));
        }

        [Test]
        public void AddCommentToPsychologist_InvalidRolePassed_ReturnAccessdenied()
        {
            //given
            var comment = new Comment();
            var order = new Order();
            var client = new Client()
            {
                Email = "test@mail.ru"
            };
            _ordersRepositoryMock.Setup(o => o.GetOrderByPsychIdAndClientId(It.IsAny<int>(), It.IsAny<int>())).Returns(order);
           
            _clientsRepositoryMock.Setup(c => c.GetClientById(It.IsAny<int>())).Returns(client);
            var expectedComment = comment;

            var commentActual = new Comment()
            {
                Text = "very cool",
                Rating = 5,
                Date = DateTime.Now,
                Psychologist = new Psychologist(),
                Client = new Client()
            };
            var psychologistId = 1;
            _claims = new()
            {
                Email = "tests@mail.ru"
            };
            //when

            //then
            Assert.Throws<Exceptions.AccessException>(() => _sut.AddCommentToPsyhologist(commentActual, psychologistId, _claims));
        }

        [Test]
        public void AddPsychologist_ValidRequestPassed_ReturnId()
        {
            //given
            _psychologistsRepositoryMock.Setup(c => c.AddPsychologist(It.IsAny<Psychologist>()))
                .Returns(1);
            var expectedId = 1;
            var psych = new Psychologist();
            //when
            var actual = _sut.AddPsychologist(psych);
            //then
            Assert.AreEqual(expectedId, actual);
            _psychologistsRepositoryMock.Verify(c => c.AddPsychologist(psych), Times.Once());
        }

        [Test]
        public void AddPsychologist_EmailIsNotUnique_ThrowUniquenessException()
        {
            //given
            var psychologist = new Psychologist() { Email = "lala@.ru" };
            var newPsych = new Psychologist()
            {
                Email = "lala@.ru"
            };
            _psychologistsRepositoryMock.Setup(c => c.GetPsychologistByEmail("lala@.ru")).Returns(psychologist);
            //when
            //then
            Assert.Throws<Exceptions.UniquenessException>(() => _sut.AddPsychologist(newPsych));
            _psychologistsRepositoryMock.Verify(c => c.AddPsychologist(It.IsAny<Psychologist>()), Times.Never());
        }

        [Test]
        public void DeletePsychologist_ValidRequestPassed_DeletePsychologist()
        {
            //given
            _claims = new()
            {
                Email = "test@mail.ru"
            };
            var psychologist = new Psychologist()
            {
                Email= "test@mail.ru"
            };
            _sut.AddPsychologist(psychologist);
            _psychologistsRepositoryMock.Setup(c => c.DeletePsychologist(psychologist.Id));
            //when
            var actual = _sut.GetPsychologist(psychologist.Id, _claims);
            //then
            Assert.That(actual, Is.Null);
        }

        [Test]
        public void DeletePsychologist_PsychologistNotFound_ThrowException()
        {
            //given
            _claims = new();
            var fakeId = 2;
            _psychologistsRepositoryMock.Setup(o => o.DeletePsychologist(fakeId));
            //when
            //then
            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.DeletePsychologist(fakeId, _claims));
            _psychologistsRepositoryMock.Verify(c => c.DeletePsychologist(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void DeletePsychologist_InvalidRequestPassed_DeletePsychologist()
        {
            //given
            _claims = new()
            {
                Email = "test@mail.ru"
            };
            var psychologist = new Psychologist()
            {
                Id = 2,
                Email = "fake@mail.ru"
            };
            _psychologistsRepositoryMock.Setup(p => p.GetPsychologist(It.IsAny<int>())).Returns(psychologist);
            _sut.AddPsychologist(psychologist);
            var result=_psychologistsRepositoryMock.Setup(c => c.DeletePsychologist(psychologist.Id));
            //when
            //then
            Assert.Throws<Exceptions.AccessException>(() => _sut.DeletePsychologist(psychologist.Id, _claims));
        }

        [Test]
        public void GetAllPsychologists_ValidRequestPassed_PsychologistsReceived()
        {
            //given
            var psychologists = new List<Psychologist>()
                {
                    new Psychologist()
                    {
                        Name = "Robert",
                        Comments = new List<Comment>() { new Comment { Text="Nice"} }
                    },
                    new Psychologist()
                    {
                        Name = "Tanya",
                        IsDeleted = true
                    }
                };
            _claims = new();
            _psychologistsRepositoryMock.Setup(o => o.GetAllPsychologists()).Returns(psychologists);
            //when
            var actual = _sut.GetAllPsychologists(_claims);
            //then
            Assert.NotNull(actual);
            Assert.True(actual.GetType() == typeof(List<Psychologist>));
            Assert.NotNull(actual[0].Comments);
            Assert.AreEqual(actual[0].Problems, null);
            Assert.True(actual[1].IsDeleted == true);
            _psychologistsRepositoryMock.Verify(c => c.GetAllPsychologists(), Times.Once);
        }


        [Test]
        public void GetCommentsByPsychologistId_ValidRequestPassed()
        {
            //given

            var psychologist = new Psychologist()
            {
                Email= "test@mail.ru",
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        Rating=5,
                        Date= DateTime.Now,
                        Text = "Nice"
                    },
                    new Comment
                    {
                        Rating=5,
                        Text = "Bad",
                        Date= DateTime.Now,
                        IsDeleted = true
                    },
                    new Comment
                    {
                        Rating=5,
                        Date= DateTime.Now,
                        Text = "Horrible",
                    }
                }
            };
            _claims = new()
            {
                Email= "test@mail.ru"
            };
            _psychologistsRepositoryMock.Setup(o => o.GetPsychologist(psychologist.Id)).Returns(psychologist);
            _sut.AddPsychologist(psychologist);
            _psychologistsRepositoryMock.Setup(o => o.GetCommentsByPsychologistId(psychologist.Id)).Returns(psychologist.Comments);
            var expected = psychologist.Comments;
            //when
            var actual = _sut.GetCommentsByPsychologistId(psychologist.Id, _claims);

            //then
            expected.Should().BeEquivalentTo(actual);
            Assert.True(actual[1].IsDeleted == true);
            _psychologistsRepositoryMock.Verify(c => c.GetCommentsByPsychologistId(psychologist.Id), Times.Once);
        }

        [Test]
        public void GetCommentsByPsychologistId_PsychologistIsNull_Exception()
        {
            //given
            var psychologist = new Psychologist()
            {
                IsDeleted = true
            };
            _psychologistsRepositoryMock.Setup(o => o.GetCommentsByPsychologistId(psychologist.Id)).Returns(psychologist.Comments);
            _claims = new() { Email = "test@mail.ru" };
            //when

            //then
            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.GetCommentsByPsychologistId(psychologist.Id, _claims));
        }

        [Test]
        public void GetCommentsByPsychologistId_InvalidRequest_AccessException()
        {
            //given
            var psychologist = new Psychologist()
            {
                Id=1
            };
            _psychologistsRepositoryMock.Setup(p => p.GetPsychologist(It.IsAny<int>())).Returns(psychologist);
            _psychologistsRepositoryMock.Setup(o => o.GetCommentsByPsychologistId(psychologist.Id)).Returns(psychologist.Comments);
            _claims = new() { Id = 2, Role=Role.Psychologist.ToString()};
            
            //when

            //then
            Assert.Throws<Exceptions.AccessException>(() => _sut.GetCommentsByPsychologistId(psychologist.Id, _claims));
        }

        [Test]
        public void GetOrdersByPsychologistId_ValidRequestPassed_ReturnOrders()
        {
            //given
            var psychologist = new Psychologist()
            {
                Id = 1,
                Orders = new List<Order>
                {
                    new Order
                    {
                        Client = new Client(),
                        Cost=1000
                    },
                    new Order
                    {
                        Client = new Client(),
                        Cost=500
                    }
                }
            };
            _claims = new()
            {
                Id=1
            };
            _psychologistsRepositoryMock.Setup(o => o.GetPsychologist(psychologist.Id)).Returns(psychologist);
            _sut.AddPsychologist(psychologist);
            _psychologistsRepositoryMock.Setup(o => o.GetOrdersByPsychologistsId(psychologist.Id)).Returns(psychologist.Orders);
            var expected = psychologist.Orders;

            //when
            var actual = _sut.GetOrdersByPsychologistId(psychologist.Id, _claims);

            //then
            expected.Should().BeEquivalentTo(actual);
        }

        [Test]
        public void GetOrdersByPsychologistId_PsychologistIsNull_Exception()
        {
            //given
            var psychologist = new Psychologist()
            {
                IsDeleted = true
            };
            _claims = new()
            {
                Id = 1
            };
            _psychologistsRepositoryMock.Setup(o => o.GetOrdersByPsychologistsId(psychologist.Id)).Returns(psychologist.Orders);
            //when

            //then
            Assert.Throws<Exceptions.EntityNotFoundException>(() => _sut.GetOrdersByPsychologistId(psychologist.Id, _claims));
        }

        [Test]
        public void GetOrdersByPsychologistId_InvalidRequest_AccessException()
        {
            //given
            var psychologist = new Psychologist()
            {
                Id = 1
            };
            _psychologistsRepositoryMock.Setup(p => p.GetPsychologist(It.IsAny<int>())).Returns(psychologist);
            _psychologistsRepositoryMock.Setup(o => o.GetOrdersByPsychologistsId(psychologist.Id)).Returns(psychologist.Orders);
            _claims = new() { Id = 2, Role = Role.Psychologist.ToString() };

            //when

            //then
            Assert.Throws<Exceptions.AccessException>(() => _sut.GetOrdersByPsychologistId(psychologist.Id, _claims));
        }

        [Test]
        public void UpdatePsychologist_ValidRequestPassed_UpdatePsycho()
        {
            //given
            var psychologist = new Psychologist()
            {
                Id = 2,
                Name = "лял",
                LastName = "пвфа",
                Patronymic = "ПВАПВА",
                Gender = Gender.Male,
                Phone = "85884859",
                Educations = new List<Education> { new Education { EducationData = "2020-12-12", IsDeleted = false } },
                CheckStatus = CheckStatus.Completed,
                Email = "ros@fja.com",
                PasportData = "23146456",
                Price = 2000,
                Problems = new List<Problem> { new Problem { ProblemName = "ds", IsDeleted = false } },
                TherapyMethods = new List<TherapyMethod> { new TherapyMethod { Method = "therapy lal", IsDeleted = false } },
                WorkExperience = 10,
                BirthDate = DateTime.Parse("1210 - 12 - 12"),
                Password = "12334534"
            };
            var newPsychologist = new Psychologist()
            {
                Price = 1000
            };

            _psychologistsRepositoryMock.Setup(o => o.UpdatePsychologist(newPsychologist, psychologist.Id));
            _claims = new() { Id = 2, Role = Role.Psychologist.ToString() };
            //when
            _sut.UpdatePsychologist(newPsychologist, psychologist.Id, _claims);

            //then
            _psychologistsRepositoryMock.Verify(c => c.GetPsychologist(It.IsAny<int>()), Times.Exactly(1));
            _psychologistsRepositoryMock.Verify(o => o.UpdatePsychologist(It.IsAny<Psychologist>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void UpdatePsychologist_InvalidRequestPassed_AccessException()
        {
            //given
            var psychologist = new Psychologist()
            {
                Id = 1,
                Name = "лял",
                LastName = "пвфа",
                Patronymic = "ПВАПВА",
                Gender = Gender.Male,
                Phone = "85884859",
                Educations = new List<Education> { new Education { EducationData = "2020-12-12", IsDeleted = false } },
                CheckStatus = CheckStatus.Completed,
                Email = "ros@fja.com",
                PasportData = "23146456",
                Price = 2000,
                Problems = new List<Problem> { new Problem { ProblemName = "ds", IsDeleted = false } },
                TherapyMethods = new List<TherapyMethod> { new TherapyMethod { Method = "therapy lal", IsDeleted = false } },
                WorkExperience = 10,
                BirthDate = DateTime.Parse("1210 - 12 - 12"),
                Password = "12334534"
            };
            var newPsychologist = new Psychologist()
            {
                Price = 1000
            };

            _claims = new() { Id = 2, Role = Role.Psychologist.ToString() };
            //when
            //then
            Assert.Throws<Exceptions.AccessException>(() => _sut.UpdatePsychologist(newPsychologist, psychologist.Id, _claims));
  
        }
    }
    }
