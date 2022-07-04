﻿using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models;
using System.Collections;


namespace BBSK_Psychologists.Tests.ModelControllerSource;

public class ClientUpdateRequestNegativeTestsSourceForRequired : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
             new ClientUpdateRequest
             {
                 Name = "",
                 LastName ="",
                 BirthDate = DateTime.Now,
             },
             ApiErrorMessage.NameIsRequired
        };


    }
}

