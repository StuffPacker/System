using MongoDB.Bson;
using MongoDB.Driver;
using SP.Shared.Common.Feature.User.Model;

namespace SP.Database.Mongo.Feature.User;

public class UserRepository : DataBaseFetchBase, IUserRepository
{
    private readonly IDatabaseClient _databaseClient;

    public UserRepository(IDatabaseClient databaseClient)
        : base(databaseClient)
    {
        _databaseClient = databaseClient;
    }

    public async Task<UserProfileModel> GetByUserId(Guid userId)
    {
        var db = _databaseClient.GetDatabase();
        var collection = db.GetCollection<UserProfileEntity>(MongoDbNames.UserProfileName);
        var filter = MongoDB.Driver.Builders<UserProfileEntity>.Filter.Eq("UserId", userId.ToString());
        var results = await (await collection.FindAsync(filter)).FirstOrDefaultAsync();
        if (results == null)
        {
            return null!;
        }

        return GetModel(results);
    }

    public async Task<UserProfileModel> CreateUserProfile(UserProfileModel model)
    {
        var newid = ObjectId.GenerateNewId();
        model.Id = newid.ToString();
        var entity = new UserProfileEntity(model);
        await Create(MongoDbNames.UserProfileName, entity);
        return model;
    }

    private UserProfileModel GetModel(UserProfileEntity entity)
    {
        return new UserProfileModel
        {
            UserId = Guid.Parse(entity.UserId),
            Id = entity.Id,
            Name = entity.Name
        };
    }
}