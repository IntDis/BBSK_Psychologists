﻿using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models;
using BBSK_Psychologists.Tests.ModelControllerSource;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psychologists.Tests;

public class ClientRegisterRequestValidationsTests
{

    [TestCaseSource(typeof(ClientRegisterRequestNegativeTestsSource))]
    public void ClientRegisterRequest_SendingIncorrectData_GetErrorMessage(ClientRegisterRequest client, string errorMessage)
    {
        //given
        var validationsResults = new List<ValidationResult>();

        //when
        var isValid = Validator.TryValidateObject(client, new ValidationContext(client), validationsResults, true);

        //then
        var actualMessage = validationsResults[0].ErrorMessage;
        Assert.AreEqual(errorMessage, actualMessage);
    }



    [TestCaseSource(typeof(ClientRegisterRequestPositiveTestsSource))]
    public void ClientRegisterRequest_SendingCorrectData_GetAnEmptyStringError (ClientRegisterRequest client)
    {
        //given
        var validationsResults = new List<ValidationResult>();

        //when
        var isValid = Validator.TryValidateObject(client, new ValidationContext(client), validationsResults, true);

        //then
        Assert.True(isValid);
    }

}
