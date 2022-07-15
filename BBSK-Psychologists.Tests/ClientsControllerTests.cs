using AutoMapper;
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
    private Mock<IClientsServices> ClientsServicesMock;
    private Mock<IMapper> _mapper;

    private  List<ClaimsIdentity> _identities;

    [SetUp]
    public void Setup()
    {
        
        _mapper = new Mock<IMapper>();
        ClientsServicesMock = new Mock<IClientsServices>();
        _sut= new ClientsController(ClientsServicesMock.Object, _mapper.Object);        
    }

    [Test]
    public void AddClient_ValidRequestPassed_CreatedResultReceived()
    {
        //given
        ClientsServicesMock.Setup(c => c.AddClient(It.IsAny<Client>()))
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

        ClientsServicesMock.Verify(c => c.AddClient(It.IsAny<Client>()), Times.Once);
        ClientsServicesMock.Verify(c => c.DeleteClient(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetClientById(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetClients(),  Times.Never);
        ClientsServicesMock.Verify(c => c.UpdateClient(It.IsAny<Client>(), It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetOrdersByClientId(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetCommentsByClientId(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
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
        ClientsServicesMock.Setup(o => o.GetClientById(expectedClient.Id, _identities)).Returns(expectedClient);


        //when
        var actual = _sut.GetClientById(expectedClient.Id);

        //then
        var actualResult = actual.Result as ObjectResult;

        
        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        ClientsServicesMock.Verify(c => c.AddClient(It.IsAny<Client>()), Times.Never);
        ClientsServicesMock.Verify(c => c.DeleteClient(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetClientById(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Once);
        ClientsServicesMock.Verify(c => c.GetClients(), Times.Never);
        ClientsServicesMock.Verify(c => c.UpdateClient(It.IsAny<Client>(), It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetOrdersByClientId(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetCommentsByClientId(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);

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


        var newProperty = new ClientUpdateRequest()
        {
            Name = "Petro",
            LastName = "Sobakov",
        };

        ClientsServicesMock.Setup(o => o.UpdateClient(client, client.Id, _identities));


        //when
        var actual = _sut.UpdateClientById(newProperty, client.Id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);
        ClientsServicesMock.Verify(c => c.AddClient(It.IsAny<Client>()), Times.Once);
        ClientsServicesMock.Verify(c => c.DeleteClient(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetClientById(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetClients(), Times.Never);
        ClientsServicesMock.Verify(c => c.UpdateClient(It.IsAny<Client>(), It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Once);
        ClientsServicesMock.Verify(c => c.GetOrdersByClientId(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetCommentsByClientId(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);

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


        ClientsServicesMock.Setup(o => o.GetCommentsByClientId(expectedClient.Id,_identities)).Returns(expectedClient.Comments);

        //when
        var actual = _sut.GetCommentsByClientId(expectedClient.Id);

        //then
        var actualResult = actual.Result as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        ClientsServicesMock.Verify(c => c.AddClient(It.IsAny<Client>()), Times.Once);
        ClientsServicesMock.Verify(c => c.DeleteClient(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetClientById(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetClients(), Times.Never);
        ClientsServicesMock.Verify(c => c.UpdateClient(It.IsAny<Client>(), It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetOrdersByClientId(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetCommentsByClientId(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Once);
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

        ClientsServicesMock.Setup(o => o.GetOrdersByClientId(expectedClient.Id, _identities)).Returns(expectedClient.Orders);

        //when
        var actual = _sut.GetOrdersByClientId(expectedClient.Id);

        //then
        var actualResult = actual.Result as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        ClientsServicesMock.Verify(c => c.AddClient(It.IsAny<Client>()), Times.Once);
        ClientsServicesMock.Verify(c => c.DeleteClient(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetClientById(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetClients(), Times.Never);
        ClientsServicesMock.Verify(c => c.UpdateClient(It.IsAny<Client>(), It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetOrdersByClientId(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Once);
        ClientsServicesMock.Verify(c => c.GetCommentsByClientId(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
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

        ClientsServicesMock.Setup(o => o.GetClientById(expectedClient.Id, _identities)).Returns(expectedClient);

        //when
        var actual = _sut.DeleteClientById(expectedClient.Id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);
        ClientsServicesMock.Verify(c => c.AddClient(It.IsAny<Client>()), Times.Once);
        ClientsServicesMock.Verify(c => c.DeleteClient(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Once);
        ClientsServicesMock.Verify(c => c.GetClientById(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetClients(), Times.Never);
        ClientsServicesMock.Verify(c => c.UpdateClient(It.IsAny<Client>(), It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetOrdersByClientId(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetCommentsByClientId(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);

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
        

        ClientsServicesMock.Setup(o => o.GetClients()).Returns(clients).Verifiable();

        //when
        var actual = _sut.GetClients();

        //then
        var actualResult = actual.Result as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        ClientsServicesMock.Verify(c => c.AddClient(It.IsAny<Client>()), Times.Once);
        ClientsServicesMock.Verify(c => c.DeleteClient(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetClientById(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetClients(), Times.Once);
        ClientsServicesMock.Verify(c => c.UpdateClient(It.IsAny<Client>(), It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetOrdersByClientId(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);
        ClientsServicesMock.Verify(c => c.GetCommentsByClientId(It.IsAny<int>(), It.IsAny<List<ClaimsIdentity>>()), Times.Never);

    }

}

