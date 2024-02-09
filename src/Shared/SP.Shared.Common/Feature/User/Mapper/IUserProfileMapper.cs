using SP.Shared.Common.Feature.Item.Model;
using SP.Shared.Common.Feature.User.Dto;
using SP.Shared.Common.Feature.User.Model;

namespace SP.Shared.Common.Feature.User.Mapper;

public interface IUserProfileMapper
{
    UserProfileDto Map(UserProfileModel model);

    UserProfileModel Map(UserProfileDto model);

    IEnumerable<UserProfileModel> Map(IEnumerable<UserProfileDto> model);

    IEnumerable<UserProfileDto> Map(IEnumerable<UserProfileModel> model);
}