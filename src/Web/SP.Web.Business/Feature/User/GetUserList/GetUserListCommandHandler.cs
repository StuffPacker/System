using MediatR;
using Sp.Api.Client.Feature.User;
using SP.Shared.Common.Feature.User.Model;

namespace SP.Web.Business.Feature.User.GetUserList;

public class GetUserListCommandHandler : IRequestHandler<GetUserListCommand, IEnumerable<UserProfileModel>>
{
    private readonly IApiUserClient _apiUserClient;

    public GetUserListCommandHandler(IApiUserClient apiUserClient)
    {
        _apiUserClient = apiUserClient;
    }

    public async Task<IEnumerable<UserProfileModel>> Handle(GetUserListCommand request, CancellationToken cancellationToken)
    {
        var result = await _apiUserClient.GetUserList(request.CurrentUser);
        return result;
    }
}