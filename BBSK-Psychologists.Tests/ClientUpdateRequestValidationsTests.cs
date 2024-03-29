﻿using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models;
using BBSK_Psychologists.Tests.ModelControllerSource;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psychologists.Tests;

public class ClientUpdateRequestValidationsTests
{

    [TestCaseSource(typeof(ClientUpdateRequestNegativeTestsSource))]
    public async Task ClientUpdateRequest_SendingIncorrectData_GetErrorMessage(ClientUpdateRequest client, string errorMessage)
    {
        //given
        var validationsResults = new List<ValidationResult>();

        //when
        var isValid = Validator.TryValidateObject(client, new ValidationContext(client), validationsResults, true);

        //then
        var actualMessage = validationsResults[0].ErrorMessage;
        Assert.AreEqual(errorMessage, actualMessage);
    }


    [Test]
    public async Task ClientUpdateRequest_SendingCorrectData_GetAnEmptyStringError()
    {
        //given
        DateTime start = new DateTime(1995, 1, 1);
        int range = (DateTime.Today - start).Days;
        var client = new ClientUpdateRequest()
        {
            Name = "Petro",
            BirthDate = DateTime.Now.AddYears(-20),
            LastName = ""
        };

        var validationsResults = new List<ValidationResult>();

        //when
        var isValid = Validator.TryValidateObject(client, new ValidationContext(client), validationsResults, true);

        //then
        Assert.True(isValid);
    }
}
