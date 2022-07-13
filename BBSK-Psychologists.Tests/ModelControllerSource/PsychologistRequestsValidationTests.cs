using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models.Requests;
using BBSK_Psychologists.Tests.ModelControllerSource;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psychologists.Tests.ModelControllerSource
{
    public class PsychologistRequestsValidationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCaseSource(typeof(AddPsychologistRequestSource))]
        public void PsychologistAddRequestValidation_WhenInvalidModel_ValidationErrorsReceived(AddPsychologistRequest addPsychologistRequest, string errorMessage)
        {
            //given
            var validationResultList = new List<ValidationResult>();
            //when
            var isValid = Validator.TryValidateObject(addPsychologistRequest, new ValidationContext(addPsychologistRequest), validationResultList, true);
            var message = validationResultList[0].ErrorMessage;
            //then
            Assert.AreEqual(errorMessage, message);

        }

        [TestCaseSource(typeof(PsychologistUpdateNegativeTestSource))]
        public void PsychologistUpdateRequestValidation_WhenInvalidModel_ValidationErrorsReceived(UpdatePsychologistRequest updatePsychologistRequest, string errorMessage)
        {
            //given
            var validationResultList = new List<ValidationResult>();
            //when
            var isValid = Validator.TryValidateObject(updatePsychologistRequest, new ValidationContext(updatePsychologistRequest), validationResultList, true);
            var message = validationResultList[0].ErrorMessage;
            //then
            Assert.AreEqual(errorMessage, message);

        }

        [TestCaseSource(typeof(PsychologistAddPositiveTestSource))]
        public void PsychologistAddRequestValidation_WhenValidModel_ValidationErrorsNotReceived(AddPsychologistRequest addPsychologistRequest, string errorMessage)
        {
            //given
            var validationResultList = new List<ValidationResult>();
            //when
            var isValid = Validator.TryValidateObject(addPsychologistRequest, new ValidationContext(addPsychologistRequest), validationResultList, true);
            var message = "";
            //then
            Assert.AreEqual(errorMessage, message);

        }

    }

    
}
