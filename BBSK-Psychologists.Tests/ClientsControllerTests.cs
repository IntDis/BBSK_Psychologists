using AutoMapper;
using BBSK_Psycho.BusinessLayer;
using BBSK_Psycho.BusinessLayer.Services;
using BBSK_Psycho.Controllers;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Security.Claims;

namespace BBSK_Psychologists.Tests;

public class ClientsControllerTests
{
    private ClientsController _sut;
    private Mock<IClientsServices> _clientsServicesMock;
    private Mock<IMapper> _mapper;

    private ClaimModel _claim;

    [SetUp]
    public void Setup()
    {
        _claim = new();
        _mapper = new Mock<IMapper>();
        _clientsServicesMock = new Mock<IClientsServices>();
        _sut= new ClientsController(_clientsServicesMock.Object, _mapper.Object);        
    }

    [Test]
    public void AddClient_ValidRequestPassed_CreatedResultReceived()
    {
        //given
        _clientsServicesMock.Setup(c => c.AddClient(It.IsAny<Client>()))
         .Returns(1);

        var client = new ClientRegisterRequest()
        {
            Name = "Petro",
            Password = "1234567894",
            Email = "a@hdjk.com"
        };

        //when
        var actual = _sut.AddClient(client);
        var a = actual.Result;

        //then
        var actualResult = actual.Result as CreatedResult;

        Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);
        Assert.True((int)actualResult.Value == 1);

        _clientsServicesMock.Verify(c => c.AddClient(It.IsAny<Client>()), Times.Once);
        
    }



    [Test]
    public void GetClientById_ValidRequestPassed_OkReceived()
    {
        //given
        var expectedClient = new Client()
        {
            Id = 1,
            Name = "Roma",
            LastName = "Petrov",
            Email = "Va@gmail.com",
            Password = "12345678dad",
            PhoneNumber = "89119856375",
        };

        //_claim = new() { Email = expectedClient.Email, Role = "Client" };

        _clientsServicesMock.Setup(o => o.GetClientById(expectedClient.Id, _claim)).Returns(expectedClient);


        //when
        var actual = _sut.GetClientById(expectedClient.Id);

        //then
        var actualResult = actual.Result as ObjectResult;

        
        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        _clientsServicesMock.Verify(c => c.GetClientById(It.IsAny<int>(), It.IsAny<ClaimModel>()), Times.Once);


    }

    [Test]
    public void UpdateClientById_ValidRequestPassed_NoContentReceived()
    {
        //given

        var client = new Client()
        {
            Id=1,
            Name = "Vasya",
            LastName = "Petrov",
            Email = "Va@gmail.com",
            Password = "12345678dad",
            PhoneNumber = "89119856375",
        };


        var newClientModel = new ClientUpdateRequest()
        {
            Name = "Petro",
            LastName = "Sobakov",
        };

        _clientsServicesMock.Setup(o => o.UpdateClient(client, client.Id, _claim));


        //when
        var actual = _sut.UpdateClientById(newClientModel, client.Id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

        _clientsServicesMock.Verify(c => c.UpdateClient(It.IsAny<Client>(), It.IsAny<int>(), It.IsAny<ClaimModel>()), Times.Once);


    }

    [Test]
    public void GetCommentsByClientId_ValidRequestPassed_RequestedTypeReceived()
    {
        //given
        var expectedClient = new Client()
        {
            Id = 1,
            Name = "Vasya",
            LastName = "Petrov",
            Email = "Va@gmail.com",
            Password = "12345678dad",
            PhoneNumber = "89119856375",
            Comments = new()
            {
                new()
                {
                    Id = 1, Text="ApAp",Rating=1,Date=DateTime.Now
                },
                new()
                {
                    Id = 2, Text="222",Rating=3,Date=DateTime.Now
                }
            },

        };


        _clientsServicesMock.Setup(o => o.GetCommentsByClientId(expectedClient.Id, _claim)).Returns(expectedClient.Comments);

        //when
        var actual = _sut.GetCommentsByClientId(expectedClient.Id);

        //then
        var actualResult = actual.Result as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        _clientsServicesMock.Verify(c => c.GetCommentsByClientId(It.IsAny<int>(), It.IsAny<ClaimModel>()), Times.Once);
    }


    [Test]
    public void GetOrdersByClientId_ValidRequestPassed_RequestedTypeReceived()
    {
        //given

        var expectedClient = new Client()
        {

            Name = "Vasya",
            LastName = "Petrov",
            Email = "Va@gmail.com",
            Password = "12345678dad",
            PhoneNumber = "89119856375",
            Orders = new()
            {
                new()
                {
                    Id = 1, Message="ApAp",Cost=1,PayDate=DateTime.Now
                },
                new()
                {
                    Id = 2, Message="222",Cost=3,PayDate=DateTime.Now
                }
            },

        };

        _clientsServicesMock.Setup(o => o.GetOrdersByClientId(expectedClient.Id, _claim)).Returns(expectedClient.Orders);

        //when
        var actual = _sut.GetOrdersByClientId(expectedClient.Id);

        //then
        var actualResult = actual.Result as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        _clientsServicesMock.Verify(c => c.GetOrdersByClientId(It.IsAny<int>(), It.IsAny<ClaimModel>()), Times.Once);

    }

    [Test]
    public void DeleteClientById_ValidRequestPassed_NoContentReceived()
    {
        //given
        var expectedClient = new Client()
        {
            Id=1,
            Name = "Vasya",
            LastName = "Petrov",
            Email = "Va@gmail.com",
            Password = "12345678dad",
            PhoneNumber = "89119856375",
            IsDeleted = false

        };

        _clientsServicesMock.Setup(o => o.GetClientById(expectedClient.Id, _claim)).Returns(expectedClient);

        //when
        var actual = _sut.DeleteClientById(expectedClient.Id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);
        _clientsServicesMock.Verify(c => c.DeleteClient(It.IsAny<int>(), It.IsAny<ClaimModel>()), Times.Once);


    }

    [Test]
    public void GetClients_ValidRequestPassed_RequestedTypeReceived()
    {
        //given
        var clients = new List<Client>
        {
            new Client()
            {
                Name = "John",
                LastName = "Petrov",
                Email = "Va@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375",
            },
            new Client()
            {
                Name = "Vasya",
                LastName = "Petrov",
                Email = "Va@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375",
                IsDeleted = true,
            },
            new Client()
            {
                 Name = "Petya",
                 LastName = "Petrov",
                 Email = "Va@gmail.com",
                 Password = "12345678dad",
                 PhoneNumber = "89119856375",
            }
        };
        

        _clientsServicesMock.Setup(o => o.GetClients()).Returns(clients).Verifiable();

        //when
        var actual = _sut.GetClients();

        //then
        var actualResult = actual.Result as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        _clientsServicesMock.Verify(c => c.GetClients(), Times.Once);


    }

}

