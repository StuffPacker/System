using System.Security.Cryptography;
using SP.Database.Mongo.Entity;
using SP.Shared.Common.Feature.User.Model;

namespace SP.Database.Mongo.Feature.User;

public class UserProfileEntity : BaseEntity
{
    public UserProfileEntity(UserProfileModel model)
    {
        Id = model.Id;
        UserId = model.UserId.ToString();
        Name = model.Name;
    }

    public UserProfileEntity()
    {
    }

    public string Name { get; set; }

    public string UserId { get; set; }
}