using FluentAssertions;
using Microsoft.Extensions.Options;
using SP.Database.Mongo.Feature.UserItem;
using SP.Shared.Common.Feature.Database.UserItem;
using SP.Shared.Common.Feature.Item.Model;

namespace SP.Database.Mongo.Tests;

[TestClass]
public class UserItemTest
{
    [TestMethod]
    [Ignore]
    public async Task CRUD()
    {
        var userId = Guid.NewGuid();
        var model = new ItemModel
        {
            Name = "name1",
            WeightSufix = "g",
            Weight = 999,
            UserId = userId
        };
        var name2 = "name2";
        var target = GetTarget();
        var m1 = await target.Create(model);
        var m2 = await target.Create(model);
        var result1 = await target.GetById(m1.Id);
        result1.Should().NotBeNull();
        model.Name = name2;
        await target.Update(model);
        var result2 = await target.GetById(m1.Id);
        result2.Name.Should().BeEquivalentTo(name2);

        var list = await target.GetByUserId(userId);
        list.Count().Should().BeGreaterThan(1);
        foreach (var item in list)
        {
            await target.Delete(item.Id);
        }

        var result3 = await target.GetById(m1.Id);
        result3.Should().BeNull();
    }

    private IUserItemRepository GetTarget()
    {
        IOptions<MongoDbDatabaseOptions> databaseOptions = Options.Create<MongoDbDatabaseOptions>(new MongoDbDatabaseOptions
        {
            MongoDbDatabase = "StuffPacker",
            MongoDbConnectionString = "mongodb://localhost:41017"
        });
        var databaseClient = new DatabaseClient(databaseOptions);
        return new UserItemRepository(databaseClient);
    }
}