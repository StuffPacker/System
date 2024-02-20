using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sp.Api.Client.Feature.Client;
using Sp.Api.Client.Feature.Event;
using Sp.Api.Client.Feature.Feature.Item;
using Sp.Api.Client.Feature.Health;
using Sp.Api.Client.Feature.PackingList;
using Sp.Api.Client.Feature.User;

namespace Sp.Api.Client;
public static class Extensions
{
    public static IServiceCollection AddInfrastructureApiClient(
        this IServiceCollection services,
        IConfiguration configuration,
        SpApiOptions options)
    {
        services.AddTransient<ISpApiClient, SpApiClient>();
        services.AddScoped<IApiHealthClient, ApiHealthClient>();
        services.AddScoped<IApiPackingListClient, ApiPackingListClient>();
        services.AddScoped<IApiItemClient, ApiItemClient>();
        services.AddScoped<IApiUserClient, ApiUserClient>();
        services.AddScoped<IApiEventClient, ApiEventClient>();

        services.AddHttpClient("SpApi", httpClient =>
        {
            httpClient.BaseAddress = new Uri(options.BaseUrl);

            // using Microsoft.Net.Http.Headers;
            // The GitHub API requires two headers.
            httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
        });
        return services;
    }
}