using SP.Shared.Common.Feature.User.Dto;
using SP.Shared.Common.Feature.User.Model;

namespace Sp.Api.Business.Feature.User;

public interface IUserService
{
    Task<UserProfileModel?> GetUser(Guid parse, Guid guid);

    Task<UserProfileModel> CreateUser(Guid user, UserProfileModel model);

    Task<UserProfileModel> UpdateUser(Guid user, UserProfileModel model);

    Task<IEnumerable<UserProfileModel>> GetUserList(Guid currentUser);
}