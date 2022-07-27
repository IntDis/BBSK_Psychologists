﻿using BBSK_Psycho.DataLayer;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BBSK_DataLayer.Tests;

public class ApplicationForPsychologistSearchRepositoryTests
{
    private DbContextOptions<BBSK_PsychoContext> _dbContextOptions;

    private ApplicationForPsychologistSearchRepository _sut;
    private BBSK_PsychoContext context;

    public ApplicationForPsychologistSearchRepositoryTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<BBSK_PsychoContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

    }

    [SetUp]
    public void Setup()
    {

        if (context is not null)
            context.Database.EnsureDeleted();


        context = new BBSK_PsychoContext(_dbContextOptions);

        _sut = new ApplicationForPsychologistSearchRepository(context);

    }

    [Test]
    public void AddApplicationForPsychologist_WhenCorrectData_ThenAddApplicationForPsychologistInDbAndReturnedId()
    {
        //given

        var client = new Client()
        {
            Name = "vasya",
            Email = "a@mail.ru",
            Password = "11111111",
            PhoneNumber = "89119802569",
        };

        context.Clients.Add(client);
        context.SaveChanges();

        var application = new ApplicationForPsychologistSearch()
        {

            Name = "Alla",
            PhoneNumber = "89119856375",
            Description = "give me a help",
            PsychologistGender = Gender.Male,
            CostMin = 100,
            CostMax = 200,
            Date = new DateTime(2022, 02, 02),
            Time = TimeOfDay.Day,
        };

        //when
        var actualId = _sut.AddApplicationForPsychologist(application, client);
   

        //then
        var actualIdApplication = _sut.GetApplicationForPsychologistById(actualId);


        Assert.AreEqual(actualId, application.Id);
        Assert.AreEqual(actualIdApplication.Name, application.Name);
        Assert.AreEqual(actualIdApplication.PhoneNumber, application.PhoneNumber);
        Assert.AreEqual(actualIdApplication.Description, application.Description);
        Assert.AreEqual(actualIdApplication.PsychologistGender, application.PsychologistGender);
        Assert.AreEqual(actualIdApplication.CostMin, application.CostMin);
        Assert.AreEqual(actualIdApplication.CostMax, application.CostMax);
        Assert.AreEqual(actualIdApplication.Date, application.Date);
        Assert.AreEqual(actualIdApplication.Time, application.Time);
        Assert.False(actualIdApplication.IsDeleted);
    }


    [Test]
    public void DeleteApplicationForPsychologist_WhenCorrecId_ThenSoftDelete()
    {
        //given
        var application = new ApplicationForPsychologistSearch()
        {
            Name = "Alla",
            PhoneNumber = "89119856375",
            Description = "give me a help",
            PsychologistGender = Gender.Male,
            CostMin = 100,
            CostMax = 200,
            Date = new DateTime(2022, 02, 02),
            Time = TimeOfDay.Day,
            Client = new()
            {
                Name = "vasya",
                Email = "a@mail.ru",
                Password = "11111111",
                PhoneNumber = "89119802569",
            }
        };

        context.ApplicationForPsychologistSearches.Add(application);
        context.SaveChanges();

        //when
        _sut.DeleteApplicationForPsychologist(application);

        //then
        var actualIdApplication = _sut.GetApplicationForPsychologistById(application.Id);

        Assert.True(actualIdApplication.IsDeleted);
    }


    [Test]
    public void GetAllApplicationsForPsychologist_WhenCorrectDate_ThenApplicationsForPsychologistReturned()
    {
        //given
        var expectedCount = 1;

        var applicationFirst = new ApplicationForPsychologistSearch()
        {
            Name = "Alla",
            PhoneNumber = "89119856375",
            Description = "give me a help",
            PsychologistGender = Gender.Male,
            CostMin = 100,
            CostMax = 200,
            Date = new DateTime(2022, 02, 02),
            Time = TimeOfDay.Day
        };
        var applicationSecond = new ApplicationForPsychologistSearch()
        {
            Name = "Petrovich",
            PhoneNumber = "89119850000",
            Description = "Hello",
            PsychologistGender = Gender.Male,
            CostMin = 1000,
            CostMax = 2000,
            Date = new DateTime(2022, 06, 02),
            Time = TimeOfDay.Morning,
            IsDeleted = true
        };

        context.ApplicationForPsychologistSearches.Add(applicationFirst);
        context.ApplicationForPsychologistSearches.Add(applicationSecond);

        context.SaveChanges();

        //when
        var actualIdApplications = _sut.GetAllApplicationsForPsychologist();

        //then
        Assert.True(actualIdApplications is not null);
        Assert.AreEqual(actualIdApplications.Count, expectedCount);
        Assert.AreEqual(actualIdApplications[0].Id, applicationFirst.Id);
        Assert.False(actualIdApplications[0].IsDeleted);

    }

    [Test]
    public void GetApplicationForPsychologistById_WhenCorrectDataPassed_ThenApplicationReturned()
    {
        //given

        var client = new Client()
        {

            Name = "vasya",
            Email = "a@mail.ru",
            Password = "11111111",
            PhoneNumber = "89119802569",
            ApplicationForPsychologistSearch = new()
            {
                new()
                {
                    Name = "Alla",
                    PhoneNumber = "89119856375",
                    Description = "give me a help",
                    PsychologistGender = Gender.Male,
                    CostMin = 100,
                    CostMax = 200,
                    Date = new DateTime(2022, 02, 02),
                    Time = TimeOfDay.Day,
                }
            }
        };

        context.Clients.Add(client);
        context.SaveChanges();

        //when

        var actualIdApplication = _sut.GetApplicationForPsychologistById(client.ApplicationForPsychologistSearch[0].Id);


        //then
        Assert.NotNull(actualIdApplication);
        Assert.AreEqual(actualIdApplication.Name, client.ApplicationForPsychologistSearch[0].Name);
        Assert.AreEqual(actualIdApplication.PhoneNumber, client.ApplicationForPsychologistSearch[0].PhoneNumber);
        Assert.AreEqual(actualIdApplication.Description, client.ApplicationForPsychologistSearch[0].Description);
        Assert.AreEqual(actualIdApplication.PsychologistGender, client.ApplicationForPsychologistSearch[0].PsychologistGender);
        Assert.AreEqual(actualIdApplication.CostMax, client.ApplicationForPsychologistSearch[0].CostMax);
        Assert.AreEqual(actualIdApplication.CostMin, client.ApplicationForPsychologistSearch[0].CostMin);
        Assert.AreEqual(actualIdApplication.Date, client.ApplicationForPsychologistSearch[0].Date);
        Assert.AreEqual(actualIdApplication.Time, client.ApplicationForPsychologistSearch[0].Time);
        Assert.False(actualIdApplication.IsDeleted);
    }


    [Test]
    public void UpdateApplicationsForPsychologist_WhenCorrectDate_ThenChangePoperties()
    {
        //given
        var newName = "Oleg";
        var newGender = Gender.Male;
        var newCostMax = 10000;

        var application = new ApplicationForPsychologistSearch()
        {
            Name = "Petya",
            PhoneNumber = "89119802536",
            Description = "Hello",
            PsychologistGender = Gender.Famale,
            CostMin = 20000,
            CostMax = 2555550,
            Date = new DateTime(2022, 01, 01),
            Time = TimeOfDay.Evening,
            Client = new()
            {
                Name = "vasya",
                Email = "a@mail.ru",
                Password = "11111111",
                PhoneNumber = "89119802569",
            }

        };

        context.ApplicationForPsychologistSearches.Add(application);
        context.SaveChanges();

        application.Name = newName;
        application.PsychologistGender = newGender;
        application.CostMax = newCostMax;

        //when
        _sut.UpdateApplicationForPsychologist(application);

        //then
        var actualApplication = _sut.GetApplicationForPsychologistById(application.Id);

        Assert.AreEqual(actualApplication.Name, newName);
        Assert.AreEqual(actualApplication.PsychologistGender, newGender);
        Assert.AreEqual(actualApplication.CostMax, newCostMax);
        Assert.False(actualApplication.IsDeleted);

    }
}