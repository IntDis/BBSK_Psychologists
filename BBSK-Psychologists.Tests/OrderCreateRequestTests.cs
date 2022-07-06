﻿using BBSK_Psycho.Models;
using BBSK_Psychologists.Tests.ModelControllerService;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psychologists.Tests
{
    public class OrderCreateRequestTests
    {
        [TestCaseSource(typeof(OrderCreateRequestNegativeTestSource))]
        public void WhenMessageIsNullShouldThrowException(OrderCreateRequest order, string errorMessage)
        {
            //given
            var validationsResults = new List<ValidationResult>();

            //when
            var isValid = Validator.TryValidateObject(order, new ValidationContext(order), validationsResults, true);

            //then
            var actualMessage = validationsResults[0].ErrorMessage;
            Assert.AreEqual(errorMessage, actualMessage);
        }
    }
}
