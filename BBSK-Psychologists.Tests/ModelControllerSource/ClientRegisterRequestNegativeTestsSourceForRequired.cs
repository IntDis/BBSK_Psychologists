﻿using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models;
using System.Collections;


namespace BBSK_Psychologists.Tests.ModelControllerSource;

public class ClientRegisterRequestNegativeTestsSourceForRequired : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
             new ClientRegisterRequest
             {
                 Name = "",
                 LastName ="Petrov",
                 Password="123456789",
                 Email ="p@petrov.com",
                 PhoneNumber ="89119118696",
                 BirthDate = DateTime.Now,
             },
             ApiErrorMessage.NameIsRequired
        };

        yield return new object[]
        {
             new ClientRegisterRequest
             {
                 Name = "Petro",
                 LastName ="Petrov",
                 Password="",
                 Email ="p@petrov.com",
                 PhoneNumber ="89119118696",
                 BirthDate = DateTime.Now,
             },
             ApiErrorMessage.PasswordIsRequire
        };

        yield return new object[]
        {
             new ClientRegisterRequest
             {
                 Name = "Petro",
                 LastName ="Petrov",
                 Password="123456789",
                 Email ="",
                 PhoneNumber ="89119118696",
                 BirthDate = DateTime.Now,
             },
             ApiErrorMessage.EmailIsRequire
        };
    }
}

