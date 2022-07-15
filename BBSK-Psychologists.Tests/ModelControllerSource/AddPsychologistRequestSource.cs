using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psychologists.Tests
{
    public class AddPsychologistRequestSource : IEnumerable
    {
        private static AddPsychologistRequest ReturnModelOfAdd()
        {
            return new AddPsychologistRequest()
            {
                Name = "лял",
                LastName = "пвфа",
                Patronymic = "ПВАПВА",
                gender = Gender.Male,
                Phone = "85884859",
                Education = new List<string> { "2013 - воврварараар; Dev Education", "sg osgj sopj r" },
                checkStatus = CheckStatus.Completed,
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
            var requestWithInvalidPasswordAdd = ReturnModelOfAdd();
            requestWithInvalidPasswordAdd.Password = "123";
            yield return new object[]
            {
            requestWithInvalidPasswordAdd,
            ApiErrorMessage.PasswordLengthIsLessThanAllowed
            };

            var requestWithInvalidNameAdd = ReturnModelOfAdd();
            requestWithInvalidNameAdd.Name = "";
            yield return new object[]
            {
               requestWithInvalidNameAdd,
            ApiErrorMessage.NameIsRequired
            };

            var requestWithRequiredPasswordAdd = ReturnModelOfAdd();
            requestWithRequiredPasswordAdd.Password = "";
            yield return new object[]
            {
             requestWithRequiredPasswordAdd,
            ApiErrorMessage.PasswordIsRequired
            };

            var requestWithRequiredEmailAdd = ReturnModelOfAdd();
            requestWithRequiredEmailAdd.Email = "";
            yield return new object[]
            {
            requestWithRequiredEmailAdd,
            ApiErrorMessage.EmailIsRequire
            };


            var requestWithRequiredLastNameAdd = ReturnModelOfAdd();
            requestWithRequiredLastNameAdd.LastName = "";
            yield return new object[]
            {
            requestWithRequiredLastNameAdd,
            ApiErrorMessage.LastNameIsRequired
            };


            var requestWithRequiredPatronymicAdd = ReturnModelOfAdd();
            requestWithRequiredPatronymicAdd.Patronymic = "";
            yield return new object[]
            {
            requestWithRequiredPatronymicAdd,
            ApiErrorMessage.PatronymicIsRequired
            };

            var requestWithRequiredPhoneNumberAdd = ReturnModelOfAdd();
            requestWithRequiredPhoneNumberAdd.Phone = "";
            yield return new object[]
            {
            requestWithRequiredPhoneNumberAdd,
            ApiErrorMessage.PhoneNumberIsRequired
            };

            var requestWithRequiredEducationAdd = ReturnModelOfAdd();
            requestWithRequiredEducationAdd.Education = null;
            yield return new object[]
            {
            requestWithRequiredEducationAdd,
            ApiErrorMessage.EducationIsRequired
            };

            var requestWithRequiredTherapyMethodsAdd = ReturnModelOfAdd();
            requestWithRequiredTherapyMethodsAdd.TherapyMethods = null;
            yield return new object[]
            {
            requestWithRequiredTherapyMethodsAdd,
            ApiErrorMessage.TherapyMethodsIsRequired
            };

            var requestWithRequiredProblemsAdd = ReturnModelOfAdd();
            requestWithRequiredProblemsAdd.Problems = null;
            yield return new object[]
            {
            requestWithRequiredProblemsAdd,
            ApiErrorMessage.ProblemsIsRequired
            };
        }
    }
 }

