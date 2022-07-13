using BBSK_Psycho.DataLayer;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psychologists.Tests
{
    public class PsychologistRepositoryTests
    {
        private readonly DbContextOptions<BBSK_PsychoContext> _dbContextOptions;

        public PsychologistRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<BBSK_PsychoContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
        }
        [Test]
        public void DeletePsychologist_WhenCorrectIdPassed_ThenSoftDeleteApplied()
        {
            //given
            var context = new BBSK_PsychoContext(_dbContextOptions);
            var sut = new PsychologistsRepository(context);

            var psychologist = new Psychologist
            {
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
            var expected = new Psychologist
            {
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

            context.Psychologists.Add(expected);
            context.SaveChanges();

            //when
            var actual = sut.GetPsychologist(expected.Id);

            //then
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddPsychologist_WhenAddedInDB_ThenReturnId()
        {
            //given
            var context = new BBSK_PsychoContext(_dbContextOptions);
            var sut = new PsychologistsRepository(context);

            var expected = new Psychologist
            {
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
                Password = "1233453"
            };


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

            var startPsycholog = new Psychologist
            {
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
                Password = "123345"
            };
            context.Psychologists.Add(startPsycholog);
            context.SaveChanges();
            var modelToUpdate = new Psychologist
            {
                Name = "sfd",
                LastName = "sf",
                Patronymic = "sd",
                Gender = Gender.Famale,
                Phone = "888888889",
                Educations = new List<Education> { new Education { EducationData = "2020-12-12", IsDeleted = false } },
                CheckStatus = CheckStatus.Waiting,
                Email = "urs@fja.com",
                PasportData = "8888456",
                Price = 500,
                Problems = new List<Problem> { new Problem { ProblemName = "ds", IsDeleted = false } },
                TherapyMethods = new List<TherapyMethod> { new TherapyMethod { Method = "therapy lal", IsDeleted = false } },
                WorkExperience = 5,
                BirthDate = DateTime.Parse("1870 - 07 - 12"),
                Password = "155545"
            };

            //when

           sut.UpdatePsychologist(modelToUpdate, startPsycholog.Id);


            var expected = new Psychologist
            {
                Id=startPsycholog.Id,
                Name = "лял",
                LastName = "пвфа",
                Patronymic = "ПВАПВА",
                Gender = Gender.Famale,
                Phone = "888888889",
                Educations = new List<Education> { new Education { EducationData = "2020-12-12", IsDeleted = false } },
                CheckStatus = CheckStatus.Completed,
                Email = "urs@fja.com",
                PasportData = "23146456",
                Price = 500,
                Problems = new List<Problem> { new Problem { ProblemName = "ds", IsDeleted = false } },
                TherapyMethods = new List<TherapyMethod> { new TherapyMethod { Method = "therapy lal", IsDeleted = false } },
                WorkExperience = 10,
                BirthDate = DateTime.Parse("1210 - 12 - 12"),
                Password = "155545",
                Comments = new List<Comment> { }
            };
            
            
            var actual = context.Psychologists.Find(startPsycholog.Id);

            expected.Should().BeEquivalentTo(actual);

        }

        [Test]
        public void GetAllPsychologists_WhenPassed_ThenGetAll()
        {
            //given
            var context = new BBSK_PsychoContext(_dbContextOptions);
            var sut = new PsychologistsRepository(context);

            var psych1 = new Psychologist
            {
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
                Password = "123345"
            };

            var psych2 = new Psychologist
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
                Password = "123543"
            };
            context.Psychologists.Add(psych1);
            context.Psychologists.Add(psych2);
            context.SaveChanges();

            //when
            var actual = sut.GetAllPsychologists();

            //then
            Assert.IsNotEmpty(actual);

        }

        [Test]
        public void AddCommentToPsyhologist_When_Then()
        {
            //given
            var context = new BBSK_PsychoContext(_dbContextOptions);
            var sut = new PsychologistsRepository(context);

            var psychologist = new Psychologist
            {
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
                Password = "123345"

            };
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
            context.SaveChanges();
            context.Psychologists.Add(psychologist);
            context.SaveChanges();

            var actual = sut.AddCommentToPsyhologist(comment, psychologist.Id);

            //when
            var expected = context.Comments.Find(actual.Id);

            Assert.AreEqual(expected, actual);
        }

        [Test]

        public void GetCommentsByPsychologistId()
        {
            //given
            var context = new BBSK_PsychoContext(_dbContextOptions);
            var sut = new PsychologistsRepository(context);

            var psychologist = new Psychologist
            {
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
                Password = "123345"

            };
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

            context.Comments.Add(comment);
            context.SaveChanges();

            //when
            var actual = sut.GetCommentsByPsychologistId(psychologist.Id);
            var expected = context.Psychologists.Find(psychologist.Id);

            //then
            Assert.AreEqual(expected.Comments, actual);
        }

    }
}
