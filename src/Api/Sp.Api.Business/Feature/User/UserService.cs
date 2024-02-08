using SP.Database.Mongo.Feature.User;
using SP.Database.Mongo.Feature.UserItem;
using SP.Shared.Common.Feature.User.Dto;
using SP.Shared.Common.Feature.User.Model;

namespace Sp.Api.Business.Feature.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserProfileModel?> GetUser(Guid id, Guid currentUserId)
    {
        var result = await _userRepository.GetByUserId(id);
        return result;
    }

    public async Task<UserProfileModel> CreateUser(Guid user, UserProfileModel model)
    {
        var result = await _userRepository.CreateUserProfile(model);
        return result;
    }

    public async Task<UserProfileModel> UpdateUser(Guid user, UserProfileModel model)
    {
        var result = await _userRepository.Update(model);
        return result;
    }
}