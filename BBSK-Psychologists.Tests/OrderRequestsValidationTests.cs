using BBSK_Psycho.Models;
using BBSK_Psycho.DataLayer.Enums;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BBSK_Psycho.Infrastructure;

namespace BBSK_Psychologists.Tests
{
    public class OrderRequestsValidationTests
    {
        [Test]
        public void OrderCreateRequest_SendingIncorrectData_GetMessageRequiredError()
        {
            //given
            OrderCreateRequest order = new() 
            { 
                ClientId = 1, 
                Cost = 1200, 
                Duration = SessionDuration.OneAcademicHour, 
                Message = null, 
                OrderDate = DateTime.Now, 
                OrderPaymentStatus = OrderPaymentStatus.Paid, 
                PayDate = DateTime.Now.AddDays(3), 
                SessionDate = DateTime.Now.AddDays(5)
            };

            var validationsResults = new List<ValidationResult>();
            string errorMessage = ApiErrorMessage.MessageIsRequired;

            //when
            var isValid = Validator.TryValidateObject(order, new ValidationContext(order), validationsResults, true);


            //then
            var actualMessage = validationsResults[0].ErrorMessage;


            Assert.AreEqual(order.Message, null);
            Assert.AreEqual(actualMessage, errorMessage);
        }
    }
}
