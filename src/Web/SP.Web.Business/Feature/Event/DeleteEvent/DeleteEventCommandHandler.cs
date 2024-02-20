using System.Security.Authentication;
using MediatR;
using Sp.Api.Client.Feature.Event;

namespace SP.Web.Business.Feature.Event.DeleteEvent;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, string>
{
    private readonly IApiEventClient _apiEventClient;

    public DeleteEventCommandHandler(IApiEventClient apiEventClient)
    {
        _apiEventClient = apiEventClient;
    }

    public async Task<string> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var model = await _apiEventClient.GetById(request.Id, request.UserId);
        if (model.UserId == request.UserId)
        {
            await _apiEventClient.Delete(request.Id, request.UserId);
            return string.Empty;
        }
        else
        {
            throw new AuthenticationException();
        }
    }
}