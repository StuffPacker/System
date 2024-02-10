using SP.Shared.Common.Feature.Item.Model;
using SP.Shared.Common.Feature.User.Dto;
using SP.Shared.Common.Feature.User.Model;

namespace SP.Shared.Common.Feature.User.Mapper;

public class UserProfileMapper : IUserProfileMapper
{
    public UserProfileDto Map(UserProfileModel model)
    {
        return new UserProfileDto
        {
            Id = model.Id,
            Name = model.Name,
            UserId = model.UserId
        };
    }

    public UserProfileModel Map(UserProfileDto model)
    {
        return new UserProfileModel
        {
            Id = model.Id,
            Name = model.Name,
            UserId = model.UserId
        };
    }

    public IEnumerable<UserProfileModel> Map(IEnumerable<UserProfileDto> model)
    {
        var list = new List<UserProfileModel>();
        foreach (var item in model)
        {
            list.Add(Map(item));
        }

        return list;
    }

    public IEnumerable<UserProfileDto> Map(IEnumerable<UserProfileModel> model)
    {
        var list = new List<UserProfileDto>();
        foreach (var item in model)
        {
            list.Add(Map(item));
        }

        return list;
    }
}