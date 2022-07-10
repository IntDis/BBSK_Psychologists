﻿using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psychologists.Tests.ModelControllerSource
{
    public class PsychologistUpdateNegativeTestSource : IEnumerable
    {
        private static UpdatePsychologistRequest ReturnModelOfUpdate()
        {
            return new UpdatePsychologistRequest()
            {
                Name = "лял",
                LastName = "пвфа",
                Patronymic = "ПВАПВА",
                gender = BBSK_Psycho.Enums.Gender.Male,
                Phone = "85884859",
                Education = new List<string> { "2013 - воврварараар; Dev Education", "sg osgj sopj r" },
                checkStatus = BBSK_Psycho.Enums.CheckStatus.Completed,
                Email = "rosgdsfg@fja.com",
                PasportData = "23146456",
                Price = 2000,
                Problems = new List<string> { "dfsdf", "dasd" },
                TherapyMethods = new List<string> { "dasda", "asd" },
                WorkExperience = 10,
                BirthDate = DateTime.Parse("1210 - 12 - 12"),
                Password = "123645643"
            };
        }


        public IEnumerator GetEnumerator()
        {
            var requestWithInvalidPassword = ReturnModelOfUpdate();
            requestWithInvalidPassword.Password = "123";
            yield return new object[]
            {
            requestWithInvalidPassword,
            ApiErrorMessage.PasswordLengthIsLessThanAllowed
            };

            var requestWithInvalidName = ReturnModelOfUpdate();
            requestWithInvalidName.Name = "";
            yield return new object[]
            {
               requestWithInvalidName,
            ApiErrorMessage.NameIsRequired
            };

            var requestWithRequiredPassword = ReturnModelOfUpdate();
            requestWithRequiredPassword.Password = "";
            yield return new object[]
            {
             requestWithRequiredPassword,
            ApiErrorMessage.PasswordIsRequire
            };

            var requestWithRequiredEmail = ReturnModelOfUpdate();
            requestWithRequiredEmail.Email = "";
            yield return new object[]
            {
            requestWithRequiredEmail,
            ApiErrorMessage.EmailIsRequire
            };


            var requestWithRequiredLastName = ReturnModelOfUpdate();
            requestWithRequiredLastName.LastName = "";
            yield return new object[]
            {
            requestWithRequiredLastName,
            ApiErrorMessage.LastNameIsRequired
            };


            var requestWithRequiredPatronymic = ReturnModelOfUpdate();
            requestWithRequiredPatronymic.Patronymic = "";
            yield return new object[]
            {
            requestWithRequiredPatronymic,
            ApiErrorMessage.PatronymicIsRequired
            };

            var requestWithRequiredPhoneNumber = ReturnModelOfUpdate();
            requestWithRequiredPhoneNumber.Phone = "";
            yield return new object[]
            {
            requestWithRequiredPhoneNumber,
            ApiErrorMessage.PhoneNumberIsRequired
            };

            var requestWithRequiredEducation = ReturnModelOfUpdate();
            requestWithRequiredEducation.Education = null;
            yield return new object[]
            {
            requestWithRequiredEducation,
            ApiErrorMessage.EducationIsRequired
            };

            var requestWithRequiredTherapyMethods = ReturnModelOfUpdate();
            requestWithRequiredTherapyMethods.TherapyMethods = null;
            yield return new object[]
            {
            requestWithRequiredTherapyMethods,
            ApiErrorMessage.TherapyMethodsIsRequired
            };

            var requestWithRequiredProblems = ReturnModelOfUpdate();
            requestWithRequiredProblems.Problems = null;
            yield return new object[]
            {
            requestWithRequiredProblems,
            ApiErrorMessage.ProblemsIsRequired
            };

        }
    }
}