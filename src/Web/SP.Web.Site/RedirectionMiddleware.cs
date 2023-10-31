using Microsoft.AspNetCore.Http.Extensions;

namespace SP.Web.Site;

public class RedirectionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RedirectionMiddleware> _logger;

    public RedirectionMiddleware(RequestDelegate next, ILogger<RedirectionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var originalUrl = context.Request.GetDisplayUrl(); // Check the URL which you want to validate
        _logger.LogWarning("check middlewere " + originalUrl);
        if (originalUrl.Contains("https://stuffpacker.net"))
        {
            _logger.LogWarning("check middlewere redirect from " + originalUrl);
            var redirectToAnotherURL = "https://beta.stuffpacker.net";

            context.Response.Redirect(redirectToAnotherURL);
        }

        await _next(context);
    }
}