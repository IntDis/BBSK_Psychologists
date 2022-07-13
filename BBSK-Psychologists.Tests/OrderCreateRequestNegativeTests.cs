using BBSK_Psycho.Models;
using ModelControllerSource;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psychologists.Tests
{
    public class OrderCreateRequestNegativeTests
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

            Assert.IsNull(order.Message);
            Assert.AreEqual(errorMessage, actualMessage);
        }
    }
}
