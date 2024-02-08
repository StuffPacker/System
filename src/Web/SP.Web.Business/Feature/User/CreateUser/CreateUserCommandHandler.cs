using MediatR;
using Sp.Api.Client.Feature.User;
using SP.Shared.Common.Feature.User.Model;

namespace SP.Web.Business.Feature.User.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
{
    private readonly IApiUserClient _apiUserClient;

    public CreateUserCommandHandler(IApiUserClient apiUserClient)
    {
        _apiUserClient = apiUserClient;
    }

    public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var model = new UserProfileModel
        {
            UserId = request.UserId,
            Name = DateTime.UtcNow.Ticks.ToString()
        };
        await _apiUserClient.CreateUser(request.CurrentUserId, model);
        return string.Empty;
    }
}