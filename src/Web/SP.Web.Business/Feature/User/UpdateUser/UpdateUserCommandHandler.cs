using System.Security.Authentication;
using MediatR;
using Sp.Api.Client.Feature.User;
using SP.Shared.Common.Feature.User.Model;

namespace SP.Web.Business.Feature.User.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserProfileModel?>
{
    private readonly IApiUserClient _apiUserClient;

    public UpdateUserCommandHandler(IApiUserClient apiUserClient)
    {
        _apiUserClient = apiUserClient;
    }

    public async Task<UserProfileModel?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _apiUserClient.GetUser(request.UserId, request.CurrentUserId);
        if (user!.UserId != request.CurrentUserId)
        {
            throw new AuthenticationException();
        }

        user.Name = request.Model.Name;
        var result = await _apiUserClient.UpdateUser(request.CurrentUserId, user);
        return user;
    }
}