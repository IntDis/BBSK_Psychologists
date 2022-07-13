using BBSK_Psycho.Models.Requests;
using BBSK_Psychologists.Tests.ModelControllerSource;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psychologists.Tests;

public class CommentRequestValidationsTests
{

    [TestCaseSource(typeof(CommentRequestNegativeTestsSource))]
    public void CommentRequest_SendingIncorrectData_GetErrorMessage(CommentRequest comet, string errorMessage)
    {
        //given
        var validationsResults = new List<ValidationResult>();

        //when
        var isValid = Validator.TryValidateObject(comet, new ValidationContext(comet), validationsResults, true);

        //then
        var actualMessage = validationsResults[0].ErrorMessage;
        Assert.AreEqual(errorMessage, actualMessage);
    }


    [TestCaseSource(typeof(CommentRequestPositiveTestsSource))]
    public void ClientUpdateRequest_SendingCorrectData_GetAnEmptyStringError(CommentRequest comet)
    {
        //given
        var validationsResults = new List<ValidationResult>();

        //when
        var isValid = Validator.TryValidateObject(comet, new ValidationContext(comet), validationsResults, true);

        //then
        Assert.True(isValid);
    }
}
