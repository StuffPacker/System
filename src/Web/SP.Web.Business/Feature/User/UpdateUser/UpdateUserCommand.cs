using MediatR;
using SP.Shared.Common.Feature.User.Model;

namespace SP.Web.Business.Feature.User.UpdateUser;

public class UpdateUserCommand : IRequest<UserProfileModel?>
{
    public UpdateUserCommand(Guid userId, UserProfileUpdateViewModel model, Guid currentUserId)
    {
        UserId = userId;
        Model = model;
        CurrentUserId = currentUserId;
    }

    public Guid CurrentUserId { get; set; }

    public Guid UserId { get; set; }

    public UserProfileUpdateViewModel Model { get; set; }
}