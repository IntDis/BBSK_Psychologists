using BBSK_Psycho.DataLayer;
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
    public class PsychologistAddRequestTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[]
            {
            new AddPsychologistRequest
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
                Password = "123"
            },
            ApiErrorMessage.PasswordLengthIsLessThanAllowed
            };

            yield return new object[]
           {
            new AddPsychologistRequest
            {
                Name = "",
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
                Password = "123354545"
            },
            ApiErrorMessage.NameIsRequired
           };

            yield return new object[]
         {
            new AddPsychologistRequest
            {
                Name = "Глаша",
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
                Password = ""
            },
            ApiErrorMessage.PasswordIsRequire
         };

            yield return new object[]
      {
            new AddPsychologistRequest
            {
                Name = "Глаша",
                LastName = "пвфа",
                Patronymic = "ПВАПВА",
                gender = Gender.Male,
                Phone = "85884859",
                Education = new List<string> { "2013 - воврварараар; Dev Education", "sg osgj sopj r" },
                checkStatus = CheckStatus.Completed,
                Email = "",
                PasportData = "23146456",
                Price = 2000,
                Problems = new List<string> { "dfsdf", "dasd" },
                TherapyMethods = new List<string> { "dasda", "asd" },
                WorkExperience = 10,
                BirthDate = DateTime.Parse("1210 - 12 - 12"),
                Password = "23534534"
            },
            ApiErrorMessage.EmailIsRequire
      };

            yield return new object[]
     {
            new AddPsychologistRequest
            {
                Name = "Глаша",
                LastName = "",
                Patronymic = "ПВАПВА",
                gender = Gender.Male,
                Phone = "85884859",
                Education = new List<string> { "2013 - воврварараар; Dev Education", "sg osgj sopj r" },
                checkStatus = CheckStatus.Completed,
                Email = "fj@fk.ru",
                PasportData = "23146456",
                Price = 2000,
                Problems = new List<string> { "dfsdf", "dasd" },
                TherapyMethods = new List<string> { "dasda", "asd" },
                WorkExperience = 10,
                BirthDate = DateTime.Parse("1210 - 12 - 12"),
                Password = "23534534"
            },
            ApiErrorMessage.LastNameIsRequired
     };

            yield return new object[]
 {
            new AddPsychologistRequest
            {
                Name = "Глаша",
                LastName = "Лялла",
                Patronymic = "",
                gender = Gender.Male,
                Phone = "85884859",
                Education = new List<string> { "2013 - воврварараар; Dev Education", "sg osgj sopj r" },
                checkStatus = CheckStatus.Completed,
                Email = "fj@fk.ru",
                PasportData = "23146456",
                Price = 2000,
                Problems = new List<string> { "dfsdf", "dasd" },
                TherapyMethods = new List<string> { "dasda", "asd" },
                WorkExperience = 10,
                BirthDate = DateTime.Parse("1210 - 12 - 12"),
                Password = "23534534"
            },
            ApiErrorMessage.PatronymicIsRequired
 };

            yield return new object[]
{
                        new AddPsychologistRequest
                        {
                            Name = "Глаша",
                            LastName = "Лялла",
                            Patronymic = "Пывпапы",
                            gender = null,
                            Phone = "85884859",
                            Education = new List<string> { "2013 - воврварараар; Dev Education", "sg osgj sopj r" },
                            checkStatus = CheckStatus.Completed,
                            Email = "fj@fk.ru",
                            PasportData = "23146456",
                            Price = 2000,
                            Problems = new List<string> { "dfsdf", "dasd" },
                            TherapyMethods = new List<string> { "dasda", "asd" },
                            WorkExperience = 10,
                            BirthDate = DateTime.Parse("1210 - 12 - 12"),
                            Password = "23534534"
                        },
                        ApiErrorMessage.PsychologistGenderIsRequired
};
            yield return new object[]
{
            new AddPsychologistRequest
            {
                Name = "Глаша",
                LastName = "Лялла",
                Patronymic = "Лалаовв",
                gender = Gender.Male,
                Phone = "85884859",
                Education = new List<string> { "2013 - воврварараар; Dev Education", "sg osgj sopj r" },
                checkStatus = CheckStatus.Completed,
                Email = "fj@fk.ru",
                PasportData = "23146456",
                Price = 2000,
                Problems = new List<string> { "dfsdf", "dasd" },
                TherapyMethods = new List<string> { "dasda", "asd" },
                WorkExperience = 10,
                BirthDate = null,
                Password = "23534534"
            },
            ApiErrorMessage.BirthDateIsRequired
};
            yield return new object[]
{
            new AddPsychologistRequest
            {
                Name = "Глаша",
                LastName = "Лялла",
                Patronymic = "Лалаовв",
                gender = Gender.Male,
                Phone = "",
                Education = new List<string> { "2013 - воврварараар; Dev Education", "sg osgj sopj r" },
                checkStatus = CheckStatus.Completed,
                Email = "fj@fk.ru",
                PasportData = "23146456",
                Price = 2000,
                Problems = new List<string> { "dfsdf", "dasd" },
                TherapyMethods = new List<string> { "dasda", "asd" },
                WorkExperience = 10,
                BirthDate = DateTime.Parse("1210 - 12 - 12"),
                Password = "23534534"
            },
            ApiErrorMessage.PhoneNumberIsRequired
};

            yield return new object[]
{
            new AddPsychologistRequest
            {
                Name = "Глаша",
                LastName = "Лялла",
                Patronymic = "Лалаовв",
                gender = Gender.Male,
                Phone = "3645622456546",
                Education = null,
                checkStatus = CheckStatus.Completed,
                Email = "fj@fk.ru",
                PasportData = "583757",
                Price = 2000,
                Problems = new List<string> { "dfsdf", "dasd" },
                TherapyMethods = new List<string> { "dasda", "asd" },
                WorkExperience = 10,
                BirthDate = DateTime.Parse("1210 - 12 - 12"),
                Password = "23534534"
            },
            ApiErrorMessage.EducationIsRequired
};

            yield return new object[]
{
            new AddPsychologistRequest
            {
                Name = "Глаша",
                LastName = "Лялла",
                Patronymic = "Лалаовв",
                gender = Gender.Male,
                Phone = "3645622456546",
                Education = new List<string> { "2013 - воврварараар; Dev Education", "sg osgj sopj r" } ,
                checkStatus = CheckStatus.Completed,
                Email = "fj@fk.ru",
                PasportData = "583757",
                Price = 2000,
                Problems = new List<string> { "dfsdf", "dasd" },
                TherapyMethods = null,
                WorkExperience = 10,
                BirthDate = DateTime.Parse("1210 - 12 - 12"),
                Password = "23534534"
            },
            ApiErrorMessage.TherapyMethodsIsRequired
};
            yield return new object[]
{
            new AddPsychologistRequest
            {
                Name = "Глаша",
                LastName = "Лялла",
                Patronymic = "Лалаовв",
                gender = Gender.Male,
                Phone = "3645622456546",
                Education = new List<string> { "2013 - воврварараар; Dev Education", "sg osgj sopj r" } ,
                checkStatus = CheckStatus.Completed,
                Email = "fj@fk.ru",
                PasportData = "583757",
                Price = 2000,
                Problems = null,
                TherapyMethods =  new List<string> { "dasda", "asd" },
                WorkExperience = 10,
                BirthDate = DateTime.Parse("1210 - 12 - 12"),
                Password = "23534534"
            },
            ApiErrorMessage.ProblemsIsRequired
};
        }
    }
}
