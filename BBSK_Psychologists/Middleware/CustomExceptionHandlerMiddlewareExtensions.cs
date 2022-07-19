using Microsoft.AspNetCore.Builder;

namespace BBSK_Psycho.Middleware;

public static class CustomExeptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}
