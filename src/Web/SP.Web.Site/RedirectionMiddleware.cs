using Microsoft.AspNetCore.Http.Extensions;

namespace SP.Web.Site;

public class RedirectionMiddleware
{
    private readonly RequestDelegate _next;

    public RedirectionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var originalUrl = context.Request.GetDisplayUrl(); // Check the URL which you want to validate

        if (originalUrl.Contains("https://stuffpacker.net"))
        {
            var redirectToAnotherURL = "https://beta.stuffpacker.net";

            context.Response.Redirect(redirectToAnotherURL);
        }

        await _next(context);
    }
}