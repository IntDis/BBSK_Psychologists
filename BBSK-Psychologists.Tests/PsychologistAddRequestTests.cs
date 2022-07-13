using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models.Requests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psychologists.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCaseSource(typeof(PsychologistAddRequestTestSource))]
        public void TestPasswordLengthIsLess(AddPsychologistRequest addPsychologistRequest, string errorMessage)
        {
            var validationResultList = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(addPsychologistRequest, new ValidationContext(addPsychologistRequest), validationResultList, true);
            var message = validationResultList[0].ErrorMessage;
            Assert.AreEqual(errorMessage, message);

        }

        [TestCaseSource(typeof(PsychologistAddRequestTestSource))]
        public void TestNameIsRequired(AddPsychologistRequest addPsychologistRequest, string errorMessage)
        {
            var validationResultList = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(addPsychologistRequest, new ValidationContext(addPsychologistRequest), validationResultList, true);
            var message = validationResultList[0].ErrorMessage;
            Assert.AreEqual(errorMessage, message);

        }

        [TestCaseSource(typeof(PsychologistAddRequestTestSource))]
        public void TestPasswordIsRequire(AddPsychologistRequest addPsychologistRequest, string errorMessage)
        {
            var validationResultList = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(addPsychologistRequest, new ValidationContext(addPsychologistRequest), validationResultList, true);
            var message = validationResultList[0].ErrorMessage;
            Assert.AreEqual(errorMessage, message);

        }

        [TestCaseSource(typeof(PsychologistAddRequestTestSource))]
        public void TestEmailIsRequire(AddPsychologistRequest addPsychologistRequest, string errorMessage)
        {
            var validationResultList = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(addPsychologistRequest, new ValidationContext(addPsychologistRequest), validationResultList, true);
            var message = validationResultList[0].ErrorMessage;
            Assert.AreEqual(errorMessage, message);

        }
        [TestCaseSource(typeof(PsychologistAddRequestTestSource))]
        public void TestLastNameIsRequired(AddPsychologistRequest addPsychologistRequest, string errorMessage)
        {
            var validationResultList = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(addPsychologistRequest, new ValidationContext(addPsychologistRequest), validationResultList, true);
            var message = validationResultList[0].ErrorMessage;
            Assert.AreEqual(errorMessage, message);

        }
        [TestCaseSource(typeof(PsychologistAddRequestTestSource))]
        public void TestPatronymicIsRequired(AddPsychologistRequest addPsychologistRequest, string errorMessage)
        {
            var validationResultList = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(addPsychologistRequest, new ValidationContext(addPsychologistRequest), validationResultList, true);
            var message = validationResultList[0].ErrorMessage;
            Assert.AreEqual(errorMessage, message);

        }
        [TestCaseSource(typeof(PsychologistAddRequestTestSource))]
        public void TestPsychologistGenderIsRequired(AddPsychologistRequest addPsychologistRequest, string errorMessage)
        {
            var validationResultList = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(addPsychologistRequest, new ValidationContext(addPsychologistRequest), validationResultList, true);
            var message = validationResultList[0].ErrorMessage;
            Assert.AreEqual(errorMessage, message);

        }

        [TestCaseSource(typeof(PsychologistAddRequestTestSource))]
        public void TestBirthDateIsRequired(AddPsychologistRequest addPsychologistRequest, string errorMessage)
        {
            var validationResultList = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(addPsychologistRequest, new ValidationContext(addPsychologistRequest), validationResultList, true);
            var message = validationResultList[0].ErrorMessage;
            Assert.AreEqual(errorMessage, message);

        }

        [TestCaseSource(typeof(PsychologistAddRequestTestSource))]
        public void TestPhineNumberIsRequired(AddPsychologistRequest addPsychologistRequest, string errorMessage)
        {
            var validationResultList = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(addPsychologistRequest, new ValidationContext(addPsychologistRequest), validationResultList, true);
            var message = validationResultList[0].ErrorMessage;
            Assert.AreEqual(errorMessage, message);
        }
         [TestCaseSource(typeof(PsychologistAddRequestTestSource))]
        public void WorkExperienceIsRequired(AddPsychologistRequest addPsychologistRequest, string errorMessage)
        {
            var validationResultList = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(addPsychologistRequest, new ValidationContext(addPsychologistRequest), validationResultList, true);
            var message = validationResultList[0].ErrorMessage;
            Assert.AreEqual(errorMessage, message);
        }

        [TestCaseSource(typeof(PsychologistAddRequestTestSource))]
        public void PassportDataIsRequired(AddPsychologistRequest addPsychologistRequest, string errorMessage)
        {
            var validationResultList = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(addPsychologistRequest, new ValidationContext(addPsychologistRequest), validationResultList, true);
            var message = validationResultList[0].ErrorMessage;
            Assert.AreEqual(errorMessage, message);
        }

        [TestCaseSource(typeof(PsychologistAddRequestTestSource))]
        public void EducationIsRequired(AddPsychologistRequest addPsychologistRequest, string errorMessage)
        {
            var validationResultList = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(addPsychologistRequest, new ValidationContext(addPsychologistRequest), validationResultList, true);
            var message = validationResultList[0].ErrorMessage;
            Assert.AreEqual(errorMessage, message);
        }
        [TestCaseSource(typeof(PsychologistAddRequestTestSource))]
        public void TherapyMethodsIsRequired(AddPsychologistRequest addPsychologistRequest, string errorMessage)
        {
            var validationResultList = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(addPsychologistRequest, new ValidationContext(addPsychologistRequest), validationResultList, true);
            var message = validationResultList[0].ErrorMessage;
            Assert.AreEqual(errorMessage, message);
        }

        [TestCaseSource(typeof(PsychologistAddRequestTestSource))]
        public void ProblemsIsRequired(AddPsychologistRequest addPsychologistRequest, string errorMessage)
        {
            var validationResultList = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(addPsychologistRequest, new ValidationContext(addPsychologistRequest), validationResultList, true);
            var message = validationResultList[0].ErrorMessage;
            Assert.AreEqual(errorMessage, message);
        }
       
    }

    
}
