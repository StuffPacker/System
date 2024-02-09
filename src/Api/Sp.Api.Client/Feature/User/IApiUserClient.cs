using SP.Shared.Common.Feature.User.Model;

namespace Sp.Api.Client.Feature.User;

public interface IApiUserClient
{
    Task<UserProfileModel?> GetUser(Guid id, Guid currentUser);

    Task CreateUser(Guid currentUserId, UserProfileModel model);

    Task<object> UpdateUser(Guid currentUserId, UserProfileModel model);

    Task<IEnumerable<UserProfileModel>> GetUserList(Guid currentUser);
}