using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SP.Database.Mongo;
using SP.Database.Mongo.Feature.PackingList;
using SP.Database.Mongo.Feature.UserItem;
using SP.Shared.Common.Feature.Database.UserItem;
using SP.Web.Business.Feature.Item;
using SP.Web.Business.Feature.PackingList;

namespace SP.Web.Business;

public static class Extensions
{
    public static IServiceCollection AddInfrastructureBusiness(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructureMongoDb(configuration);
        services.AddSingleton<IItemService, ItemService>();
        services.AddSingleton<IPackingListService, PackingListService>();
        return services;
    }
}