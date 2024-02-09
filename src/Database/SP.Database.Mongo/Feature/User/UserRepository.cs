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

    public async Task<UserProfileModel> Update(UserProfileModel model)
    {
        var entity = GetEntity(model);
        var db = _databaseClient.GetDatabase();
        var collection = db.GetCollection<UserProfileEntity>(MongoDbNames.UserProfileName);
        await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        return GetModel(entity);
    }

    public async Task<IEnumerable<UserProfileModel>> GetUsers()
    {
        var db = _databaseClient.GetDatabase();
        var collection = db.GetCollection<UserProfileEntity>(MongoDbNames.UserProfileName);

        var filter = MongoDB.Driver.Builders<UserProfileEntity>.Filter.Empty; // .Eq("UserId", userId.ToString());
        var results = await (await collection.FindAsync(filter)).ToListAsync();
        var list = new List<UserProfileModel>();
        foreach (var item in results)
        {
            list.Add(GetModel(item));
        }

        return list;
    }

    private UserProfileEntity GetEntity(UserProfileModel model)
    {
        return new UserProfileEntity
        {
            Id = model.Id,
            UserId = model.UserId.ToString(),
            Name = model.Name
        };
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