using MediatR;
using SP.Shared.Common.Feature.User.Model;

namespace SP.Web.Business.Feature.User.GetUser;

public class GetUserCommand : IRequest<UserProfileModel?>
{
    public GetUserCommand(Guid userId, Guid currentUserId)
    {
        UserId = userId;
        CurrentUserId = currentUserId;
    }

    public Guid UserId { get; set; }

    public Guid CurrentUserId { get; set; }
}