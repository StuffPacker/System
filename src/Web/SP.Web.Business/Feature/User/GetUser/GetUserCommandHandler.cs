using MediatR;
using Sp.Api.Client.Feature.User;
using SP.Shared.Common.Feature.User.Model;

namespace SP.Web.Business.Feature.User.GetUser;

public class GetUserCommandHandler : IRequestHandler<GetUserCommand, UserProfileModel?>
{
    private readonly IApiUserClient _apiUserClient;

    public GetUserCommandHandler(IApiUserClient apiUserClient)
    {
        _apiUserClient = apiUserClient;
    }

    public async Task<UserProfileModel?> Handle(GetUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _apiUserClient.GetUser(request.UserId, request.CurrentUserId);
        return result;
    }
}