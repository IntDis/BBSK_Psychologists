using BBSK_Psycho.DataLayer.Entities;

using System.Collections;
using System.Data;

namespace BBSK_Psycho.BusinessLayer.Tests.ModelControllerSource;

public class ClientsModelIncorrectedPhoneNumber : IEnumerable
{

    public Client GetClientRegisterProperModelForTest()
    {
        return new Client()
        {
            Name = "Petro",
            LastName = "Petrov",
            Password = "12124564773",
            Email = "p@petrov.com",
            PhoneNumber = "89119118696",
            BirthDate = DateTime.Now,

        };
    }

    public IEnumerator GetEnumerator()
    {
        var clientRegisterRequest = GetClientRegisterProperModelForTest();
        clientRegisterRequest.PhoneNumber = "1";
        yield return new object[]
        {
            clientRegisterRequest,
        };

        clientRegisterRequest = GetClientRegisterProperModelForTest();
        clientRegisterRequest.PhoneNumber = "ppetrov.com";
        yield return new object[]
        {
             clientRegisterRequest,
        };

        clientRegisterRequest = GetClientRegisterProperModelForTest();
        clientRegisterRequest.PhoneNumber = "55555555555";
        yield return new object[]
        {
             clientRegisterRequest,
        };


        clientRegisterRequest = GetClientRegisterProperModelForTest();
        clientRegisterRequest.PhoneNumber = "8(911)9803692";
        yield return new object[]
        {
             clientRegisterRequest,
        };


        clientRegisterRequest = GetClientRegisterProperModelForTest();
        clientRegisterRequest.PhoneNumber = "8-980-999-93-21";
        yield return new object[]
        {
             clientRegisterRequest,
        };

        clientRegisterRequest = GetClientRegisterProperModelForTest();
        clientRegisterRequest.PhoneNumber = "02-05-2325";
        yield return new object[]
        {
             clientRegisterRequest,
        };
    }

}

