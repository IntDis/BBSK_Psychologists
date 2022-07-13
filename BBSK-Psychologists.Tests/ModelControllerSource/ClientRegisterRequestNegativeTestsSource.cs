using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models;
using System.Collections;


namespace BBSK_Psychologists.Tests.ModelControllerSource;

public class ClientRegisterRequestNegativeTestsSource : IEnumerable
{

    public ClientRegisterRequest GetClientRegisterProperModelForTest()
    {
        return new ClientRegisterRequest()
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
        clientRegisterRequest.Password = "12";
        yield return new object[]
        {
            clientRegisterRequest,
            ApiErrorMessage.PasswordLengthIsLessThanAllowed
        };

        clientRegisterRequest = GetClientRegisterProperModelForTest();
        clientRegisterRequest.Email = "ppetrov.com";
        yield return new object[]
        {
             clientRegisterRequest,
             ApiErrorMessage.InvalidCharacterInEmail
        };

        clientRegisterRequest = GetClientRegisterProperModelForTest();
        clientRegisterRequest.Name = "";
        yield return new object[]
        {
             clientRegisterRequest,
             ApiErrorMessage.NameIsRequired
        };


        clientRegisterRequest = GetClientRegisterProperModelForTest();
        clientRegisterRequest.Password = "";
        yield return new object[]
        {
             clientRegisterRequest,
             ApiErrorMessage.PasswordIsRequire
        };


        clientRegisterRequest = GetClientRegisterProperModelForTest();
        clientRegisterRequest.Email = "";
        yield return new object[]
        {
             clientRegisterRequest,
             ApiErrorMessage.EmailIsRequire
        };

        clientRegisterRequest = GetClientRegisterProperModelForTest();
        clientRegisterRequest.PhoneNumber = "8911911118696";
        yield return new object[]
        {
             clientRegisterRequest,
             ApiErrorMessage.LengthExceeded
        };
    }

}

