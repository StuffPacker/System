using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SP.Web.Business.Feature.PackingList;

namespace SP.Web.Business;

public static class Extensions
{
    public static IServiceCollection AddInfrastructureBusiness(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(typeof(Extensions));
        services.AddTransient<IPackingListService, PackingListService>();

        return services;
    }
}