using SP.Shared.Common.Feature.User.Model;

namespace SP.Database.Mongo.Feature.User;

public interface IUserRepository
{
    Task<UserProfileModel> GetByUserId(Guid userId);

    Task<UserProfileModel> CreateUserProfile(UserProfileModel model);

    Task<UserProfileModel> Update(UserProfileModel model);
}