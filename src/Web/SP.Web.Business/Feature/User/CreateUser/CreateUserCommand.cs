using MediatR;

namespace SP.Web.Business.Feature.User.CreateUser;

public class CreateUserCommand : IRequest<string>
{
    public CreateUserCommand(Guid userId, Guid currentUserId)
    {
        UserId = userId;
        CurrentUserId = currentUserId;
    }

    public Guid CurrentUserId { get; set; }

    public Guid UserId { get; set; }
}