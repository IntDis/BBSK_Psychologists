﻿using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.BusinessLayer.Services.Validators;
﻿using BBSK_Psycho.BusinessLayer.Services.Helpers;
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
        private Mock<IOrdersRepository> _ordersRepositoryMock;
        private Mock<IClientsRepository> _clientsRepositoryMock;
        private IPsychologistsValidator _psychologistsValidator;

        private  ISearchByFilter _searchByFilter;
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
            _searchByFilter = new SearchByFilter();
            _psychologistsValidator = new PsychologistsValidator(_psychologistsRepositoryMock.Object);
            _sut = new PsychologistService(_psychologistsValidator,
                                           _psychologistsRepositoryMock.Object, 
                                           _clientsRepositoryMock.Object, 
                                           _ordersRepositoryMock.Object,
                                           _searchByFilter);
        }

        [Test]
        public async Task AddCommentToPsychologist_WhenPsychologistIsNotNull_ReturnComment()
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
            var order = new Order();
            var client = new Client()
            {
                Email = "test@mail.ru"
            };
            var commentActual = new Comment()
            {
                Text = "very cool",
                Rating = 5,
                Date = DateTime.Now,
                Psychologist = new Psychologist(),
                Client = new Client()
            };
            _ordersRepositoryMock.Setup(o => o.GetOrderByPsychIdAndClientId(It.Is<int>(i => i == commentActual.Psychologist.Id), It.Is<int>(i => i == commentActual.Client.Id))).ReturnsAsync(order);
            _psychologistsRepositoryMock.Setup(c => c.AddCommentToPsyhologist(It.Is<Comment>(c => c.Id == comment.Id && c.Text == comment.Text), (It.Is<int>(i => i == commentActual.Psychologist.Id))))
                .ReturnsAsync(comment);
            _clientsRepositoryMock.Setup(c => c.GetClientById(It.Is<int>(i => i == commentActual.Client.Id))).ReturnsAsync(client);
            var expectedComment = comment;


            _claims = new()
            {
                Email = "test@mail.ru"
            };
            //when
            var actual = await _sut.AddCommentToPsyhologist(commentActual, commentActual.Id, _claims);

            //then
            Assert.AreEqual(expectedComment.Id, actual);
            _psychologistsRepositoryMock.Verify(c => c.AddCommentToPsyhologist(commentActual, commentActual.Id), Times.Once);
        }

        [Test]
        public async Task AddCommentToPsychologist_InValidClientId_ReturnEntityNotFoundException()
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

            _ordersRepositoryMock.Setup(o => o.GetOrderByPsychIdAndClientId(It.Is<int>(p => p == comment.Psychologist.Id), It.Is<int>(c => c == comment.Client.Id))).ReturnsAsync((Order?)null);
            //when
            //then
            Assert.ThrowsAsync<Exceptions.EntityNotFoundException>(() => _sut.AddCommentToPsyhologist(comment, psychologistId, _claims));
        }


        [Test]
        public async Task AddCommentToPsychologist_CommonOrderIsNotFound_ReturnEntityNotFoundException()
        {
            //given
            var comment = new Comment();
            var order = new Order();
            var client = new Client()
            {
                Email = "test@mail.ru"
            };
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
            Assert.ThrowsAsync<Exceptions.EntityNotFoundException>(() => _sut.AddCommentToPsyhologist(commentActual, psychologistId, _claims));
        }

        [Test]
        public async Task AddCommentToPsychologist_InvalidRolePassed_ReturnAccessdenied()
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
            var order = new Order();
            var client = new Client()
            {
                Email = "test@mail.ru"
            };
            var commentActual = new Comment()
            {
                Text = "very cool",
                Rating = 5,
                Date = DateTime.Now,
                Psychologist = new Psychologist(),
                Client = new Client()
            };
            _ordersRepositoryMock.Setup(o => o.GetOrderByPsychIdAndClientId(It.Is<int>(i => i == commentActual.Psychologist.Id), It.Is<int>(i => i == commentActual.Client.Id))).ReturnsAsync(order);

            _clientsRepositoryMock.Setup(c => c.GetClientById(It.Is<int>(i => i == commentActual.Client.Id))).ReturnsAsync(client);
            var expectedComment = comment;


            var psychologistId = 1;
            _claims = new()
            {
                Email = "tests@mail.ru"
            };
            //when

            //then
            Assert.ThrowsAsync<Exceptions.AccessException>(() => _sut.AddCommentToPsyhologist(commentActual, psychologistId, _claims));
        }

        [Test]
        public async Task AddPsychologist_ValidRequestPassed_ReturnId()
        {
            //given
            var psych = new Psychologist()
            {
                Email = "lalrt@o.ru",
                Password = "аоы48аыо"
            };
            _psychologistsRepositoryMock.Setup(c => c.AddPsychologist(It.Is<Psychologist>(p => p.Id == psych.Id)))
                .ReturnsAsync(1);
            var expectedId = 1;

            //when
            var actual = await _sut.AddPsychologist(psych);
            //then
            Assert.AreEqual(expectedId, actual);
            _psychologistsRepositoryMock.Verify(c => c.AddPsychologist(psych), Times.Once());
        }

        [Test]
        public void AddPsychologist_EmailIsNotUnique_ThrowUniquenessException()
        {
            //given
            var psychologist = new Psychologist() { Email = "lala@o.ru", Password = "аоы48аыо" };
            var newPsych = new Psychologist()
            {
                Email = "lala@o.ru",
                Password = "аоы48аыо"
            };
            _psychologistsRepositoryMock.Setup(c => c.GetPsychologistByEmail("lala@o.ru")).ReturnsAsync(newPsych);
            //when
            //then
            Assert.ThrowsAsync<Exceptions.UniquenessException>(() => _sut.AddPsychologist(newPsych));
            _psychologistsRepositoryMock.Verify(c => c.AddPsychologist(It.IsAny<Psychologist>()), Times.Never());
        }

        [Test]
        public void DeletePsychologist_WhenPsychologistIsNotNullAndHasAccess_DeletePsychologist()
        {
            //given
            _claims = new()
            {
                Id = 1
            };
            var psychologist = new Psychologist()
            {
                Id = 1
            };

            _psychologistsRepositoryMock.Setup(c => c.DeletePsychologist(psychologist.Id));
            _psychologistsRepositoryMock.Setup(p => p.GetPsychologist(psychologist.Id)).ReturnsAsync(psychologist);
            //when
            _sut.DeletePsychologist(psychologist.Id, _claims);
            //then
            _psychologistsRepositoryMock.Verify(c => c.DeletePsychologist(psychologist.Id), Times.Once());
        }

        [Test]
        public void DeletePsychologist_PsychologistNotFound_ThrowExceptionEntityNotFound()
        {
            //given
            _claims = new();
            var fakeId = 2;
            _psychologistsRepositoryMock.Setup(o => o.DeletePsychologist(fakeId));
            //when
            //then
            Assert.ThrowsAsync<Exceptions.EntityNotFoundException>(() => _sut.DeletePsychologist(fakeId, _claims));
            _psychologistsRepositoryMock.Verify(c => c.DeletePsychologist(It.IsAny<int>()), Times.Never);
        }


        [Test]
        public void DeletePsychologist_PsychologistHasNotAccess_ReturnAccessDenied()
        {
            //given
            _claims = new()
            {
                Id = 1
            };
            var psychologist = new Psychologist()
            {
                Id = 2
            };

            _psychologistsRepositoryMock.Setup(c => c.DeletePsychologist(psychologist.Id));
            _psychologistsRepositoryMock.Setup(p => p.GetPsychologist(psychologist.Id)).ReturnsAsync(psychologist);
            //when
            _sut.DeletePsychologist(psychologist.Id, _claims);
            //then
            _psychologistsRepositoryMock.Verify(c => c.DeletePsychologist(psychologist.Id), Times.Once());
        }


        [Test]
        public async Task GetAllPsychologists_WhenMethodIsCalled_PsychologistsReceived()
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
            _psychologistsRepositoryMock.Setup(o => o.GetAllPsychologists()).ReturnsAsync(psychologists);
            //when
            var actual = await _sut.GetAllPsychologists(_claims);
            //then
            Assert.NotNull(actual);
            Assert.True(actual.GetType() == typeof(List<Psychologist>));
            Assert.NotNull(actual[0].Comments);
            Assert.Null(actual[0].Problems);
            Assert.True(actual[1].IsDeleted == true);
            _psychologistsRepositoryMock.Verify(c => c.GetAllPsychologists(), Times.Once);
        }

        [Test]
        public async Task GetCommentsByPsychologistId_WhenThereIsAccess_ReturnComments()
        {
            //given

            var psychologist = new Psychologist()
            {
                Email = "test@mail.ru",
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
                Email = "test@mail.ru"
            };
            _psychologistsRepositoryMock.Setup(o => o.GetPsychologist(psychologist.Id)).ReturnsAsync(psychologist);

            _psychologistsRepositoryMock.Setup(o => o.GetCommentsByPsychologistId(psychologist.Id)).ReturnsAsync(psychologist.Comments);
            var expected = psychologist.Comments;
            //when
            var actual = await _sut.GetCommentsByPsychologistId(psychologist.Id, _claims);

            //then
            expected.Should().BeEquivalentTo(actual);
            _psychologistsRepositoryMock.Verify(c => c.GetCommentsByPsychologistId(psychologist.Id), Times.Once);
        }

        [Test]
        public void GetCommentsByPsychologistId_PsychologistIsNull_ReturnEntityNotFoundException()
        {
            //given
            var psychologist = new Psychologist()
            {
                IsDeleted = true
            };
            _psychologistsRepositoryMock.Setup(o => o.GetCommentsByPsychologistId(psychologist.Id)).ReturnsAsync(psychologist.Comments);
            _claims = new() { Email = "test@mail.ru" };
            //when

            //then
            Assert.ThrowsAsync<Exceptions.EntityNotFoundException>(() => _sut.GetCommentsByPsychologistId(psychologist.Id, _claims));
        }

        [Test]
        public void GetCommentsByPsychologistId_WhenAccessIsUnavailable_ReturnExceptionAccessDenied()
        {
            //given
            var psychologist = new Psychologist()
            {
                Id = 1
            };
            _psychologistsRepositoryMock.Setup(p => p.GetPsychologist(It.Is<int>(i => i == psychologist.Id))).ReturnsAsync(psychologist);
            _psychologistsRepositoryMock.Setup(o => o.GetCommentsByPsychologistId(psychologist.Id)).ReturnsAsync(psychologist.Comments);
            _claims = new() { Id = 2, Role = Role.Psychologist };

            //when

            //then
            Assert.ThrowsAsync<Exceptions.AccessException>(() => _sut.GetCommentsByPsychologistId(psychologist.Id, _claims));
        }

        [Test]
        public async Task GetOrdersByPsychologistId_WhenThereIsAccess_ReturnOrders()
        {
            //given
            var psychologist = new Psychologist()
            {
                Id = 1,
                Email = "sad@asd.aa",
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
                Id = 1
            };
            _psychologistsRepositoryMock.Setup(o => o.GetPsychologist(psychologist.Id)).ReturnsAsync(psychologist);

            _psychologistsRepositoryMock.Setup(o => o.GetOrdersByPsychologistsId(psychologist.Id)).ReturnsAsync(psychologist.Orders);
            var expected = psychologist.Orders;

            //when
            var actual = await _sut.GetOrdersByPsychologistId(psychologist.Id, _claims);

            //then
            expected.Should().BeEquivalentTo(actual);
        }

        [Test]
        public void GetOrdersByPsychologistId_PsychologistIsNull_ReturnEntityNotFoundException()
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
            _psychologistsRepositoryMock.Setup(o => o.GetOrdersByPsychologistsId(psychologist.Id)).ReturnsAsync(psychologist.Orders);
            //when

            //then
            Assert.ThrowsAsync<Exceptions.EntityNotFoundException>(() => _sut.GetOrdersByPsychologistId(psychologist.Id, _claims));
        }

        [Test]
        public void GetOrdersByPsychologistId_WhenAccessUnavailable_ReturnAccessException()
        {
            //given
            var psychologist = new Psychologist()
            {
                Id = 1
            };
            _psychologistsRepositoryMock.Setup(p => p.GetPsychologist(It.Is<int>(i => i == psychologist.Id))).ReturnsAsync(psychologist);
            _psychologistsRepositoryMock.Setup(o => o.GetOrdersByPsychologistsId(psychologist.Id)).ReturnsAsync(psychologist.Orders);
            _claims = new() { Id = 2, Role = Role.Psychologist };

            //when

            //then
            Assert.ThrowsAsync<Exceptions.AccessException>(() => _sut.GetOrdersByPsychologistId(psychologist.Id, _claims));
        }

        [Test]
        public void UpdatePsychologist_WhenThereIsAccess_UpdatePsycho()
        {
            int id = 2;
            //give
            var newPsychologist = new Psychologist()
            {
                Id = 2,
                Name = "Grigoriy",
                LastName = "Grigorievich",
                Patronymic = "Grigoriev",
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

            var expectedPsychologist = new Psychologist()
            {
                Id = 2,
                Name = "Grigoriy",
                LastName = "Grigorievich",
                Patronymic = "Grigoriev",
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

            _psychologistsRepositoryMock.Setup(p => p.GetPsychologist(id));
            _psychologistsRepositoryMock.Setup(o => o.UpdatePsychologist(newPsychologist, id));
            _claims = new() { Id = 2, Role = Role.Psychologist };
            //when
            _sut.UpdatePsychologist(newPsychologist, id, _claims);

            //then
            _psychologistsRepositoryMock.Verify(o => o.GetPsychologist(It.Is<int>(p => p == id)), Times.Exactly(1));
            _psychologistsRepositoryMock.Verify(p => p.UpdatePsychologist(It.Is<Psychologist>(p => p.Price == expectedPsychologist.Price
             && p.Id == expectedPsychologist.Id
             && p.CheckStatus == expectedPsychologist.CheckStatus
             && p.PasportData == expectedPsychologist.PasportData
             && p.Patronymic == expectedPsychologist.Patronymic
             && p.Email == expectedPsychologist.Email
             && p.WorkExperience == expectedPsychologist.WorkExperience
             && p.CheckStatus == expectedPsychologist.CheckStatus
             && p.Name == expectedPsychologist.Name
             && p.LastName == expectedPsychologist.LastName
             && p.Gender == expectedPsychologist.Gender), It.Is<int>(i => i == id)), Times.Once);
        }

        [Test]
        public void UpdatePsychologist_WhenAccessDenied_ReturnAccessException()
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

            _claims = new() { Id = 2, Role = Role.Psychologist };
            //when
            //then
            Assert.ThrowsAsync<Exceptions.AccessException>(() =>  _sut.UpdatePsychologist(newPsychologist, psychologist.Id, _claims));

        }

        [Test]
        public async Task GetPsychologistsByParametrs_ValidRequestParametrsGenderIsEmptyPriceAssending_PsychologistsSelectionOfParametersReceived()
        {
            //given
            var expectedPrice = Price.Ascending;
            Gender? expectedGender = null;
            var expectedProblems = new List<int>() { 1, 5 };

            var psychologists = new List<Psychologist>()
                {
                    new Psychologist()
                    {
                        Id = 1,
                        Name = "Петр",
                        LastName = "Петров",
                        Patronymic = "Петькович",
                        Gender = Gender.Male,
                        Phone = "85884859",
                        Educations = new List<Education> { new Education { EducationData = "2020-12-12", IsDeleted = false } },
                        CheckStatus = CheckStatus.Completed,
                        Email = "ros@fja.com",
                        PasportData = "23146456",
                        Price = 20000,
                        Problems = new List<Problem> {
                            new Problem
                            {
                                Id = 1,
                                ProblemName = "Насилие", 
                                IsDeleted = false
                            },
                        },
                        TherapyMethods = new List<TherapyMethod> { new TherapyMethod { Method = "Душевная беседа", IsDeleted = false } },
                        WorkExperience = 10,
                        BirthDate = DateTime.Parse("1210 - 12 - 12"),
                        Password = "12334534"
                    },
                    new Psychologist()
                    {
                        Id = 2,
                        Name = "Валера",
                        LastName = "Валеров",
                        Patronymic = "Валерьевич",
                        Gender = Gender.Male,
                        Phone = "85884859",
                        Educations = new List<Education> { new Education { EducationData = "2020-12-12", IsDeleted = false } },
                        CheckStatus = CheckStatus.Completed,
                        Email = "ros@fja.com",
                        PasportData = "23146456",
                        Price = 3000,
                        Problems = new List<Problem> { new Problem { Id = 2, ProblemName = "Зависимость", IsDeleted = false } },
                        TherapyMethods = new List<TherapyMethod> { new TherapyMethod { Method = "Гипноз", IsDeleted = false } },
                        WorkExperience = 10,
                        BirthDate = DateTime.Parse("1210 - 12 - 12"),
                        Password = "12334534"
                    },
                    new Psychologist()
                    {
                        Id = 3,
                        Name = "Зинаида",
                        LastName = "Зиновна",
                        Patronymic = "Александровна",
                        Gender = Gender.Famale,
                        Phone = "85884859",
                        Educations = new List<Education> { new Education { EducationData = "2020-12-12", IsDeleted = false } },
                        CheckStatus = CheckStatus.Completed,
                        Email = "ros@fja.com",
                        PasportData = "23146456",
                        Price = 200,
                        Problems = new List<Problem>
                        {
                            new Problem
                            {
                                Id = 1,
                                ProblemName = "Насилие",
                                IsDeleted = false
                            },
                            new Problem
                            {
                                Id = 5,
                                ProblemName = "Комплексы",
                                IsDeleted = false
                            }
                        },
                        TherapyMethods = new List<TherapyMethod> { new TherapyMethod { Method = "Гештальд", IsDeleted = false } },
                        WorkExperience = 10,
                        BirthDate = DateTime.Parse("1210 - 12 - 12"),
                        Password = "12334534"
                    },
                    new Psychologist()
                    {
                        Id = 4,
                        Name = "Гена",
                        LastName = "Гена",
                        Patronymic = "Гена",
                        Gender = Gender.Famale,
                        Phone = "85884859",
                        Educations = new List<Education> { new Education { EducationData = "2020-12-12", IsDeleted = false } },
                        CheckStatus = CheckStatus.Completed,
                        Email = "ros@fja.com",
                        PasportData = "23146456",
                        Price = 200,
                        Problems = new List<Problem>
                        {
                            new Problem
                            {
                                Id = 1,
                                ProblemName = "Насилие",
                                IsDeleted = false
                            },
                            new Problem
                            {
                                Id = 5,
                                ProblemName = "Комплексы",
                                IsDeleted = false
                            }
                        },
                        TherapyMethods = new List<TherapyMethod> { new TherapyMethod { Method = "Гештальд", IsDeleted = false } },
                        WorkExperience = 10,
                        BirthDate = DateTime.Parse("1210 - 12 - 12"),
                        Password = "12334534"
                    }

                };

            _psychologistsRepositoryMock.Setup(o => o.GetAllPsychologistsWithFullInformations()).ReturnsAsync(psychologists);
            //when
            var actual = await _sut.GetPsychologistsByFilter(expectedPrice, expectedProblems, expectedGender);
            //then
            Assert.NotNull(actual);
            Assert.AreEqual(actual[0], psychologists[2]);
            Assert.AreEqual(actual[1], psychologists[3]);
            Assert.AreEqual(actual[2], psychologists[0]);

            _psychologistsRepositoryMock.Verify(c => c.GetAllPsychologistsWithFullInformations(), Times.Once);
        }

        [Test]
        public async Task GetPsychologistsByParametrs_ValidRequestParametrsGenderFamalePriceDescending_PsychologistsSelectionOfParametersReceived()
        {
            //given
            var expectedPrice = Price.Descending;
            Gender? expectedGender = Gender.Famale;
            var expectedProblems = new List<int>() { 1, 5 };

            var psychologists = new List<Psychologist>()
                {
                    new Psychologist()
                    {
                        Id = 1,
                        Name = "Жанна",
                        LastName = "Петрова",
                        Patronymic = "Алек",
                        Gender = Gender.Famale,
                        Phone = "85884859",
                        Educations = new List<Education> { new Education { EducationData = "2020-12-12", IsDeleted = false } },
                        CheckStatus = CheckStatus.Completed,
                        Email = "ros@fja.com",
                        PasportData = "23146456",
                        Price = 20000,
                        Problems = new List<Problem> {
                            new Problem
                            {
                                Id = 1,
                                ProblemName = "Насилие",
                                IsDeleted = false
                            },
                        },
                        TherapyMethods = new List<TherapyMethod> { new TherapyMethod { Method = "Душевная беседа", IsDeleted = false } },
                        WorkExperience = 10,
                        BirthDate = DateTime.Parse("1210 - 12 - 12"),
                        Password = "12334534"
                    },
                    new Psychologist()
                    {
                        Id = 2,
                        Name = "Валера",
                        LastName = "Валеров",
                        Patronymic = "Валерьевич",
                        Gender = Gender.Male,
                        Phone = "85884859",
                        Educations = new List<Education> { new Education { EducationData = "2020-12-12", IsDeleted = false } },
                        CheckStatus = CheckStatus.Completed,
                        Email = "ros@fja.com",
                        PasportData = "23146456",
                        Price = 3000,
                        Problems = new List<Problem> { new Problem { Id = 2, ProblemName = "Зависимость", IsDeleted = false } },
                        TherapyMethods = new List<TherapyMethod> { new TherapyMethod { Method = "Гипноз", IsDeleted = false } },
                        WorkExperience = 10,
                        BirthDate = DateTime.Parse("1210 - 12 - 12"),
                        Password = "12334534"
                    },
                    new Psychologist()
                    {
                        Id = 3,
                        Name = "Зинаида",
                        LastName = "Зиновна",
                        Patronymic = "Александровна",
                        Gender = Gender.Famale,
                        Phone = "85884859",
                        Educations = new List<Education> { new Education { EducationData = "2020-12-12", IsDeleted = false } },
                        CheckStatus = CheckStatus.Completed,
                        Email = "ros@fja.com",
                        PasportData = "23146456",
                        Price = 200,
                        Problems = new List<Problem>
                        {
                            new Problem
                            {
                                Id = 1,
                                ProblemName = "Насилие",
                                IsDeleted = false
                            },
                            new Problem
                            {
                                Id = 5,
                                ProblemName = "Комплексы",
                                IsDeleted = false
                            }
                        },
                        TherapyMethods = new List<TherapyMethod> { new TherapyMethod { Method = "Гештальд", IsDeleted = false } },
                        WorkExperience = 10,
                        BirthDate = DateTime.Parse("1210 - 12 - 12"),
                        Password = "12334534"
                    },
                    new Psychologist()
                    {
                        Id = 4,
                        Name = "Гена",
                        LastName = "Гена",
                        Patronymic = "Гена",
                        Gender = Gender.Male,
                        Phone = "85884859",
                        Educations = new List<Education> { new Education { EducationData = "2020-12-12", IsDeleted = false } },
                        CheckStatus = CheckStatus.Completed,
                        Email = "ros@fja.com",
                        PasportData = "23146456",
                        Price = 200,
                        Problems = new List<Problem>
                        {
                            new Problem
                            {
                                Id = 1,
                                ProblemName = "Насилие",
                                IsDeleted = false
                            },
                            new Problem
                            {
                                Id = 5,
                                ProblemName = "Комплексы",
                                IsDeleted = false
                            }
                        },
                        TherapyMethods = new List<TherapyMethod> { new TherapyMethod { Method = "Гештальд", IsDeleted = false } },
                        WorkExperience = 10,
                        BirthDate = DateTime.Parse("1210 - 12 - 12"),
                        Password = "12334534"
                    }

                };

            _psychologistsRepositoryMock.Setup(o => o.GetAllPsychologistsWithFullInformations()).ReturnsAsync(psychologists);
            //when
            var actual = await _sut.GetPsychologistsByFilter(expectedPrice, expectedProblems, expectedGender);
            //then
            Assert.NotNull(actual);
            Assert.AreEqual(actual[0], psychologists[2]);
            Assert.AreEqual(actual[1], psychologists[0]);
            _psychologistsRepositoryMock.Verify(c => c.GetAllPsychologistsWithFullInformations(), Times.Once);
        }
    }
}
