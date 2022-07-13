using BBSK_Psycho.Infrastructure;
using BBSK_Psycho.Models;
using System.Collections;


namespace BBSK_Psychologists.Tests.ModelControllerSource;

public class LoginRequestNegativeTestsSource : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
             new LoginRequest
             {
                Email = "admail.ru",
                Password = "1234546adadas"
             },
             ApiErrorMessage.InvalidCharacterInEmail
        };

        yield return new object[]
        {
             new LoginRequest
             {
                Email = "ad@mail.ru",
                Password = "1234567"
             },
             ApiErrorMessage.PasswordLengthIsLessThanAllowed
        };

        yield return new object[]
        {
             new LoginRequest
             {
                Email = "ad@mail.ru",
                Password = ""
             },
             ApiErrorMessage.PasswordIsRequire
        };

        yield return new object[]
        {
             new LoginRequest
             {
                Email = "",
                Password = "12sf345878"
             },
             ApiErrorMessage.EmailIsRequire
        };


    }
}

