using BBSK_Psycho.Controllers;
using BBSK_Psycho.DataLayer.Enums;

using BBSK_Psycho.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using BBSK_Psycho.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBSK_Psycho.DataLayer.Repositories;
using Moq;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Repositories.Interfaces;

namespace BBSK_Psychologists.Tests
{
    public class PsychologistControllerTests
{       
        private Mock<IPsychologistsRepository> _repository;

        private PsychologistsController _sut;
        private AddPsychologistRequest psychologistDataAdd = new AddPsychologistRequest
        {
            Name = "лял",
            LastName = "пвфа",
            Patronymic = "ПВАПВА",
            gender = Gender.Male,
            Phone = "85884859",
            Education = new List<string> { "2013 - воврварараар; Dev Education", "sg osgj sopj r" },
            checkStatus = CheckStatus.Completed,
            Email = "ros@fja.com",
            PasportData = "23146456",
            Price = 2000,
            Problems = new List<string> { "dfsdf", "dasd" },
            TherapyMethods = new List<string> { "dasda", "asd" },
            WorkExperience = 10,
            BirthDate = DateTime.Parse("1210 - 12 - 12"),
            Password = "1235345"
        };

        private UpdatePsychologistRequest psychologistData = new UpdatePsychologistRequest
        {
            Name = "лял",
            LastName = "пвфа",
            Patronymic = "ПВАПВА",
            Gender = Gender.Male,
            Phone = "85884859",
            Education = new List<string> { "2013 - воврварараар; Dev Education", "sg osgj sopj r" },
            CheckStatus = CheckStatus.Completed,
            Email = "ros@fja.com",
            PasportData = "23146456",
            Price = 2000,
            Problems = new List<string> { "dfsdf", "dasd" },
            TherapyMethods = new List<string> { "dasda", "asd" },
            WorkExperience = 10,
            BirthDate = DateTime.Parse("1210 - 12 - 12"),
            Password = "1235345"
        };

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IPsychologistsRepository>();
            _sut = new PsychologistsController(_repository.Object);
        }

        [Test]
        public void AddRequestForPsy_ValidRequestPassed_CreatedResultReceived()
        {
            // given

            var request = new CommentRequest
            {
              ClientId=1,
              PsychologistId= 1,
              Text = "kdffk",
              Rating= 1,
              Date= DateTime.Now
            };
            int psId = 2;
            // when
            var actual = _sut.AddCommentToPsyhologist(request, psId);

            // then
            var actualResult = actual.Result as CreatedResult;
            Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);

        }


        [Test]
        public void UpdatePsychologist_NoContentResult()
        {
            // given

            var psychologist = psychologistData;

            // when
            int id = 2;
            var actual = _sut.UpdatePsychologist(psychologist, id);

            // then
            var actualResult = actual as NoContentResult;
            Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

        }

        [Test]
        public void GetPsychologist_ObjectResultPassed()
        {
            //_repository.Setup(r => r.GetPsychologist(It.IsAny<int>()))
            //.Returns(new Psychologist());   
            var clientId = 1;

            _repository.Setup(r => r.GetPsychologist(clientId)).Returns(new Psychologist());
            // when

            var actual = _sut.GetPsychologist(clientId);

            // then

            _repository.Verify(r => r.GetPsychologist(It.IsAny<int>()), Times.Once);
            var actualResult = actual.Result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);

        }

        [Test]
        public void GetAllPsychologist_ObjectResultPassed()
        {
            // when

            var actual = _sut.GetAllPsychologists();

            // then
            var actualResult = actual.Result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);

        }

        [Test]
        public void DeletePsychologist_NoContentResult()
        {
            // given
            int id = 2
            //var psychologist = psychologistData;

            // when
           ;
            var actual = _sut.DeletePsychologist(id);

            // then
            var actualResult = actual as NoContentResult;
            Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

        }

        [Test]
        public void AddPsychologist_ValidRequestPassed_CreatedResultReceived()
        {
            // given

            var psychologist = psychologistDataAdd;

            // when
            var actual = _sut.AddPsychologist(psychologist);

            // then
            var actualResult = actual.Result as CreatedResult;
            Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);

        }
    }
}

