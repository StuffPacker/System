using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sp.Api.Business.Feature.PackingList;
using SP.Database.Mongo;

namespace Sp.Api.Business;

public static class Extensions
{
    public static IServiceCollection AddApiInfrastructureBusiness(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IPackingListService, PackingListService>();

        services.AddMediatR(typeof(Extensions));
        services.AddInfrastructureMongoDb(configuration);

        return services;
    }
}