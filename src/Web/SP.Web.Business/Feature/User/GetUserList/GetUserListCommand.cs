using MediatR;
using SP.Shared.Common.Feature.User.Model;

namespace SP.Web.Business.Feature.User.GetUserList;

public class GetUserListCommand : IRequest<IEnumerable<UserProfileModel>>
{
    public GetUserListCommand(Guid currentUser)
    {
        CurrentUser = currentUser;
    }

    public Guid CurrentUser { get; set; }
}