
using BBSK_Psycho.Models;
using BBSK_Psycho.Models.Requests;
using BBSK_Psychologists.Tests.ModelControllerSource;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psychologists.Tests;

public class LoginRequestValidationsTests
{

    [TestCaseSource(typeof(LoginRequestNegativeTestsSource))]
    public void LoginRequest_SendingIncorrectData_GetErrorMessage(LoginRequest request, string errorMessage)
    {
        //given
        var validationsResults = new List<ValidationResult>();

        //when
        var isValid = Validator.TryValidateObject(request, new ValidationContext(request), validationsResults, true);

        //then
        var actualMessage = validationsResults[0].ErrorMessage;
        Assert.AreEqual(errorMessage, actualMessage);
    }


    [Test]
    public void LoginRequest_SendingCorrectData_GetAnEmptyStringError()
    {
        //given

        var request = new LoginRequest()
        {
            Email = "ad@mail.ru",
            Password = "123456789"
        };

        var validationsResults = new List<ValidationResult>();

        //when
        var isValid = Validator.TryValidateObject(request, new ValidationContext(request), validationsResults, true);

        //then
        Assert.True(isValid);
    }

}
