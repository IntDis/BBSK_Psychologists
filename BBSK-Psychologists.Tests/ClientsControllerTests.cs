using AutoMapper;
using BBSK_Psycho;
using BBSK_Psycho.BusinessLayer;
using BBSK_Psycho.BusinessLayer.Services;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.Controllers;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.Models;
using BBSK_Psycho.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BBSK_Psychologists.Tests;

public class ClientsControllerTests
{
    private ClientsController _sut;
    private Mock<IClientsServices> _clientsServicesMock;
    private IMapper _mapper;

    private ClaimModel _claim;

    [SetUp]
    public void Setup()
    {
        _claim = new();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MapperConfigStorage>()));
        _clientsServicesMock = new Mock<IClientsServices>();
        _sut = new ClientsController(_clientsServicesMock.Object, _mapper);

    }

    [Test]
    public void AddClient_ValidRequestPassed_CreatedResultReceived()
    {
        //given
        _clientsServicesMock.Setup(c => c.AddClient(It.IsAny<Client>()))
         .Returns(1);

        var expectedId = 1;

        var client = new ClientRegisterRequest()
        {
            
            Name = "Roma",
            LastName = "Petrov",
            Email = "Va@gmail.com",
            Password = "12345678dad",
            PhoneNumber = "89119856375",
        };

        //when
        var actual = _sut.AddClient(client);
        
        //then
        var actualResult = actual.Result as CreatedResult;

        Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);

        Assert.True((int)actualResult.Value == expectedId);

        _clientsServicesMock.Verify(c => c.AddClient(It.Is<Client>(c=>
        c.Name == client.Name &&
        c.LastName == client.LastName &&
        c.Email == client.Email &&
        c.Password == client.Password &&
        c.PhoneNumber == client.PhoneNumber &&
        !c.IsDeleted)), Times.Once);

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
            PhoneNumber = "89119856375"
        };


        _clientsServicesMock.Setup(o => o.GetClientById(expectedClient.Id, It.IsAny<ClaimModel>())).Returns(expectedClient);


        //when
        var actual = _sut.GetClientById(expectedClient.Id);

        //then
        var actualResult = actual.Result as ObjectResult;
        var actualClient = actualResult.Value as ClientResponse;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);

        Assert.AreEqual(expectedClient.Id, actualClient.Id);
        Assert.AreEqual(expectedClient.Name, actualClient.Name);
        Assert.AreEqual(expectedClient.LastName, actualClient.LastName);
        Assert.AreEqual(expectedClient.Email, actualClient.Email);
        Assert.AreEqual(expectedClient.PhoneNumber, actualClient.PhoneNumber);
        _clientsServicesMock.Verify(c => c.GetClientById(expectedClient.Id, It.IsAny<ClaimModel>()), Times.Once);


    }

    [Test]
    public void UpdateClientById_ValidRequestPassed_NoContentReceived()
    {
        //given

        var client = new Client()
        {
            Id = 1,
            Name = "Vasya",
            LastName = "Petrov",
            Email = "Va@gmail.com",
            Password = "12345678dad",
            PhoneNumber = "89119856375",
            BirthDate = DateTime.UtcNow,
        };


        var newClientModel = new ClientUpdateRequest()
        {
            Name = "Petro",
            LastName = "Sobakov",
            BirthDate = new DateTime(1995,05,05)
        };

        _clientsServicesMock.Setup(o => o.UpdateClient(client, client.Id, _claim));


        //when
        var actual = _sut.UpdateClientById(newClientModel, client.Id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode); 

        _clientsServicesMock.Verify(c => c.UpdateClient(It.Is<Client>(c=>
        c.Name== newClientModel.Name &&
        c.LastName == newClientModel.LastName &&
        c.BirthDate == newClientModel.BirthDate), client.Id, It.IsAny<ClaimModel>()), Times.Once);


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


        _clientsServicesMock.Setup(o => o.GetCommentsByClientId(expectedClient.Id, It.IsAny<ClaimModel>())).Returns(expectedClient.Comments);

        //when
        var actual = _sut.GetCommentsByClientId(expectedClient.Id);

        //then
        var actualResult = actual.Result as ObjectResult;
        var actualComments = actualResult.Value as List<CommentResponse>;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(expectedClient.Comments.Count, actualComments.Count);
        Assert.AreEqual(expectedClient.Comments[0].Id, actualComments[0].Id);
        Assert.AreEqual(expectedClient.Comments[0].Text, actualComments[0].Text);
        Assert.AreEqual(expectedClient.Comments[0].Date, actualComments[0].Date);
        Assert.AreEqual(expectedClient.Comments[1].Id, actualComments[1].Id);
        Assert.AreEqual(expectedClient.Comments[1].Text, actualComments[1].Text);
        Assert.AreEqual(expectedClient.Comments[1].Date, actualComments[1].Date);
        _clientsServicesMock.Verify(c => c.GetCommentsByClientId(expectedClient.Id, It.IsAny<ClaimModel>()), Times.Once);
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
                    Id = 1,
                    Message="ApAp",
                    Cost=1,
                    SessionDate = DateTime.Now,
                    OrderDate=DateTime.Now,
                    PayDate=DateTime.Now,
                    OrderStatus = OrderStatus.Completed,
                    OrderPaymentStatus = OrderPaymentStatus.Paid
                },
                new()
                {
                    Id = 2, Message="222",Cost=3,PayDate=DateTime.Now
                }
            },

        };

        _clientsServicesMock.Setup(o => o.GetOrdersByClientId(expectedClient.Id, It.IsAny<ClaimModel>())).Returns(expectedClient.Orders);

        //when
        var actual = _sut.GetOrdersByClientId(expectedClient.Id);

        //then
        var actualResult = actual.Result as ObjectResult;
        var actualOrders = actualResult.Value as List<OrderResponse>;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(expectedClient.Orders.Count, actualOrders.Count);
        Assert.AreEqual(expectedClient.Orders[0].Id, actualOrders[0].Id);
        Assert.AreEqual(expectedClient.Orders[0].Cost, actualOrders[0].Cost);
        Assert.AreEqual(expectedClient.Orders[0].SessionDate, actualOrders[0].SessionDate);
        Assert.AreEqual(expectedClient.Orders[0].SessionDate, actualOrders[0].SessionDate);
        Assert.AreEqual(expectedClient.Orders[0].OrderDate, actualOrders[0].OrderDate);
        Assert.AreEqual(expectedClient.Orders[0].PayDate, actualOrders[0].PayDate);
        Assert.AreEqual(expectedClient.Orders[0].OrderStatus, actualOrders[0].OrderStatus);
        Assert.AreEqual(expectedClient.Orders[0].OrderPaymentStatus, actualOrders[0].OrderPaymentStatus);


        _clientsServicesMock.Verify(c => c.GetOrdersByClientId(expectedClient.Id, It.IsAny<ClaimModel>()), Times.Once);

    }

    [Test]
    public void DeleteClientById_ValidRequestPassed_NoContentReceived()
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
            IsDeleted = false

        };

        _clientsServicesMock.Setup(o => o.GetClientById(expectedClient.Id, It.IsAny<ClaimModel>())).Returns(expectedClient);

        //when
        var actual = _sut.DeleteClientById(expectedClient.Id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);
        _clientsServicesMock.Verify(c => c.DeleteClient(expectedClient.Id, It.IsAny<ClaimModel>()), Times.Once);


    }

    [Test]
    public void GetClients_ValidRequestPassed_RequestedTypeReceived()
    {
        //given
        var clients = new List<Client>
        {
            new Client()
            {
                Id= 1,
                Name = "John",
                LastName = "Petrov",
                Email = "Va@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375",
                BirthDate = DateTime.Now
            },
            new Client()
            {
                Id =2,
                Name = "Vasya",
                LastName = "Petrov",
                Email = "Va@gmail.com",
                Password = "12345678dad",
                PhoneNumber = "89119856375",
                IsDeleted = true,
            },
            new Client()
            {
                 Id =3,
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
        var actualClients = actualResult.Value as List<ClientResponse>;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(actualClients.Count, clients.Count);
        Assert.AreEqual(actualClients[0].Id, clients[0].Id);
        Assert.AreEqual(actualClients[0].Name, clients[0].Name);
        Assert.AreEqual(actualClients[0].LastName, clients[0].LastName);
        Assert.AreEqual(actualClients[0].Email, clients[0].Email);
        Assert.AreEqual(actualClients[0].PhoneNumber, clients[0].PhoneNumber);
        Assert.AreEqual(actualClients[0].RegistrationDate, clients[0].RegistrationDate);
        Assert.AreEqual(actualClients[0].RegistrationDate, clients[0].RegistrationDate);
        Assert.AreEqual(actualClients[0].BirthDate, clients[0].BirthDate);

        _clientsServicesMock.Verify(c => c.GetClients(), Times.Once);
    }

    [Test]
    public void GetApplicationsForPsychologistByClientId_ValidRequestPassed_RequestedTypeReceived()
    {
        var expectedClient = new Client()
        {

            Name = "Vasya",
            LastName = "Petrov",
            Email = "Va@gmail.com",
            Password = "12345678dad",
            PhoneNumber = "89119856375",
            ApplicationForPsychologistSearch = new()
            {
                new()
                {
                    Id = 1,
                    Name ="Elena",
                    PhoneNumber ="89119802514",
                    Description = "Help her",
                    PsychologistGender = Gender.Male,
                    CostMin = 100,
                    CostMax = 200,
                    Date = DateTime.Now,
                    Time =TimeOfDay.Evening
                    
                },
                new()
                {
                    Id = 2,
                    Name ="Elena",
                    PhoneNumber ="89119802514",
                }
            },

        };

        _clientsServicesMock.Setup(o => o.GetApplicationsForPsychologistByClientId(expectedClient.Id, It.IsAny<ClaimModel>())).Returns(expectedClient.ApplicationForPsychologistSearch);

        //when
        var actual = _sut.GetApplicationsForPsychologistByClientId(expectedClient.Id);

        //then
        var actualResult = actual.Result as ObjectResult;
        var actualSearchRequest = actualResult.Value as List<SearchResponse>;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(expectedClient.ApplicationForPsychologistSearch.Count, actualSearchRequest.Count);
        Assert.AreEqual(expectedClient.ApplicationForPsychologistSearch[0].Id,actualSearchRequest[0].Id);
        Assert.AreEqual(expectedClient.ApplicationForPsychologistSearch[0].Name, actualSearchRequest[0].Name);
        Assert.AreEqual(expectedClient.ApplicationForPsychologistSearch[0].PhoneNumber, actualSearchRequest[0].PhoneNumber);
        Assert.AreEqual(expectedClient.ApplicationForPsychologistSearch[0].Description, actualSearchRequest[0].Description);
        Assert.AreEqual(expectedClient.ApplicationForPsychologistSearch[0].PsychologistGender, actualSearchRequest[0].PsychologistGender);
        Assert.AreEqual(expectedClient.ApplicationForPsychologistSearch[0].CostMin, actualSearchRequest[0].CostMin);
        Assert.AreEqual(expectedClient.ApplicationForPsychologistSearch[0].CostMax, actualSearchRequest[0].CostMax);
        Assert.AreEqual(expectedClient.ApplicationForPsychologistSearch[0].Date, actualSearchRequest[0].Date);
        Assert.AreEqual(expectedClient.ApplicationForPsychologistSearch[0].Time, actualSearchRequest[0].Time);

        _clientsServicesMock.Verify(c => c.GetApplicationsForPsychologistByClientId(expectedClient.Id, It.IsAny<ClaimModel>()), Times.Once);
    }

}
