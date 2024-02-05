using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sp.Api.Business.Feature.Language;
using Sp.Api.Business.Feature.PackingList;
using SP.Database.Mongo;
using SP.Shared.Common.Feature.Item.Mapper;
using SP.Shared.Common.Feature.PackingList.Mapper;

namespace Sp.Api.Business;

public static class Extensions
{
    public static IServiceCollection AddApiInfrastructureBusiness(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<LanguageServiceOptions>(configuration.GetSection("LanguageServiceOptions"));
        services.AddSingleton<IPackingListService, PackingListService>();

        services.AddMediatR(typeof(Extensions));
        services.AddInfrastructureMongoDb(configuration);
        services.AddSingleton<ILanguageService, LanguageService>();

        return services;
    }
}