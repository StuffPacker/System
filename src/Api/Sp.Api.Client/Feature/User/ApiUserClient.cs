using Sp.Api.Client.Feature.Client;
using SP.Shared.Common;
using SP.Shared.Common.Feature.PackingList.Dto;
using SP.Shared.Common.Feature.User.Dto;
using SP.Shared.Common.Feature.User.Mapper;
using SP.Shared.Common.Feature.User.Model;

namespace Sp.Api.Client.Feature.User;

public class ApiUserClient : IApiUserClient
{
    private readonly ISpApiClient _apiClient;
    private readonly IUserProfileMapper _userProfileMapper;

    public ApiUserClient(ISpApiClient apiClient, IUserProfileMapper userProfileMapper)
    {
        _apiClient = apiClient;
        _userProfileMapper = userProfileMapper;
    }

    public async Task<UserProfileModel?> GetUser(Guid id, Guid currentUser)
    {
        var result = await _apiClient.GetSecure("SpApi/v1/user/" + id, currentUser.ToString());
        if (result == null)
        {
            return null;
        }

        var dto = JsonHandler.Deserialize<UserProfileDto>(result);
        var model = _userProfileMapper.Map(dto!);
        return model;
    }

    public async Task CreateUser(Guid currentUserId, UserProfileModel model)
    {
        var dto = _userProfileMapper.Map(model);
        var result = await _apiClient.PostSecure("SpApi/v1/user/", currentUserId.ToString(), dto);
    }

    public async Task<object> UpdateUser(Guid currentUserId, UserProfileModel model)
    {
        var dto = _userProfileMapper.Map(model);
        var result = await _apiClient.PutSecure("SpApi/v1/user/" + model.Id + "/", currentUserId.ToString(), dto);
        return result;
    }
}