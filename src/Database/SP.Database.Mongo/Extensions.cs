using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SP.Database.Mongo.Feature.UserItem;
using SP.Shared.Common.Feature.Database.UserItem;

namespace SP.Database.Mongo;

public static class Extensions
{
    public static IServiceCollection AddInfrastructureMongoDb(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IDatabaseClient, DatabaseClient>();
        services.AddSingleton<IUserItemRepository, UserItemRepository>();
        services.Configure<MongoDbDatabaseOptions>(configuration.GetSection("MongoDbDatabaseOptions"));
        return services;
    }
}