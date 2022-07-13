using BBSK_Psycho.Controllers;
using BBSK_Psycho.Models;
using BBSK_Psychologists.Tests.ModelControllerSource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace BBSK_Psychologists.Tests;


public class ClientsControllerTests
{
    private ClientsController _sut;
    [SetUp]
    public void Setup()
    {
        _sut = new ClientsController();
    }

    [Test]
    public void AddClient_ValidRequestPassed_CreatedResultReceived()
    {

        //given
        var expectedId = 2;
        var client = new ClientRegisterRequest()
        {
            Name = "Petro",
            Password = "1234567894",
            Email = "a@hdjk.com"
        };
        //when
        var actual = _sut.AddClient(client);

        //then
        var actualResult = actual.Result as CreatedResult;

        Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);
        Assert.AreEqual(expectedId, actualResult.Value);
    }



    [Test]
    public void GetClientById_ValidRequestPassed_OkReceived()
    {
        //given
        var expectedClient = new ClientResponse();
        var clientId = 2;
       
        //when
        var actual = _sut.GetClientById(clientId);

        //then
        var actualResult =actual.Result as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK , actualResult.StatusCode);
        Assert.AreEqual(expectedClient.GetType(), actualResult.Value.GetType());
    }

    [Test]
    public void UpdateClientById_ValidRequestPassed_NoContentReceived()
    {
        //given
        var expectedClient = new ClientUpdateRequest();
        var clientId = 2;

        //when
        var actual = _sut.UpdateClientById(expectedClient, clientId);

        //then
        var actualResult =  actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);
       
    }

    [Test]
    public void GetCommentsByClientId_ValidRequestPassed_RequestedTypeReceived()
    {
        //given
        var expectedComment = new List <CommentResponse>();
        var clientId = 2;

        //when
        var actual = _sut.GetCommentsByClientId(clientId);

        //then
        var actualResult = actual.Result as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(expectedComment.GetType(), actualResult.Value.GetType());
    }

   
    [Test]
    public void GetOrdersByClientId_ValidRequestPassed_RequestedTypeReceived()
    {
        //given
        var expectedOrders = new List<OrderResponse>();
        var clientId = 2;

        //when
        var actual = _sut.GetOrdersByClientId(clientId);

        //then
        var actualResult = actual.Result as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(expectedOrders.GetType(), actualResult.Value.GetType());
    }

    [Test]
    public void DeleteClientById_ValidRequestPassed_NoContentReceived()
    {
        //given
        var clientId = 2;

        //when
        var actual = _sut.DeleteClientById(clientId);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

    }

    [Test]
    public void GetClients_ValidRequestPassed_RequestedTypeReceived()
    {
        //given
        var expectedClients = new List<ClientResponse>();

        //when
        var actual = _sut.GetClients();

        //then
        var actualResult = actual.Result as ObjectResult;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(expectedClients.GetType(), actualResult.Value.GetType());
    }

}

