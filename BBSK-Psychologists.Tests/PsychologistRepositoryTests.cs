using BBSK_Psycho.DataLayer;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psychologists.Tests
{
    public class PsychologistRepositoryTests
    {
        private DbContextOptions<BBSK_PsychoContext> _dbContextOptions;
        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<BBSK_PsychoContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }
        //public PsychologistRepositoryTests()
        //{
        //    _dbContextOptions = new DbContextOptionsBuilder<BBSK_PsychoContext>()
        //        .UseInMemoryDatabase(databaseName: "TestDb")
        //        .Options;
        //}
        [Test]
        public void DeletePsychologist_WhenCorrectIdPassed_ThenSoftDeleteApplied()
        {
            //given
            var context = new BBSK_PsychoContext(_dbContextOptions);
            var sut = new PsychologistsRepository(context);

            var psychologist = TestDataPsychologist.GetValidPsychologist();

            context.Psychologists.Add(psychologist);
            context.SaveChanges();

            //when

            sut.DeletePsychologist(psychologist.Id);

            //then
            Assert.True(psychologist.IsDeleted);
        }

        [Test]
        public void GetPsychologist_WhenCorrectIdPassed_ThenGetApplied()
        {
            //given
            var context = new BBSK_PsychoContext(_dbContextOptions);
            var sut = new PsychologistsRepository(context);
            var expected = TestDataPsychologist.GetValidPsychologist();

            context.Psychologists.Add(expected);
            context.SaveChanges();

            //when
            var actual = sut.GetPsychologist(expected.Id);

            //then
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddPsychologist_WhenCorrectIdPassed_ThenReturnId()
        {
            //given
            var context = new BBSK_PsychoContext(_dbContextOptions);
            var sut = new PsychologistsRepository(context);

            var expected = TestDataPsychologist.GetValidPsychologist();


            //when
            var actualId = sut.AddPsychologist(expected);
            var actual = context.Psychologists.Find(actualId);

            //then
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void UpdatePsychologist_WhenCorrectIdPassed_ThenUpdate()
        {
            //given
            var context = new BBSK_PsychoContext(_dbContextOptions);
            var sut = new PsychologistsRepository(context);

            var startPsychologist = TestDataPsychologist.GetValidPsychologist();
            context.Psychologists.Add(startPsychologist);
            context.SaveChanges();

            var modelToUpdate = new Psychologist
            {
                Name = "sfd",
                LastName = "sf",
                Patronymic = "sd",
                Gender = Gender.Famale,
                Phone = "888888889",
                Educations = new List<Education> { new Education { EducationData = "2011-11-10", IsDeleted = false } },
                CheckStatus = CheckStatus.Waiting,
                Email = "urs@fja.com",
                PasportData = "8888456",
                Price = 500,
                Problems = new List<Problem> { new Problem { ProblemName = "hhhh", IsDeleted = false } },
                TherapyMethods = new List<TherapyMethod> { new TherapyMethod { Method = "hdfffff", IsDeleted = false } },
                WorkExperience = 5,
                BirthDate = DateTime.Parse("1870 - 07 - 12"),
                Password = "155545"
            };

            var expected = new Psychologist
            {
                Id = startPsychologist.Id,
                Name = "лял",
                LastName = "пвфа",
                Patronymic = "ПВАПВА",
                Gender = Gender.Famale,
                Phone = "888888889",
                Educations = new List<Education> { new Education { EducationData = "2011-11-10", IsDeleted = false } },
                CheckStatus = CheckStatus.Completed,
                Email = "ros@fja.com",
                PasportData = "23146456",
                Price = 500,
                Problems = new List<Problem> { new Problem { ProblemName = "hhhh", IsDeleted = false } },
                TherapyMethods = new List<TherapyMethod> { new TherapyMethod { Method = "hdfffff", IsDeleted = false } },
                WorkExperience = 10,
                BirthDate = DateTime.Parse("1210 - 12 - 12"),
                Password = "12334534",
                Comments = new List<Comment> { },
                Schedules = new List<Schedule> { }
            };
            //when

            sut.UpdatePsychologist(modelToUpdate, startPsychologist.Id);
            
            var actual = context.Psychologists.Find(startPsychologist.Id);

            //then
            expected.Should().BeEquivalentTo(actual, options =>

               options.Excluding(o => o.Educations)
               .Excluding(o => o.TherapyMethods)
               .Excluding(o => o.Schedules)
               .Excluding(o => o.Problems)

           );

            expected.Educations.Should().BeEquivalentTo(actual.Educations, options =>  options.Excluding(o => o.Id).Excluding(o=>o.Psychologist));
            expected.Problems.Should().BeEquivalentTo(actual.Problems, options => options.Excluding(o => o.Id).Excluding(o => o.Psychologists));
            expected.TherapyMethods.Should().BeEquivalentTo(actual.TherapyMethods, options => options.Excluding(o => o.Id).Excluding(o => o.Psychologists));
        }

        [Test]
        public void GetAllPsychologists_WhenPassed_ThenGetAll()
        {
            //given
            var context = new BBSK_PsychoContext(_dbContextOptions);
            var sut = new PsychologistsRepository(context);

            var psych1 = TestDataPsychologist.GetValidPsychologist();
            var psych2 = TestDataPsychologist.GetValidPsychologist();

            var psych3 = new Psychologist
            {
                Name = "sdfs",
                LastName = "sjfs",
                Patronymic = "sgd",
                Gender = Gender.Male,
                Phone = "44885884859",
                Educations = new List<Education> { new Education { EducationData = "2020-12-12", IsDeleted = false } },
                CheckStatus = CheckStatus.Completed,
                Email = "rosj@fja.com",
                PasportData = "23146456",
                Price = 2000,
                Problems = new List<Problem> { new Problem { ProblemName = "ds", IsDeleted = false } },
                TherapyMethods = new List<TherapyMethod> { new TherapyMethod { Method = "therapy lal", IsDeleted = false } },
                WorkExperience = 10,
                BirthDate = DateTime.Parse("1210 - 12 - 12"),
                Password = "123543",
                IsDeleted=true
            };
            context.Psychologists.Add(psych1);
            context.Psychologists.Add(psych2);
            context.Psychologists.Add(psych3);
            context.SaveChanges();

            //when
            var actual = sut.GetAllPsychologists();

            //then
            Assert.IsNotEmpty(actual);
            Assert.That(actual, Is.Not.Contains(psych3));

        }

        [Test]
        public void AddCommentToPsyhologist_WhenCorrectIdPassed_ThenAddComment()
        {
            //given
            var context = new BBSK_PsychoContext(_dbContextOptions);
            var sut = new PsychologistsRepository(context);

            var psychologist = TestDataPsychologist.GetValidPsychologist();

            var client = new Client
            {
                Name = "Ляляка",
                Email = "fsh@jfks.ru",
                Password = "fijs",
                RegistrationDate = DateTime.Now
            };
            var comment = new Comment
            {
                Text = "fsdjf",
                Rating = 10,
                Date = DateTime.Now,
                Client = client

            };

            
            context.Clients.Add(client);
            context.Psychologists.Add(psychologist);
            context.SaveChanges();
            var actual = sut.AddCommentToPsyhologist(comment, psychologist.Id);
            //when
            var expected = context.Comments.Find(actual.Id);

            //then
            Assert.AreEqual(expected, actual);
        }


        [Test]

        public void GetCommentsByPsychologistId()
        {
            //given
            var context = new BBSK_PsychoContext(_dbContextOptions);
            var sut = new PsychologistsRepository(context);

            var psychologist = TestDataPsychologist.GetValidPsychologist();
            var client = new Client
            {
                Name = "Ляляка",
                Email = "fsh@jfks.ru",
                Password = "fijs",
                RegistrationDate = DateTime.Now
            };

            var comment = new Comment
            {
                Text = "fsdfsdjf",
                Rating = 10,
                Date = DateTime.Now,
                Client = client,
                Psychologist = psychologist
            };
            var comment2 = new Comment
            {
                Text = "fsdfsdjf",
                Rating = 10,
                Date = DateTime.Now,
                Client = client,
                IsDeleted = true,
                Psychologist = psychologist
            };

            context.Comments.Add(comment);
            context.SaveChanges();
            context.Comments.Add(comment2);
            context.SaveChanges();

            //when
            var actual = sut.GetCommentsByPsychologistId(psychologist.Id);
            //var expected = context.Psychologists.(psychologist.Id);
            var expected= new List<Comment> { comment};
            var isContains = actual.Contains(comment2);
            //then
            Assert.AreEqual(expected, actual);
            Assert.IsFalse(isContains);

        }
    }
}
