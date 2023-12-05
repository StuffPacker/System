using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SP.Database.Mongo;

namespace Sp.Api.Business;

public static class Extensions
{
    public static IServiceCollection AddApiInfrastructureBusiness(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(typeof(Extensions));
        services.AddInfrastructureMongoDb(configuration);

        return services;
    }
}